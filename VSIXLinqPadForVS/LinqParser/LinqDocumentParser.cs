using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;


namespace VSIXLinqPadForVS.LinqParser
{
    public partial class LinqDocument
    {
        private static readonly Regex _regexProperty = new(@"^(?<name>""[^""]+""|@)(\s)*(?<equals>=)\s*(?<value>((dword:|hex).+|"".+))", RegexOptions.Compiled);
        private static readonly Regex _regexRef = new(@"\$[\w]+\$?", RegexOptions.Compiled);

        public void Parse()
        {
            int start = 0;

            List<LinqParseItem> items = new();

            foreach (string line in _lines)
            {
                IEnumerable<LinqParseItem> current = ParseLine(start, line, items);
                items.AddRange(current);
                start += line.Length;
            }

            Items = items;
        }

        private LinqEntry _currentEntry = null;

        private IEnumerable<LinqParseItem> ParseLine(int start, string line, List<LinqParseItem> tokens)
        {
            string trimmedLine = line.Trim();
            List<LinqParseItem> items = new();

            // Comment
            if (trimmedLine.StartsWith(Constants.CommentChars[0]) || trimmedLine.StartsWith(Constants.CommentChars[1]))
            {
                items.Add(ToParseItem(line, start, LinqItemType.Comment, false));
            }
            // Preprocessor
            else if (trimmedLine.StartsWith("#include"))
            {
                items.Add(ToParseItem(line, start, LinqItemType.Preprocessor, false));
            }
            // Registry key
            else if (trimmedLine.StartsWith("[", StringComparison.Ordinal))
            {
                LinqParseItem key = new LinqParseItem(start, line, this, LinqItemType.RegistryKey);
                _currentEntry = new LinqEntry(key, this);
                items.Add(_currentEntry);
                items.Add(key);
                AddVariableReferences(key);
            }
            // Property
            else if (tokens.Count > 0 && IsMatch(_regexProperty, trimmedLine, out Match matchHeader))
            {
                LinqParseItem name = ToParseItem(matchHeader, start, "name", false);
                LinqParseItem equals = ToParseItem(matchHeader, start, "equals", LinqItemType.Operator, false);
                LinqParseItem value = ToParseItem(matchHeader, start, "value");

                if (_currentEntry != null)
                {
                    LinqProperty prop = new LinqProperty(name, value);
                    _currentEntry.Properties.Add(prop);
                }

                items.Add(name);
                items.Add(equals);
                items.Add(value);
            }
            // Unknown
            else if (trimmedLine.Length > 0)
            {
                // Check for line splits which is a line ending with a backslash
                bool lineSplit = tokens.LastOrDefault()?.Text.TrimEnd().EndsWith("\\") == true;
                LinqItemType type = lineSplit ? tokens.Last().Type : LinqItemType.Unknown;
                items.Add(new LinqParseItem(start, line, this, type));
            }

            return items;
        }

        public static bool IsMatch(Regex regex, string line, out Match match)
        {
            match = regex.Match(line);
            return match.Success;
        }

        private LinqParseItem ToParseItem(string line, int start, LinqItemType type, bool supportsVariableReferences = true)
        {
            LinqParseItem item = new LinqParseItem(start, line, this, type);

            if (supportsVariableReferences)
            {
                AddVariableReferences(item);
            }

            return item;
        }

        private LinqParseItem ToParseItem(Match match, int start, string groupName, LinqItemType type, bool supportsVariableReferences = true)
        {
            Group group = match.Groups[groupName];
            return ToParseItem(group.Value, start + group.Index, type, supportsVariableReferences);
        }

        private LinqParseItem ToParseItem(Match match, int start, string groupName, bool supportsVariableReferences = true)
        {
            Group group = match.Groups[groupName];
            LinqItemType type = group.Value.StartsWith("\"") ? LinqItemType.String : LinqItemType.Literal;
            return ToParseItem(group.Value, start + group.Index, type, supportsVariableReferences);
        }

        private void AddVariableReferences(LinqParseItem token)
        {
            foreach (Match match in _regexRef.Matches(token.Text))
            {
                LinqParseItem reference = ToParseItem(match.Value, token.Span.Start + match.Index, LinqItemType.Reference, false);
                token.References.Add(reference);
            }
        }
    }
}
