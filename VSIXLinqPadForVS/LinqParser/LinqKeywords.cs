﻿using System.Linq;

namespace VSIXLinqPadForVS.LinqParser
{
    public class LinqKeywords
    {
        //CSharp Keywords Reference URL: (https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/keywords/)
        public static readonly string[] Keywords = "abstract as base bool break by byte case catch char checked class const continue decimal default delegate do double descending else enum event explicit extern false finally fixed float for foreach from goto group if implicit in int interface internal into is lock long new null namespace object operator out override orderby params private protected public readonly ref return switch struct sbyte sealed select short sizeof stackalloc static string struct this throw true try typeof uint ulong unchecked unsafe ushort using var virtual void volatile while where yield".Split().ToArray();
    }
}
