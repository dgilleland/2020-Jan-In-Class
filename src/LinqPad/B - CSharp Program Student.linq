<Query Kind="Program" />

void Main()
{
	var freshman = new Student("Bob Smith", new DateTime(1993, 8, 14));
	freshman.Dump();
}

// Define other methods and classes here
public class Student
{
	public string Name { get; private set; }
	public DateTime DOB { get; private set; }
	public int Age { get { return (DateTime.Today.Year - DOB.Year); } }
	
	// Constructor
	public Student(string name, DateTime birthdate)
	{
		if(string.IsNullOrWhiteSpace(name)) throw new ArgumentNullException("Invalid name");
		if(birthdate > DateTime.Today) throw new ArgumentException("Invalid birthdate - no futures");
		Name = name.Trim();
		DOB = birthdate;
	}
}