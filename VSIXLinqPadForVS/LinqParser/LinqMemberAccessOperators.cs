using System.Linq;

namespace VSIXLinqPadForVS.LinqParser
{
    public class LinqMemberAccessOperators
    {
        // Member-access-operators Reference URL: (https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/operators/member-access-operators)
        // Member-access-expression . Reference URL: (https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/operators/member-access-operators#member-access-expression-)
        // Indexer-operator [] Reference URL: (https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/operators/member-access-operators#indexer-operator-)
        // Null-conditional-operator ?. Reference URL: (https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/operators/member-access-operators#null-conditional-operators--and-)
        // Null-conditional-operator ?[] Reference URL: (https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/operators/member-access-operators#null-conditional-operators--and-)
        // Invocation-expression () Reference URL: (https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/operators/member-access-operators#invocation-expression-)
        // index-from-end-operator ^ Reference URL: (https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/operators/member-access-operators#index-from-end-operator-)
        // Range-operator .. Reference URL: (https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/operators/member-access-operators#range-operator-)
        public static readonly string[] MemberAccessOperators = ". [] ?. ?[] () ^ ..".Split().ToArray();

        public enum CSharp_MemberAccessOperators
        {
            MemberAccessExpression,
            Indexer,
            NullConditionalQMarkPeriod,
            NullConditionalQMarkIndexer,
            InvocationExpression,
            IndexFromEnd,
            Range


        }
    }
}
