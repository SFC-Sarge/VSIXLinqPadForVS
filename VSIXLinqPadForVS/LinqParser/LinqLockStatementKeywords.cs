using System.Linq;

namespace VSIXLinqPadForVS.LinqParser
{
    public class LinqLockStatementKeywords
    {
        // Lock Statement Keywords Reference URL: (https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/statements/lock)
        public static readonly string[] LockStatementKeywords = "lock".Split().ToArray();

        public enum CSharp_LockStatementKeywords
        {
            Lock
        }
    }
}
