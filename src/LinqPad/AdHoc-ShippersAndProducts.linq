<Query Kind="Expression">
  <Connection>
    <ID>18109b28-6563-40c1-97b9-460c7b5b66d7</ID>
    <Persist>true</Persist>
    <Server>.</Server>
    <Database>WestWind</Database>
  </Connection>
</Query>

// List all the shippers and the products that they shipped.
// Include the company name and phone of the shipper
// along with the name and quantity of the items shipped.

from eachRow in Shippers
select new
{
    Company = eachRow.CompanyName,
    Phone = eachRow.Phone,
    ItemsShipped = from shipped in eachRow.ShipViaShipments
                   from item in shipped.ManifestItems
                   select new
                   {
                        Product = item.Product.ProductName,
                        Qty = item.ShipQuantity
                   }
}

// TODO: Also read the Notes->Linq Intro and
//       the Demos -> eRestaurant -> Linq - Query and Method Syntax