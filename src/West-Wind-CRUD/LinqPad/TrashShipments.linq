<Query Kind="Statements">
  <Connection>
    <ID>149e3b65-50a0-4b71-a4cd-1724339bc361</ID>
    <Persist>true</Persist>
    <Server>.</Server>
    <Database>WestWind</Database>
  </Connection>
</Query>

var trash = Shipments.Where(x => x.OrderID == 11077);
trash.Dump();
foreach(var item in trash)
{
    foreach (var sent in item.ManifestItems)
    {
        //ManifestItems.DeleteOnSubmit(sent);
        sent.ShipQuantity = (short)(sent.ShipQuantity / 2);
        if (sent.ShipQuantity == 0)
            sent.ShipQuantity = 1;
    }
	ManifestItems.Context.SubmitChanges();
	//Shipments.DeleteOnSubmit(item);
}
Shipments.Context.SubmitChanges();
// Trash only specific supplier products from specific orders
var trashSpecific = from item in ManifestItems
                    where (item.Product.SupplierID == 3
					       || item.Product.SupplierID == 12)
					   && (item.Shipment.OrderID == 11076
					       || item.Shipment.OrderID == 11072)
				    select item;
foreach(var sent in trashSpecific)
    ManifestItems.DeleteOnSubmit(sent);
ManifestItems.Context.SubmitChanges();
Orders.Single(o => o.OrderID == 11072).Shipped = false;
Orders.Single(o => o.OrderID == 11076).Shipped = false;
Orders.Single(o => o.OrderID == 11077).Shipped = false;
Orders.Context.SubmitChanges();