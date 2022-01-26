using System.Linq;

namespace VSIXLinqPadForVS.LinqParser
{
    public class BinaryShiftOperators
    {
        // Boolean logical Arithmetic Operators Reference URL: (https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/operators/bitwise-and-shift-operators)
        // Binary Left-shift-operator Reference URL: (https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/operators/bitwise-and-shift-operators#left-shift-operator-)
        // Binary Right-shift-operator Reference URL: (https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/operators/bitwise-and-shift-operators#right-shift-operator-)
        public static readonly string[] ShiftOperator = "<< >>".Split().ToArray();

        public enum CSharp_ShiftOperators
        {
            LeftShift,
            RightShift
        }
    }
}
