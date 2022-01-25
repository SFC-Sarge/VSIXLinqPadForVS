using System.Linq;

namespace VSIXLinqPadForVS.LinqParser
{
    public class LinqBooleanlogicalOperatorsKeywords
    {
        // Boolean logical Arithmetic Operators Keywords Reference URL: (https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/operators/arithmetic-operators)
        // Unary Boolean logical Negation Operators Keywords Reference URL: (https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/operators/boolean-logical-operators#logical-negation-operator-)
        // Binary Boolean logical AND Operators Keywords Reference URL: (https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/operators/boolean-logical-operators#logical-and-operator-)
        // Binary Boolean logical OR Operators Keywords Reference URL: (https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/operators/boolean-logical-operators#logical-or-operator-)
        // Binary Boolean logical Exclusive OR Operators Keywords Reference URL: (https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/operators/boolean-logical-operators#logical-exclusive-or-operator-)
        // Binary Boolean Conditional logical AND Operators Keywords Reference URL: (https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/operators/boolean-logical-operators#conditional-logical-and-operator-)
        // Binary Boolean Conditional logical OR Operators Keywords Reference URL: (https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/operators/boolean-logical-operators#conditional-logical-and-operator-)
        public static readonly string[] BooleanlogicalOperatorsKeywords = "! & | ^ && ||".Split().ToArray();

        public enum CSharp_BooleanlogicalOperatorsKeywords
        {
            BooleanlogicalOperatorsKeyword_Negation,
            BooleanlogicalOperatorsKeyword_And,
            BooleanlogicalOperatorsKeyword_Or,
            BooleanlogicalOperatorsKeyword_Exclusive_Or,
            BooleanlogicalOperatorsKeyword_Conditional_And,
            BooleanlogicalOperatorsKeyword_Conditional_Or
        }
    }
}
