using Microsoft.VisualStudio.Text;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VSIXLinqPadForVS.Parser
{
    public class Entry : ParseItem
    {
        public Entry(ParseItem registryKey, Document document)
            : base(registryKey.Span.Start, registryKey.Text, document, ItemType.Entry)
        {
            RegistryKey = registryKey;
        }

        public ParseItem RegistryKey { get; }
        public List<Property> Properties { get; } = new();

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
            StringBuilder sb = new StringBuilder();

            if (RegistryKey != null)
            {
                sb.AppendLine(RegistryKey.Text.Trim());
                foreach (Property property in Properties)
                {
                    sb.AppendLine($"{property.Name.Text.Trim()}={property.Value.Text.Trim()}");
                }
            }

            return sb.ToString().Trim();
        }
    }
}
