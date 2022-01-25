using System.Linq;

namespace VSIXLinqPadForVS.LinqParser
{
    public class LinqMethodParametersKeywords
    {
        // Method Parameters Keywords Reference URL: (https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/keywords/method-parameters)
        // Method Parameters Keywords Reference URL: (https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/keywords/params)
        // Method Parameters Keywords Reference URL: (https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/keywords/in-parameter-modifier)
        // Method Parameters Keywords Reference URL: (https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/keywords/ref)
        // Method Parameters Keywords Reference URL: (https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/keywords/out-parameter-modifier)
        public static readonly string[] MethodParametersKeywords = "params in ref out".Split().ToArray();

        public enum CSharp_MethodParametersKeywords
        {
            MethodParametersKeyword_params,
            MethodParametersKeyword_in,
            MethodParametersKeyword_ref,
            MethodParametersKeyword_out
        }
    }
}
