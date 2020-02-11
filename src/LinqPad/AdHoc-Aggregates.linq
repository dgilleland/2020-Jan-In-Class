<Query Kind="Statements">
  <Connection>
    <ID>18109b28-6563-40c1-97b9-460c7b5b66d7</ID>
    <Persist>true</Persist>
    <Server>.</Server>
    <Database>WestWind</Database>
  </Connection>
</Query>

/* Aggregate Extension Methods
    - Count()
    - Sum()
    - Average()
    - Min()
    - Max()
*/
// 1) Date of the most recent order
var mostRecent = Orders.Max(anOrder => anOrder.OrderDate);
mostRecent.Dump("Date of the most recent order");

// 2) Cheapest selling price of all the products we sell
var cheapest = Products.Min(x => x.UnitPrice);
cheapest.ToString("C").Dump("Cheapest selling price of all the products we sell");

// 3) How much has the company received in payments?
var total = Payments.Sum(aPayment => aPayment.Amount);
total.ToString("C").Dump("How much has the company received in payments");

// 4) For the most recent order, what is the total of all items sold on that order?
var step1 = from sale in Orders
            where sale.OrderDate == mostRecent
            select sale;
step1.Dump();
var step2 = from sale in step1
            select sale.OrderDetails.Sum(x => x.UnitPrice * (1 - (decimal)x.Discount) * x.Quantity);
step2.Dump();

// 5) What is the average of all orders?
var temp = from sale in Orders
            select sale.OrderDetails.Sum(x => x.UnitPrice * (1 - (decimal)x.Discount) * x.Quantity);
temp.Average().ToString("C").Dump("The average of all orders");

// 6) Who has been working at the company the longest?
var temp2 = from row in Employees
            where row.HireDate == Employees.Min(x => x.HireDate)
            select row.FirstName + row.LastName;






