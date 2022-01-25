using System.Linq;

namespace VSIXLinqPadForVS.LinqParser
{
    public class LinqCheckedUncheckedKeywords
    {
        // Checked and Unchecked Keywords Reference URL: (https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/keywords/checked-and-unchecked)
        public static readonly string[] CheckedUncheckedKeywords = "checked unchecked".Split().ToArray();

        public enum CSharp_CheckedUncheckedKeywords
        {
            CheckedUncheckedKeyword_checked,
            CheckedUncheckedKeyword_unchecked
        }
    }
}
