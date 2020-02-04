<Query Kind="Program" />

void Main()
{
    "Bob".Dump();
    var firstCar = new Car { Model = "Chev", Make = "Impala" };
    firstCar.Dump();
    string name = "Dan";
    name.Quack().Dump();
}

// Define other methods and classes here
public class Car
{
    public string Model { get; set; }
    public string Make  { get; set; }
}

// Define our extension methods
public static class StringExtensions
{
    public static string Quack(this string self)
    {
        return self + " (quack)";
    }
}














