<Query Kind="Program" />

void Main()
{
    Instructors.Dump();
    // This is a demo of using Query syntax with Linq
    //  foreach(var person in Instructors)
    var best = from person in Instructors
    //             \string/   \string[]/
               where person.StartsWith("D")
               select person;
    best.Dump("The best instructors");
}

// Define other methods and classes here
public IEnumerable<string> Instructors
{
    get
    {
        yield return "Dan";
        yield return "Don";
        yield return "Sam";
        yield return "Jim";
        yield return "Frank";
        yield return "Steve";
    }
}