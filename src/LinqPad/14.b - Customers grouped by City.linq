<Query Kind="Expression">
  <Connection>
    <ID>18109b28-6563-40c1-97b9-460c7b5b66d7</ID>
    <Persist>true</Persist>
    <Server>.</Server>
    <Database>WestWind</Database>
  </Connection>
</Query>

from row in Customers
where row.Address.City.StartsWith("M")
group  row   by   row.Address.City into CustomersByCountry
//    \what/      \       how       /
where CustomersByCountry.Count() > 2
select new
{
   City = CustomersByCountry.Key, // the key is "how" we have sorted the data
   Customers = from data in CustomersByCountry
               select new
               {
                   Company = data.CompanyName,
                   Contact = data.ContactName
               }
}