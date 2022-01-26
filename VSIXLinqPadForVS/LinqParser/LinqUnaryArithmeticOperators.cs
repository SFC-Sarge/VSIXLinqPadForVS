using System.Linq;

namespace VSIXLinqPadForVS.LinqParser
{
    public class LinqUnaryArithmeticOperators
    {
        // Unary Arithmetic Operators Reference URL: (https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/operators/arithmetic-operators)
        // Unary Increment Arithmetic Operators Reference URL: (https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/operators/arithmetic-operators#increment-operator-)
        // Unary Decrement Arithmetic Operators Reference URL: (https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/operators/arithmetic-operators#decrement-operator---)
        // Unary Plus and Minus Arithmetic Operators Reference URL: (https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/operators/arithmetic-operators#unary-plus-and-minus-operators)
        public static readonly string[] UnaryArithmeticOperators = "++ -- + -".Split().ToArray();

        public enum CSharp_UnaryArithmeticOperators
        {
            Increment,
            Decrement,
            Plus,
            Minus
        }
    }
}
