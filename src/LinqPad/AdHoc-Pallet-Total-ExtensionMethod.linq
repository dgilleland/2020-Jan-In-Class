<Query Kind="Program" />

void Main()
{
    var data = new List<Pallet>
    {
        new Pallet("Red Deer", 200),
        new Pallet("Olds", 100),
        new Pallet("Calgary", 220),
        new Pallet("Calgary", 575)
    };
    double totalWeight = data.Total(x => x.Weight);
    totalWeight.Dump("Weight of shipments");
}

// Define other methods and classes here
public static class MyExtensions
{
    public static double Total<T>(this IEnumerable<T> collection, Func<T, double> method)
    {
        double result = 0;
        foreach(T item in collection)
        {
            result += method(item);
        }
        return result;
    }
}

public class Pallet
{
    public string DestinationCity { get; set; }
    public double Weight { get; set; }
    public Pallet(string city, double weight)
    {
        DestinationCity = city;
        Weight = weight;
    }
}


