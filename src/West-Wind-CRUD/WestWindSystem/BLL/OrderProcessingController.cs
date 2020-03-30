using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WestWindSystem.DataModels.OrderProcessing;
using WestWindSystem.DAL;
using WestWindSystem.Entities;

namespace WestWindSystem.BLL
{
    [DataObject]
    public class OrderProcessingController
    {
        #region Query Methods
        [DataObjectMethod(DataObjectMethodType.Select)]
        public List<OutstandingOrder> LoadOrders(int supplierID)
        {
            using (var context = new WestWindContext())
            {
                var result = from ord in context.Orders
                             where !ord.Shipped // Still items to be shipped
                                && ord.OrderDate.HasValue // The order has been placed and is ready to ship
                             select new
                             {
                                 OrderId = ord.OrderID,
                                 ShipToName = ord.ShipName,
                                 OrderDate = ord.OrderDate.Value,
                                 RequiredBy = ord.RequiredDate.Value,
                                 // Note to self:
                                 // If there is a ShipTo address, use that, otherwise use the customer address
                                 ShipTo = ord.ShipAddressID.HasValue
                                        ? ord.Address
                                        : ord.Customer.Address,
                                 Comments = ord.Comments,
                                 OutstandingItems =
                                    from detail in ord.OrderDetails
                                    where detail.Product.SupplierID == supplierID
                                    select new
                                    {
                                        ProductId = detail.ProductID,
                                        ProductName = detail.Product.ProductName,
                                        Qty = detail.Quantity,
                                        QtyPerUnit = detail.Product.QuantityPerUnit,
                                        ShippedQtys = (from ship in detail.Order.Shipments
                                                       from item in ship.ManifestItems
                                                       where item.ProductID == detail.ProductID
                                                       select item.ShipQuantity)
                                    }
                             };

                var nextResult = from item in result
                                 select new OutstandingOrder
                                 {
                                     OrderID = item.OrderId,
                                     ShipToName = item.ShipToName,
                                     OrderedDate = item.OrderDate,
                                     RequiredDate = item.RequiredBy,
                                     FullShippingAddress = item.ShipTo.Address1 + Environment.NewLine +
                                                           item.ShipTo.City + Environment.NewLine +
                                                           item.ShipTo.Region + " " +
                                                           item.ShipTo.Country + ", " +
                                                           item.ShipTo.PostalCode,
                                     Comments = item.Comments,
                                     OutstandingItems = from detail in item.OutstandingItems
                                                        select new ProductSummary
                                                        {
                                                            ProductID = detail.ProductId,
                                                            ProductName = detail.ProductName,
                                                            Quantity = detail.Qty,
                                                            QtyPerUnit = detail.QtyPerUnit,
                                                            OutstandingQty = detail.ShippedQtys.Count() > 0
                                                                        ? detail.Qty - detail.ShippedQtys.Cast<int>().Sum()
                                                                        : detail.Qty
                                                        }
                                 };

                var finalResult = nextResult.Where(x => x.OutstandingItems.Count() > 0);

                return finalResult.ToList();
            }
        }

        [DataObjectMethod(DataObjectMethodType.Select)]
        public List<ShipperSelection> ListShippers()
        {
            using(var context = new WestWindContext())
            {
                var results = from company in context.Shippers
                              select new ShipperSelection
                              {
                                  Name = company.CompanyName,
                                  ShipperID = company.ShipperID
                              };
                return results.ToList();
            }
        }

        public SupplierSummary GetSupplier(int supplierID)
        {
            // TODO: Implement GetSupplier
            throw new NotImplementedException();
        }
        #endregion

