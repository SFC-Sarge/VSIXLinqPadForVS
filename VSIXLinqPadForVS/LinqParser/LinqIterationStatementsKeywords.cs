using System.Linq;

namespace VSIXLinqPadForVS.LinqParser
{
    public class LinqIterationStatementsKeywords
    {
        // Iteration Statement Keywords Reference URL: (https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/statements/iteration-statements)
        public static readonly string[] IterationStatementKeywords = "do for foreach while".Split().ToArray();

        public enum CSharp_IterationStatementKeywords
        {
            IterationStatementKeyword_do,
            IterationStatementKeyword_for,
            IterationStatementKeyword_foreach,
            IterationStatementKeyword_while
        }
    }
}
