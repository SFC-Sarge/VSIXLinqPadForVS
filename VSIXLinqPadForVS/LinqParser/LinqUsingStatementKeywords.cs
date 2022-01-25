using System.Linq;

namespace VSIXLinqPadForVS.LinqParser
{
    public class LinqUsingStatementKeywords
    {
        // Using Statement Keywords Reference URL: (https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/keywords/using-statement)
        public static readonly string[] UsingStatementKeywords = "using".Split().ToArray();

        public enum CSharp_UsingStatementKeywords
        {
            UsingStatementKeyword_using
        }
    }
}
