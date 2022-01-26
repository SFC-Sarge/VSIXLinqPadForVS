using System.Linq;

namespace VSIXLinqPadForVS.LinqParser
{
    public class LinqUserDefinedConversionOperators
    {
        // User-Defined Conversion Operators Reference URL: (https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/operators/user-defined-conversion-operators)
        // Implicit Conversions Reference URL: (https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/language-specification/conversions#102-implicit-conversions)
        // Explicit Conversions Reference URL: (https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/language-specification/conversions#103-explicit-conversions)
        public static readonly string[] UserDefinedConversionOperators = "implicit explicit".Split().ToArray();

        public enum CSharp_UserDefinedConversionOperators
        {
            Implicit,
            Explicit
        }
    }
}
