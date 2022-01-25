using System.Linq;

namespace VSIXLinqPadForVS.LinqParser
{
    public class LinqExternAliasKeywords
    {
        // Extern Alias Keywords Reference URL: (https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/keywords/extern-alias)
        public static readonly string[] UsingStatementKeywords = "extern alias".Split(',').ToArray();

        public enum CSharp_UsingStatementKeywords
        {
            UsingStatementKeyword_extern_alias
        }
    }
}
