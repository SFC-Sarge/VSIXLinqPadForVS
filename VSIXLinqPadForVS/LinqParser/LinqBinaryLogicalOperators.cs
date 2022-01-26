using System.Linq;

namespace VSIXLinqPadForVS.LinqParser
{
    public class LinqBinaryLogicalOperators
    {
        // Binary Logical Operators Reference URL: (https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/operators/bitwise-and-shift-operators)
        // Binary Logical-and-operator Reference URL: (https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/operators/bitwise-and-shift-operators#logical-and-operator-)
        // Binary Logical-or-operator Reference URL: (https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/operators/bitwise-and-shift-operators#logical-or-operator-)
        // Binary Logical-exclusive-or-operator Reference URL: (https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/operators/bitwise-and-shift-operators#logical-exclusive-or-operator-)
        public static readonly string[] BinaryLogicalOperators = "& | ^".Split().ToArray();

        public enum CSharp_BinaryLogicalOperators
        {
            And,
            Or,
            Xor,
        }
    }
}
