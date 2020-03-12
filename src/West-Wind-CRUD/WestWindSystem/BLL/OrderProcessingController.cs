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
            // TODO: ShipOrder - Validation of input:
            //  - OrderId must exist
            //  - Shipper must exist
            //  - Must have one or more items to ship
            //  - ProductIds must exist/valid
            //  - Quantities must be greater than 0 and less than the number/qty outstanding on the order
            //  - Freight charge is either null or a value greater than zero
            // TODO: ShipOrder - Add a new Shipment to the database
            // TODO: ShipOrder - Add new ManifestItem objects to the new shipment
        }
        #endregion
    }
}