        #region Command Methods
        public void ShipOrder(int orderId, ShippingDirections directions, List<ProductShipment> items)
        {
            // 0) Pre-check - make sure we don't have NULL objects
            if (directions == null) throw new ArgumentNullException("No shipping directions provided.");
            if (items == null) throw new ArgumentNullException("No shipment items were provided.");

            using (var context = new WestWindContext())
            {
                #region Business Rule validations
                // TODO: ShipOrder - Validation of input:
                //  - OrderId must exist/valid
                var violations = new List<Exception>();

                var existingOrder = context.Orders.Find(orderId); // find by PK
                if (existingOrder == null)
                    violations.Add(new Exception("Order does not exist."));
                else
                {
                    if (existingOrder.Shipped)
                        violations.Add(new Exception("This order has already been completed."));
                    if (!existingOrder.OrderDate.HasValue)
                        violations.Add(new Exception("This order is not ready to be shipped (no order date has been specified)."));
                }
                //  - Shipper must exist
                var shipper = context.Shippers.Find(directions.ShipperId); // find by PK
                if (shipper == null)
                    violations.Add(new Exception("Invalid shipper ID."));
                //  - Freight charge is either null or a value greater than zero
                // TODO: Q) Should I just convert a $0 charge to a null??
                if (directions.FreightCharge.HasValue && directions.FreightCharge <= 0)
                    violations.Add(new Exception("Freight charge must be either a positive value or no charge."));

                //  - Must have one or more items to ship
                if (!items.Any()) // if there are not any items
                    violations.Add(new Exception("No products identified for shipping."));

                foreach (var item in items)
                {
                    if (item == null) violations.Add(new Exception("Blank item listed in the products to be shipped."));
                    else
                    {
                        //  - ProductIds must exist/valid (this product must be part of the original order)
                        if (!existingOrder.OrderDetails.Any(x => x.ProductID == item.ProductId)) // if the order's details do NOT have that product in the collection
                            violations.Add(new Exception($"The product {item.ProductId} does not exist on the order."));
                        //  - Quantities must be greater than 0 
                        //  - Quantities must be less than the number/qty outstanding on the order
                    }
                }

                // Check to see if any problems were identified
                if(violations.Any())
                {
                    throw new  BusinessRuleException(nameof(ShipOrder), violations);
                }
                #endregion

                #region Processing the order as a transaction
                // 1) Create a new Shipment
                var ship = new Shipment // entity class
                {
                    OrderID = orderId,
                    ShipVia = directions.ShipperId,
                    TrackingCode = directions.TrackingCode,
                    FreightCharge = directions.FreightCharge.HasValue
                                  ? directions.FreightCharge.Value
                                  : 0,
                    ShippedDate = DateTime.Now // grab the current date/time
                };

                // 2) Create manifest items for my shipment
                foreach(var item in items)
                {
                    // Notice that I'm adding the manifes item to the Shipment object
                    // rather than directly to the database context.
                    // That's because, by adding to the Shipment object, the correct
                    // values for foreign key fields will be assigned to the new data
                    // (because the ManifestItem.ShipmentID can only be known after
                    // the Shipment has been inserted - Shipment.ShipmentID is an
                    // IDENTITY column in the database).
                    ship.ManifestItems.Add(new ManifestItem
                    {
                        ProductID = item.ProductId,
                        ShipQuantity = item.Quantity
                    });
                }

                // TODONE: 3) Check if the order is complete; if so, update Order.Shipped
                // Get existing order with the quantities shipped from previous shipments
                var quantities = from detail in context.OrderDetails // existingOrder.OrderDetails
                                 where detail.OrderID == orderId
                                 select new ShipmentItemComparison // An ad-hoc class created for doing my comparisons
                                 {
                                     ProductID = detail.ProductID,
                                     ExpectedQuantity = (int)detail.Quantity,
                                     ShipQuantity = (from sent in detail.Product.ManifestItems
                                                    where sent.Shipment.OrderID == orderId
                                                    select (int) sent.ShipQuantity).Sum()
                                 };
                foreach(var toShip in items)
                {
                    quantities.Single(x => x.ProductID == toShip.ProductId).ShipQuantity += (int)toShip.Quantity;
                }

                if (quantities.All(x => x.ShipQuantity == x.ExpectedQuantity))
                {
                    existingOrder.Shipped = true;
                    context.Entry(existingOrder).Property(x => x.Shipped).IsModified = true;
                }

                // 4) Add the shipment to the database context
                context.Shipments.Add(ship);

                // 5) Save the changes (as a single transaction)
                context.SaveChanges();
                #endregion
            }
        }
        #endregion
    }
}
