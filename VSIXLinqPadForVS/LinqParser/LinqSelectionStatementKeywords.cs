using System.Linq;

namespace VSIXLinqPadForVS.LinqParser
{
    public class LinqSelectionStatementKeywords
    {
        // Selection Statement Keywords Reference URL: (https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/statements/selection-statements)
        public static readonly string[] SelectionStatementKeywords = "if switch".Split().ToArray();

        public enum CSharp_SelectionStatementKeywords
        {
            SelectStatementKeyword_if,
            SelectStatementKeyword_switch
        }
    }
}
