<Query Kind="Expression">
  <Connection>
    <ID>7d565fca-16c5-4615-89fa-17901cae76d0</ID>
    <Persist>true</Persist>
    <Server>.</Server>
    <Database>WestWind</Database>
  </Connection>
</Query>

// List the full name of all the employees who look after 7 or more territories
from person in Employees
where person.EmployeeTerritories.Count >= 7
//          \navigation property/
select person.FirstName + " " + person.LastName