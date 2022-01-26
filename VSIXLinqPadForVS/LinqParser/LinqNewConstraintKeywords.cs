using System.Linq;

namespace VSIXLinqPadForVS.LinqParser
{
    public class LinqNewConstraintKeywords
    {
        // New Constraint Keywords Reference URL: (https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/keywords/new-constraint)
        public static readonly string[] NewConstraintKeywords = "new".Split().ToArray();

        public enum CSharp_NewConstraintKeywords
        {
            New
        }
    }
}
