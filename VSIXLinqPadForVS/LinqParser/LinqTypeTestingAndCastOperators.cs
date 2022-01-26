using System.Linq;

namespace VSIXLinqPadForVS.LinqParser
{
    public class LinqTypeTestingAndCastOperators
    {
        // Type-testing-and-cast Operators Reference URL: (https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/operators/type-testing-and-cast)
        // Is-operator Reference URL: (https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/operators/type-testing-and-cast#is-operator)
        // As-operator Reference URL: (https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/operators/type-testing-and-cast#as-operator)
        // Cast-expression Reference URL: (https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/operators/type-testing-and-cast#cast-expression)
        // Typeof-operator Reference URL: (https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/operators/type-testing-and-cast#typeof-operator)
        public static readonly string[] TypeTestingAndCastOperators = "is as cast typeof".Split().ToArray();

        public enum CSharp_TypeTestingAndCastOperators
        {
            Is,
            As,
            Cast,
            Typeof
        }
    }
}
