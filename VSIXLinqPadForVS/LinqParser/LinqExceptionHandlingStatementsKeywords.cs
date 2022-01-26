using System.Linq;

namespace VSIXLinqPadForVS.LinqParser
{
    public class LinqExceptionHandlingStatementsKeywords
    {
        // Exception Handling Statement Keywords Reference URL: (https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/keywords/throw)
        // Exception Handling Statement Keywords Reference URL: (https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/keywords/try-catch)
        // Exception Handling Statement Keywords Reference URL: (https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/keywords/try-finally)
        // Exception Handling Statement Keywords Reference URL: (https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/keywords/try-catch-finally)
        public static readonly string[] ExceptionHandlingStatementsKeywords = "throw try-catch try-finally try-catch-finally".Split().ToArray();

        public enum CSharp_ExceptionHandlingStatementsKeywords
        {
            Throw,
            Try_catch,
            Try_finally,
            Try_catch_finally
        }
    }
}
