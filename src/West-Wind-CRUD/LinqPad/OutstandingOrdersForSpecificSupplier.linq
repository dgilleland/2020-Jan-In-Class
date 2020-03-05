<Query Kind="Expression">
  <Connection>
    <ID>10605093-fece-445c-afdc-14b6523b9b64</ID>
    <Persist>true</Persist>
    <Server>.</Server>
    <Database>WestWind</Database>
  </Connection>
</Query>

// List the outstanding orders of products provided by SupplierID 3 (or 12)
from sale in Orders
where sale.OrderDetails.Any(item => item.Product.SupplierID == 3)
// && also remember to only choose orders that have outstanding quantities
select sale
