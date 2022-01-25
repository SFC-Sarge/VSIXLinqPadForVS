using System.Linq;

namespace VSIXLinqPadForVS.LinqParser
{
    public class LinqBinaryArithmeticOperatorsKeywords
    {
        // Binary Arithmetic Operators Keywords Reference URL: (https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/operators/arithmetic-operators)
        // Binary Multiplication Arithmetic Operators Keywords Reference URL: (https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/operators/arithmetic-operators#multiplication-operator-)
        // Binary Division Arithmetic Operators Keywords Reference URL: (https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/operators/arithmetic-operators#division-operator-)
        // Binary Remainder Arithmetic Operators Keywords Reference URL: (https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/operators/arithmetic-operators#remainder-operator-)
        // Binary Addition Arithmetic Operators Keywords Reference URL: (https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/operators/arithmetic-operators#addition-operator-)
        // Binary Subtraction Arithmetic Operators Keywords Reference URL: (https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/operators/arithmetic-operators#subtraction-operator--)
        public static readonly string[] BinaryArithmeticOperatorsKeywords = "* / % + -".Split().ToArray();

        public enum CSharp_BinaryArithmeticOperatorsKeywords
        {
            BinaryArithmeticOperatorsKeyword_Multiplication,
            BinaryArithmeticOperatorsKeyword_Division,
            BinaryArithmeticOperatorsKeyword_Remainder,
            BinaryArithmeticOperatorsKeyword_Addition,
            BinaryArithmeticOperatorsKeyword_Subtraction
        }
    }
}
