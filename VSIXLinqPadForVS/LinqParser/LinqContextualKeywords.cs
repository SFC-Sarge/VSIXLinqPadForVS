using System.Linq;

namespace VSIXLinqPadForVS.LinqParser
{
    public class LinqContextualKeywords
    {
        // Contentual Keywords Reference URL: (https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/keywords/contextual-keywords)
        public static readonly string[] ContextualKeywords = "add and alias ascending async await descending dynamic equals from get global group into join let on or nameof nint not nuint orderby partial record remove select set value var where with yield".Split().ToArray();

        public enum CSharp_ContextualKeywords
        {
            ContextualKeywords_add,
            ContextualKeywords_and,
            ContextualKeywords_alias,
            ContextualKeywords_ascending,
            ContextualKeywords_async,
            ContextualKeywords_await,
            ContextualKeywords_descending,
            ContextualKeywords_dynamic,
            ContextualKeywords_equals,
            ContextualKeywords_from,
            ContextualKeywords_get,
            ContextualKeywords_global,
            ContextualKeywords_group,
            ContextualKeywords_into,
            ContextualKeywords_join,
            ContextualKeywords_let,
            ContextualKeywords_on,
            ContextualKeywords_or,
            ContextualKeywords_nameof,
            ContextualKeywords_nint,
            ContextualKeywords_not,
            ContextualKeywords_nuint,
            ContextualKeywords_orderby,
            ContextualKeywords_partial,
            ContextualKeywords_record,
            ContextualKeywords_remove,
            ContextualKeywords_select,
            ContextualKeywords_set,
            ContextualKeywords_value,
            ContextualKeywords_var,
            ContextualKeywords_where,
            ContextualKeywords_with,
            ContextualKeywords_yield
        }
    }
}
