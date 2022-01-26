using System.Linq;

namespace VSIXLinqPadForVS.LinqParser
{
    public class LinqContextualKeywords
    {
        // Contentual Keywords Reference URL: (https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/keywords/contextual-keywords)
        public static readonly string[] ContextualKeywords = "add and alias ascending async await descending dynamic equals from get global group into join let on or nameof nint not nuint orderby partial record remove select set value var where with yield".Split().ToArray();

        public enum CSharp_ContextualKeywords
        {
            add,
            and,
            alias,
            ascending,
            async,
            await,
            descending,
            dynamic,
            equals,
            from,
            get,
            global,
            group,
            into,
            join,
            let,
            on,
            or,
            nameof,
            nint,
            not,
            nuint,
            orderby,
            partial,
            record,
            remove,
            select,
            set,
            value,
            var,
            where,
            with,
            yield
        }
    }
}
