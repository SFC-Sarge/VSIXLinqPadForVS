<Query Kind='Program' />
void Main()
{
Sample_Cast_Lambda();
}
static void Sample_Cast_Lambda()
{
List<string> vegetables = new List<string> { "Cucumber", "Tomato", "Broccoli" };

var value = vegetables.Cast<string>();

Debug.WriteLine("List of vegetables casted to a simple string array:");
foreach (string vegetable in value)
Debug.WriteLine(vegetable);
}