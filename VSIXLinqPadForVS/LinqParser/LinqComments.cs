using System.Linq;

namespace VSIXLinqPadForVS.LinqParser
{
    public class LinqComments
    {
        public static readonly string[] Comments = "/// // /* */".Split().ToArray();
    }
}
