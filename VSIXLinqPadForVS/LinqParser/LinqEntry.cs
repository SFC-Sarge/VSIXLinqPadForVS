using Microsoft.VisualStudio.Text;

using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VSIXLinqPadForVS.LinqParser
{
    public class LinqEntry : LinqParseItem
    {
        public LinqEntry(LinqParseItem registryKey, LinqDocument document)
            : base(registryKey.Span.Start, registryKey.Text, document, LinqItemType.Entry)
        {
            RegistryKey = registryKey;
        }
        public LinqParseItem RegistryKey { get; }
        public List<LinqProperty> Properties { get; } = new();

        public override Span Span
        {
            get
            {
                int end = Properties.Any() ? Properties.Last().Value.Span.End : RegistryKey.Span.End;
                return Span.FromBounds(RegistryKey.Span.Start, end);
            }
        }

        public string GetFormattedText()
        {
            StringBuilder sb = new();

            if (RegistryKey != null)
            {
                sb.AppendLine(RegistryKey.Text.Trim());
                foreach (LinqProperty property in Properties)
                {
                    sb.AppendLine($"{property.Name.Text.Trim()}={property.Value.Text.Trim()}");
                }
            }

            return sb.ToString().Trim();
        }
    }
}
