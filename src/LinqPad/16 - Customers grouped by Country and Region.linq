<Query Kind="Expression">
  <Connection>
    <ID>18109b28-6563-40c1-97b9-460c7b5b66d7</ID>
    <Persist>true</Persist>
    <Server>.</Server>
    <Database>WestWind</Database>
  </Connection>
</Query>

// List all the customers grouped by country and region.
from row in Customers
group row by new { Country = row.Address.Country, Region = row.Address.Region } into CustomerGroups
select new
{
   Key = CustomerGroups.Key,
   Country = CustomerGroups.Key.Country,
   Region = CustomerGroups.Key.Region,
   Customers = from data in CustomerGroups
               select new
               {
                   Company = data.CompanyName,
                   City = data.Address.City
               }
}