{\rtf1\ansi\ansicpg1252\deff0\deflang1033\deflangfe1033\deftab2000{\fonttbl{\f0\fnil\fprq12\fcharset0 Times New Roman;}}
\viewkind4\uc1\pard\f0\fs24 <Query Kind="Program" />\par
\par
void Main()\par
\{\par
\tab Sample_Aggregate_Lambda_Simple();\par
\}\par
\par
private static void Sample_Aggregate_Lambda_Simple()\par
\{\par
\tab var numbers = new int[] \{ 1, 2, 3, 4, 5 \};\par
\tab var result = numbers.Aggregate((a, b) => a * b);\par
\par
\tab Debug.WriteLine("Aggregated numbers by multiplication:");\par
\tab Debug.WriteLine(result);\par
\}\par
}
 