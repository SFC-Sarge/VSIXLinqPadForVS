using System.Linq;

namespace VSIXLinqPadForVS.LinqParser
{
    public class LinqJumpStatementsKeywords
    {
        // Jump Statement Keywords Reference URL: (https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/statements/jump-statements)
        public static readonly string[] JumpStatementKeywords = "break continue goto return".Split().ToArray();

        public enum CSharp_JumpStatementKeywords
        {
            Break,
            Continue,
            Goto,
            Return
        }
    }
}
