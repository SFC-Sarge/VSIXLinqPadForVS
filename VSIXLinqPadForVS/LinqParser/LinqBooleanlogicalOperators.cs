using System.Linq;

namespace VSIXLinqPadForVS.LinqParser
{
    public class LinqBooleanlogicalOperators
    {
        // Boolean logical Arithmetic Operators Reference URL: (https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/operators/arithmetic-operators)
        // Unary Boolean logical Negation Operators Reference URL: (https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/operators/boolean-logical-operators#logical-negation-operator-)
        // Binary Boolean logical AND Operators Reference URL: (https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/operators/boolean-logical-operators#logical-and-operator-)
        // Binary Boolean logical OR Operators Reference URL: (https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/operators/boolean-logical-operators#logical-or-operator-)
        // Binary Boolean logical Exclusive OR Operators Reference URL: (https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/operators/boolean-logical-operators#logical-exclusive-or-operator-)
        // Binary Boolean Conditional logical AND Operators Reference URL: (https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/operators/boolean-logical-operators#conditional-logical-and-operator-)
        // Binary Boolean Conditional logical OR Operators Reference URL: (https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/operators/boolean-logical-operators#conditional-logical-and-operator-)
        public static readonly string[] BooleanlogicalOperators = "! & | ^ && ||".Split().ToArray();

        public enum CSharp_BooleanlogicalOperators
        {
            negation,
            and,
            or,
            Xor,
            Conditional_and,
            Conditional_or
        }
    }
}
