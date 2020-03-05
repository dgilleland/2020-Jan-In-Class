<Query Kind="Expression">
  <Connection>
    <ID>10605093-fece-445c-afdc-14b6523b9b64</ID>
    <Persist>true</Persist>
    <Server>.</Server>
    <Database>WestWind</Database>
  </Connection>
</Query>

// Find all the Suppliers that provide products for Order # 11077.
from item in OrderDetails
where item.OrderID == 11077
select new
{
	item.Product.ProductName,
	item.Product.Supplier.CompanyName,
	item.Product.SupplierID
}
// I see that I can use SupplierIDs of 3 and 12. So, find what other
// Orders have items from those suppliers.
(
from item in OrderDetails
where item.Product.SupplierID == 3
   || item.Product.SupplierID == 12
select new { item.OrderID, item.Product.SupplierID }
).Distinct()
// I found orders 11072 && 11076