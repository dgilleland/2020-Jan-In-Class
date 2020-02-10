<Query Kind="Expression">
  <Connection>
    <ID>7d565fca-16c5-4615-89fa-17901cae76d0</ID>
    <Persist>true</Persist>
    <Server>.</Server>
    <Database>WestWind</Database>
  </Connection>
</Query>

from company in Customers
//   Customer    Customer[]
group company by company.Address.Country 
                 // this is the key
	into companyByCountry // both a "collection" of Customers AND has info on the Key
	//   Grouping<Key, Customer[]>
select  //companyByCountry
new {
	CountryName = companyByCountry.Key,
	Count = companyByCountry.Count(),
	Company = from c in companyByCountry
	          select new
			  {
			  	Name = c.CompanyName,
				FreightCharges = c.Orders.Where(x => x.Freight.HasValue).Sum(x => x.Freight)
			  }
}

