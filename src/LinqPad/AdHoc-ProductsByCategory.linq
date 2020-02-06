<Query Kind="Program">
  <Connection>
    <ID>148e5189-ad11-4ba4-95be-e6d5ea18be8f</ID>
    <Persist>true</Persist>
    <Server>.</Server>
    <Database>WestWind</Database>
  </Connection>
</Query>

void Main()
{
    // List all the categories and the products in that category
    // We need the name, description, and picture of the category,
    // along with the name, quantity/unit, and price of the products.
    var result = from cat in Categories
                 select new CategorizedProducts
                 {
                     Name = cat.CategoryName,
                     Description = cat.Description,
                     Picture = cat.Picture.ToArray(), //.ToImage()
                     Products = from item in cat.Products
                               select new ProductInfo
                                {
                                    Name = item.ProductName,
                                    QuantityPerUnit = item.QuantityPerUnit,
                                    Price = item.UnitPrice
                                }
                 };
    result.Dump();             
}

// Define other methods and classes here
public class ProductInfo // POCO class (Plain Old CLR Object)
{
    public string Name { get; set; }
    public string QuantityPerUnit { get; set; }
    public decimal Price { get; set; }
}

public class CategorizedProducts // DTO (Data Transfer Object)
{
    public string Name { get; set; }
    public string Description { get; set; }
    public byte[] Picture { get; set; }
    public IEnumerable<ProductInfo> Products { get; set; }
}








