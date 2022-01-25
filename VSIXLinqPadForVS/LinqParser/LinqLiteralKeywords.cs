using System.Linq;

namespace VSIXLinqPadForVS.LinqParser
{
    public class LinqLiteralKeywords
    {
        // Literal null Keyword Reference URL: (https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/keywords/null)
        //Literal bool Keyword Reference URL: (https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/builtin-types/bool)
        //Literal default Keyword Reference URL: (https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/keywords/default)
        public static readonly string[] LiteralKeywords = "null bool default".Split().ToArray();

        public enum CSharp_LiteralKeywords
        {
            LiteralKeyword_null,
            LiteralKeyword_bool,
            LiteralKeyword_default
        }
    }
}
