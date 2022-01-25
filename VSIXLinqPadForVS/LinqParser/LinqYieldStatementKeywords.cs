using System.Linq;

namespace VSIXLinqPadForVS.LinqParser
{
    public class LinqYieldStatementKeywords
    {
        // Yield Statement Keywords Reference URL: (https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/keywords/yield)
        public static readonly string[] YieldStatementKeywords = "yield".Split().ToArray();

        public enum CSharp_YieldStatementKeywords
        {
            YieldStatementKeyword_yield
        }
    }
}
