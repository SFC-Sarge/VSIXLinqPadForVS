using System.Linq;

namespace VSIXLinqPadForVS.LinqParser
{
    public class LinqXMLDocumentationCommentTags
    {
        public static readonly string[] XMLDocumentationComments = "<summary> <remarks> <returns> <param> <paramref> <exception> <value> <para> <list> <listheader> <term> <description> <item> <c> <code> <example> <inheritdoc> <see> <seealso> <typeparam> <typeparamref> <include>".Split().ToArray();

        public enum CSharp_XMLDocumentationComments
        {
            summary,
            remarks,
            returns,
            param,
            paramref,
            exception,
            value,
            para,
            list,
            listheader,
            term,
            description,
            item,
            c,
            code,
            example,
            inheritdoc,
            see,
            seealso,
            cref,
            href,
            typeparam,
            typeparamref,
            include,
            Event,
            note
        }
    }
}
