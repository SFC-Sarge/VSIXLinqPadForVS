using System.Linq;

namespace VSIXLinqPadForVS.LinqParser
{
    public class LinqEqualityOperators
    {
        // Equality Operators Reference URL: (https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/operators/equality-operators)
        // Equality-operator Reference URL: (https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/operators/equality-operators#equality-operator-)
        // Inequality-operator Reference URL: (https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/operators/equality-operators#inequality-operator-)
        public static readonly string[] EqualityOperators = "== !=".Split().ToArray();

        public enum CSharp_EqualityOperators
        {
            Equality,
            Inequality
        }
    }
}
