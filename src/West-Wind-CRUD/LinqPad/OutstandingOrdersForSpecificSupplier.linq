<Query Kind="Statements">
  <Connection>
    <ID>10605093-fece-445c-afdc-14b6523b9b64</ID>
    <Server>.</Server>
    <Database>WestWind</Database>
  </Connection>
</Query>

// List the outstanding orders of products provided by SupplierID 3 (or 12)
int supplierId = 12;

var result = from ord in Orders
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
                        ? ord.Ship
                        : ord.Customer.Address,
                 Comments = ord.Comments,
                 OutstandingItems =
                    from detail in ord.OrderDetails
                    where detail.Product.SupplierID == supplierId
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
result.Dump();
var nextResult  = from item in result
                  select new //OutstandingOrder
                  {
                      item.OrderId,
                      item.ShipToName,
                      item.OrderDate,
                      item.RequiredBy,
                      FullShippingAddress = item.ShipTo.Address + Environment.NewLine +
                                            item.ShipTo.City + Environment.NewLine +
                                            item.ShipTo.Region + " " +
                                            item.ShipTo.Country + ", " +
                                            item.ShipTo.PostalCode,
                      item.Comments,
                      OutstandingItems = from detail in item.OutstandingItems
                                         select new //OrderProductInformation
                                         {
                                             detail.ProductId,
                                             detail.ProductName,
                                             detail.Qty,
                                             detail.QtyPerUnit,
                                             Outstanding = detail.ShippedQtys.Count() > 0
                                                         ? detail.Qty - detail.ShippedQtys.Cast<int>().Sum()
                                                         : detail.Qty
                                         }
                  };

var finalResult = nextResult.Where(x => x.OutstandingItems.Count() > 0);

finalResult.Dump();








