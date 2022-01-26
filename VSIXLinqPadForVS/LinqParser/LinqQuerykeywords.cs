﻿using System.Linq;

namespace VSIXLinqPadForVS.LinqParser
{
    public class LinqQuerykeywords
    {
        // Query Keywords reference URL: (https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/keywords/query-keywords)
        public static readonly string[] QueryKeywords = "from where select group into orderby join let in on equals by ascending descending".Split().ToArray();

        public enum CSharp_QueryKeywords
        {
            from,
            where,
            select,
            Group,
            into,
            orderby,
            join,
            let,
            In,
            on,
            equals,
            by,
            ascending,
            descending,
        }
    }
}
