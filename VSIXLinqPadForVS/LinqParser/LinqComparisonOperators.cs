using System.Linq;

namespace VSIXLinqPadForVS.LinqParser
{
    public class LinqComparisonOperators
    {
        // Comparison-operators Reference URL: (https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/operators/comparison-operators)
        // Less-than-operator Reference URL: (https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/operators/comparison-operators#less-than-operator-)
        // Greater-than-operator Reference URL: (https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/operators/comparison-operators#greater-than-operator-)
        // Less-than-or-equal-operator Reference URL: (https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/operators/comparison-operators#less-than-or-equal-operator-)
        // Greater-than-or-equal-operator Reference URL: (https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/operators/comparison-operators#greater-than-or-equal-operator-)
        public static readonly string[] EqualityOperators = "< > <= >=".Split().ToArray();

        public enum CSharp_EqualityOperators
        {
            LessThan,
            GreaterThan,
            LessThanOrEqual,
            GreaterThanOrEqual
        }
    }
}
