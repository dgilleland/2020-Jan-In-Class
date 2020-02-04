<Query Kind="Expression">
  <Connection>
    <ID>7d565fca-16c5-4615-89fa-17901cae76d0</ID>
    <Persist>true</Persist>
    <Server>.</Server>
    <Database>WestWind</Database>
  </Connection>
</Query>

// Shipper Data.linq
// List the names and phone numbers of all the shippers
// along with the number of shipments they have done.
// Use an anonymous type for your results.
from company in Shippers
select new
{
    Name = company.CompanyName,
    PhoneNumber = company.Phone,
    NumberOfShipments = company.ShipViaShipments.Count()
}
