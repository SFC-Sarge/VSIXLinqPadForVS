using System.Linq;

namespace VSIXLinqPadForVS.LinqParser
{
    public class LinqAssignmentOperators
    {
        // Assignment Operators Reference URL: (https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/operators/assignment-operator)
        // The assignment Operator = Reference URL: (https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/operators/assignment-operator)
        // ref local assignment operator Reference URL: (https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/keywords/ref#ref-locals)
        // ref readonly local assignment operator Reference URL: (https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/keywords/ref#ref-readonly-locals)
        // Compound assignment Operator op Reference URL: (https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/operators/assignment-operator#compound-assignment)
        // Null-coalescing assignment Operator ??= Reference URL: (https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/operators/assignment-operator#null-coalescing-assignment)
        public static readonly string[] AssignmentOperators = "= ref ref readonly op ??=".Split().ToArray();

        public enum CSharp_AssignmentOperators
        {
            Equals,
            Ref,
            RefReadonly,
            Op,
            NullCoalescing
        }
    }
}
