using System.Linq;

namespace VSIXLinqPadForVS.LinqParser
{
    public class LinqBinaryArithmeticOperators
    {
        // Binary Arithmetic Operators Reference URL: (https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/operators/arithmetic-operators)
        // Binary Multiplication Arithmetic Operators Reference URL: (https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/operators/arithmetic-operators#multiplication-operator-)
        // Binary Division Arithmetic Operators Reference URL: (https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/operators/arithmetic-operators#division-operator-)
        // Binary Remainder Arithmetic Operators Reference URL: (https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/operators/arithmetic-operators#remainder-operator-)
        // Binary Addition Arithmetic Operators Reference URL: (https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/operators/arithmetic-operators#addition-operator-)
        // Binary Subtraction Arithmetic Operators Reference URL: (https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/operators/arithmetic-operators#subtraction-operator--)
        public static readonly string[] BinaryArithmeticOperators = "* / % + -".Split().ToArray();

        public enum CSharp_BinaryArithmeticOperators
        {
            Multiplication,
            Division,
            Remainder,
            Addition,
            Subtraction
        }
    }
}
