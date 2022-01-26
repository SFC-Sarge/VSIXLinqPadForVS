using System.Linq;

namespace VSIXLinqPadForVS.LinqParser
{
    public class LinqPointerRelatedOperators
    {
        // Type-testing-and-cast Operators Reference URL: (https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/operators/pointer-related-operators)
        // Unary & (address-of) Operator Reference URL: (https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/operators/pointer-related-operators#address-of-operator-)
        // Unary * (pointer indirection) Operator Reference URL: (https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/operators/pointer-related-operators#pointer-indirection-operator-)
        // The -> (member access) Operator Reference URL: (https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/operators/pointer-related-operators#pointer-member-access-operator--)
        // The [] (element access) Operator Reference URL: (https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/operators/pointer-related-operators#pointer-element-access-operator-)
        // Arithmetic Operators +, -, ++, and -- Reference URL: (https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/operators/arithmetic-operators)
        // Comparison Operators ==, !=, <, >, <=, and >= Reference URL: (https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/operators/comparison-operators)
        public static readonly string[] PointerRelatedOperators = "& * -> [] + - ++ -- == != < > <= >=".Split().ToArray();

        public enum CSharp_PointerRelatedOperators
        {
            AddressOf,
            PointerIndirection,
            MemberAccess,
            ElementAccess,
            Plus,
            Minus,
            Increment,
            Decrement,
            Equality,
            Inequality,
            LessThan,
            GreaterThan,
            LessThanOrEqual,
            GreaterThanOrEqual

        }
    }
}
