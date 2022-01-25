using System.Linq;

namespace VSIXLinqPadForVS.LinqParser
{
    public class LinqNamespaceKeywords
    {
        // Namespace Keywords Reference URL: (https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/keywords/namespace)
        public static readonly string[] NamespaceKeywords = "namespace".Split().ToArray();

        public enum CSharp_NamespaceKeywords
        {
            NamespaceKeyword_namespace
        }
    }
}
