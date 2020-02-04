<Query Kind="Program">
  <Connection>
    <ID>7d565fca-16c5-4615-89fa-17901cae76d0</ID>
    <Persist>true</Persist>
    <Server>.</Server>
    <Database>WestWind</Database>
  </Connection>
</Query>

void Main()
{
    // I can create an object with a constructor
    var someone = new Person("Dwight", new DateTime(1995, 7, 14));
    someone.Dump();
    // or, I can use an initializer list
    someone = new Person
    {
        Name = "Ingrid",
        Age = 27
    };
    someone.Dump();
    // Anonymous Types
    // An anonymous type is an object that does not have a underlying class defined for it.
    // The properties of the object are "declared" dynamically, inside the initializer list.
    var something = new
    {
        // I can declare whatever properties I want here
        Color = "White",
        Width = "127",
        Units = "meters"
    };
    something.Dump();
}

// Define other methods and classes here
public class Person
{
    public string Name { get; set; }
    public int Age { get; set; }
    public Person() {} // This is a parameterless constructor
    public Person(string name, DateTime dob)
    {
        Name = name;
        Age = DateTime.Today.Year - dob.Year;
    }
}