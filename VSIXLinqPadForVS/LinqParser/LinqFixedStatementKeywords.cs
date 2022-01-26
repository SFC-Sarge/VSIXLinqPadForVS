using System.Linq;

namespace VSIXLinqPadForVS.LinqParser
{
    public class LinqFixedStatementKeywords
    {
        // Fixed Statement Keywords Reference URL: (https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/keywords/fixed-statement)
        public static readonly string[] FixedStatementKeywords = "fixed".Split().ToArray();

        public enum CSharp_FixedStatementKeywords
        {
            Fixed
        }
    }
}
