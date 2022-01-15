using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp9;

public class test
{
    private static void Sample_Aggregate_Lambda_Simple()
    {
        var result = new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 }.Aggregate((a, b) => a * b);

        Debug.WriteLine("Aggregated numbers by multiplication:");
        Debug.WriteLine(result);
    }

}
