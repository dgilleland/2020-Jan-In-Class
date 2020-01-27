<Query Kind="Program" />

/* Keyboard Shortcuts
   ctrl + k, ctrl + c  ==> Comment selected line(s)
   ctrl + k, ctrl + u  ==> Un-comment selected line(s)
   ctrl + r            ==> Toggle the results window
*/
void Main()
{
//	DemoMath();
	MoreMath();
}

// Define other methods and classes here
void MoreMath()
{
	int diameter = 10;
	double area = Math.PI * (diameter / 2) * (diameter / 2);
	area.Dump("Area of a 10cm diameter circle");
	
    double area2 = Math.PI * 5 * 5;
	area2.Dump();
}

void DemoMath()
{
	int quantity = 10;
	double weight = 4.93;
	double totalWeight = quantity * weight;
	Console.WriteLine(totalWeight);
}










