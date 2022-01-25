using System.Linq;

namespace VSIXLinqPadForVS.LinqParser
{
    public class LinqUnaryArithmeticOperatorsKeywords
    {
        // Unary Arithmetic Operators Keywords Reference URL: (https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/operators/arithmetic-operators)
        // Unary Increment Arithmetic Operators Keywords Reference URL: (https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/operators/arithmetic-operators#increment-operator-)
        // Unary Decrement Arithmetic Operators Keywords Reference URL: (https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/operators/arithmetic-operators#decrement-operator---)
        // Unary Plus and Minus Arithmetic Operators Keywords Reference URL: (https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/operators/arithmetic-operators#unary-plus-and-minus-operators)
        public static readonly string[] UnaryArithmeticOperatorsKeywords = "++ -- + -".Split().ToArray();

        public enum CSharp_UnaryArithmeticOperatorsKeywords
        {
            UnaryArithmeticOperatorsKeyword_Increment,
            UnaryArithmeticOperatorsKeyword_Decrement,
            UnaryArithmeticOperatorsKeyword_Plus,
            UnaryArithmeticOperatorsKeyword_Minus
        }
    }
}
