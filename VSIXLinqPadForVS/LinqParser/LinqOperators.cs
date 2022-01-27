using System.Linq;

namespace VSIXLinqPadForVS.LinqParser
{
    public class LinqOperators
    {
        public static readonly string[] Operators = "+ - * / % & ( ) [ ] | ^ ! ~ && || , ++ -- << >> == != < > <= >= = += -= *= /= %= &= |= ^= <<= >>= . [] () ?: => ??".Split().ToArray();

    }
}
