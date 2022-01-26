using System.Linq;

namespace VSIXLinqPadForVS.LinqParser
{
    public class LinqLambdaExpressions
    {
        //Lambda Expressions => Reference URL: (https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/operators/lambda-expressions)
        // Expression lambda Reference URL: (https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/operators/lambda-expressions#expression-lambdas)
        // Statement lambda Reference URL: (https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/operators/lambda-expressions#statement-lambdas)
        public static readonly string[] LambdaExpressions = "=> !=".Split().ToArray();

        public enum CSharp_LambdaExpressions
        {
            LambdaExpression,
            LambdaStatement
        }
    }
}
