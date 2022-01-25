using System.Linq;

namespace VSIXLinqPadForVS.LinqParser
{
    public class LinqQuerykeywords
    {
        // Query Keywords reference URL: (https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/keywords/query-keywords)
        public static readonly string[] QueryKeywords = "from where select group into orderby join let in on equals by ascending descending".Split().ToArray();

        public enum CSharp_QueryKeywords
        {
            QueryKeywords_from,
            QueryKeywords_where,
            QueryKeywords_select,
            QueryKeywords_group,
            QueryKeywords_into,
            QueryKeywords_orderby,
            QueryKeywords_join,
            QueryKeywords_let,
            QueryKeywords_in,
            QueryKeywords_on,
            QueryKeywords_equals,
            QueryKeywords_by,
            QueryKeywords_ascending,
            QueryKeywords_descending,
        }
    }
}
