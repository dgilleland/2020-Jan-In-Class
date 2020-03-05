<Query Kind="Statements">
  <Connection>
    <ID>10605093-fece-445c-afdc-14b6523b9b64</ID>
    <Persist>true</Persist>
    <Server>.</Server>
    <Database>WestWind</Database>
  </Connection>
</Query>

var trash = Shipments.Where(x => x.OrderID == 11077);
trash.Dump();
foreach(var item in trash)
{
	foreach(var sent in item.ManifestItems)
	 	ManifestItems.DeleteOnSubmit(sent);
	ManifestItems.Context.SubmitChanges();
	Shipments.DeleteOnSubmit(item);
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








