﻿using System.Linq;

namespace VSIXLinqPadForVS.LinqParser
{
    public class LinqKeywords
    {
        //CSharp Keywords Reference URL: (https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/keywords/)
        public static readonly string[] Keywords = "abstract as base bool break byte case catch char checked class const continue decimal default delegate do double else enum event explicit extern false finally fixed float for foreach goto if implicit in int interface internal is lock long namespace new null object operator out override params private protected public readonly ref return sbyte sealed short sizeof stackalloc static string struct switch this throw true try typeof uint ulong unchecked unsafe ushort using virtual void volatile while".Split().ToArray();

        public enum CSharp_Keywords
        {
            keyword_abstract,
            keyword_as,
            keyword_base,
            keyword_bool,
            keyword_break,
            keyword_byte,
            keyword_case,
            keyword_catch,
            keyword_char,
            keyword_checked,
            keyword_class,
            keyword_const,
            keyword_continue,
            keyword_decimal,
            keyword_default,
            keyword_delegate,
            keyword_do,
            keyword_double,
            keyword_else,
            keyword_enum,
            keyword_event,
            keyword_explicit,
            keyword_extern,
            keyword_false,
            keyword_finally,
            keyword_fixed,
            keyword_float,
            keyword_for,
            keyword_foreach,
            keyword_goto,
            keyword_if,
            keyword_implicit,
            keyword_in,
            keyword_int,
            keyword_interface,
            keyword_internal,
            keyword_is,
            keyword_lock,
            keyword_long,
            keyword_namespace,
            keyword_new,
            keyword_null,
            keyword_object,
            keyword_operator,
            keyword_out,
            keyword_override,
            keyword_params,
            keyword_private,
            keyword_protected,
            keyword_public,
            keyword_readonly,
            keyword_ref,
            keyword_return,
            keyword_sbyte,
            keyword_sealed,
            keyword_short,
            keyword_sizeof,
            keyword_stackalloc,
            keyword_static,
            keyword_string,
            keyword_struct,
            keyword_switch,
            keyword_this,
            keyword_throw,
            keyword_true,
            keyword_try,
            keyword_typeof,
            keyword_uint,
            keyword_ulong,
            keyword_unchecked,
            keyword_unsafe,
            keyword_ushort,
            keyword_using,
            keyword_virtual,
            keyword_void,
            keyword_volatile,
            keyword_while
        }
    }
}
