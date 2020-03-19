using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WestWindSystem.DataModels.OrderProcessing;
using WestWindSystem.DAL;

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
                var existingOrder = context.Orders.Find(orderId); // find by PK
                if (existingOrder == null)
                    throw new Exception("Order does not exist.");
                if (existingOrder.Shipped)
                    throw new Exception("This order has already been completed.");
                if (!existingOrder.OrderDate.HasValue)
                    throw new Exception("This order is not ready to be shipped (no order date has been specified).");

                //  - Shipper must exist
                var shipper = context.Shippers.Find(directions.ShipperId); // find by PK
                if (shipper == null)
                    throw new Exception("Invalid shipper ID.");
                //  - Freight charge is either null or a value greater than zero
                // TODO: Q) Should I just convert a $0 charge to a null??
                if (directions.FreightCharge.HasValue && directions.FreightCharge <= 0)
                    throw new Exception("Freight charge must be either a positive value or no charge.");

                //  - Must have one or more items to ship
                if (!items.Any()) // if there are not any items
                    throw new Exception("No products identified for shipping.");

                foreach (var item in items)
                {
                    if (item == null) throw new Exception("Blank item listed in the products to be shipped.");
                    //  - ProductIds must exist/valid (this product must be part of the original order)
                    if (!existingOrder.OrderDetails.Any(x => x.ProductID == item.ProductId)) // if the order's details do NOT have that product in the collection
                        throw new Exception($"The product {item.ProductId} does not exist on the order.");
                    //  - Quantities must be greater than 0 
                    //  - Quantities must be less than the number/qty outstanding on the order
                }
                #endregion

                #region Processing the order as a transaction
                // TODO: ShipOrder - Add a new Shipment to the database
                // TODO: ShipOrder - Add new ManifestItem objects to the new shipment
                #endregion
            }
            throw new NotImplementedException("ShipOrder is not yet implemented");
        }
        #endregion
    }
}
