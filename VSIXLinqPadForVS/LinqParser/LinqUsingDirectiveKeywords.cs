using System.Linq;

namespace VSIXLinqPadForVS.LinqParser
{
    public class LinqUsingDirectiveKeywords
    {
        // Using Directive Keywords Reference URL: (https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/keywords/using-directive)
        public static readonly string[] UsingDirectiveKeywords = "using".Split().ToArray();

        public enum CSharp_UsingDirectiveKeywords
        {
            Using
        }
    }
}
