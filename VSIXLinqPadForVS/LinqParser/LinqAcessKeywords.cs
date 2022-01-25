using System.Linq;

namespace VSIXLinqPadForVS.LinqParser
{
    public class LinqAcessKeywords
    {
        // Acess Keywords Reference URL: (https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/keywords/base)
        // Acess Keywords Reference URL: (https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/keywords/this)
        public static readonly string[] AcessKeywords = "base this".Split().ToArray();

        public enum CSharp_AcessKeywords
        {
            AcessKeyword_base,
            AcessKeyword_this
        }
    }
}
