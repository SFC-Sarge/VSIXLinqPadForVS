using System.Linq;

namespace VSIXLinqPadForVS.LinqParser
{
    public class UnaryBitwiseComplementOperators
    {
        // Bitwise-and-shift-operators Reference URL: (https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/operators/bitwise-and-shift-operators)
        // Unary Bitwise Complement Operators Reference URL: (https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/operators/bitwise-and-shift-operators#bitwise-complement-operator-)
        public static readonly string[] BitwiseComplementOperators = "~".Split().ToArray();

        public enum CSharp_BitwiseComplementOperators
        {
            Complement
        }
    }
}
