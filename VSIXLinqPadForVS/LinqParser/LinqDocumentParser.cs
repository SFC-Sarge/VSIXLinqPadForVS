using Microsoft.VisualStudio.TextManager.Interop;

using System.Collections.Generic;
using System.Text.RegularExpressions;


namespace VSIXLinqPadForVS.LinqParser
{
    public partial class LinqDocument : IVsColorizer
    {
        private static readonly Regex _regexProperty = new(@"^(?<name>""[^""]+""|@)(\s)*(?<equals>=)\s*(?<value>((dword:|hex).+|"".+))", RegexOptions.Compiled);
        private static readonly Regex _regexRef = new(@"\$[\w]+\$?", RegexOptions.Compiled);

        public void Parse()
        {
            int start = 0;

            List<LinqParseItem> items = new();
            LinqLexicalParser analyzer = new LinqLexicalParser();
            foreach (string line in _lines)
            {
                string text = line;
                while (text != null)
                {
                    text = text.Trim(' ', '\t');
                    string token = analyzer.GetNextLexicalAtom(ref text);
                    string currentValue = token;
                    LinqItemType currentType = LinqItemType.Unknown;
                    switch (currentValue)
                    {
                        case var token1 when (token).StartsWith("NumericalConstant"):
                            token = token.Substring("NumericalConstant".Length, (token.Length - "NumericalConstant".Length));
                            currentType = LinqItemType.Number;
                            break;
                        case var token1 when (token).StartsWith("Keyword"):
                            token = token.Substring("Keyword".Length, (token.Length - "Keyword".Length));
                            currentType = LinqItemType.Keywords;
                            break;
                        case var token1 when (token).StartsWith("Operator"):
                            token = token.Substring("Operator".Length, (token.Length - "Operator".Length));
                            currentType = LinqItemType.Operator;
                            break;
                        case var token1 when (token).StartsWith("Separator"):
                            token = token.Substring("Separator".Length, (token.Length - "Separator".Length));
                            currentType = LinqItemType.Preprocessor;
                            break;
                        case var token1 when (token).StartsWith("Identifier"):
                            token = token.Substring("Identifier".Length, (token.Length - "Identifier".Length));
                            currentType = LinqItemType.Identifier;
                            break;
                    }

                    IEnumerable<LinqParseItem> current = ParseLine(start, token, currentType, items);
                    items.AddRange(current);
                    start += token.Length;
                    var result = text.Length;
                    if (text == "") return;
                }
            }

            Items = items;
        }

        private LinqEntry _currentEntry = null;

        private IEnumerable<LinqParseItem> ParseLine(int start, string line, LinqItemType type, List<LinqParseItem> tokens)
        {
            string trimmedLine = line.Trim();
            List<LinqParseItem> items = new();
            items.Add(ToParseItem(line, start, type, false));

            //// Comment
            //if (trimmedLine.StartsWith(Constants.CommentChars[0]) || trimmedLine.StartsWith(Constants.CommentChars[1]))
            //{
            //    items.Add(ToParseItem(line, start, LinqItemType.Comment, false));
            //}
            //// Identifier
            //else if (trimmedLine.StartsWith("<Query Kind="))
            //{
            //    items.Add(ToParseItem(line, start, LinqItemType.Reference, false));
            //}
            //// CSharp_Keywords
            //else if (trimmedLine.StartsWith("using"))
            //{
            //    items.Add(ToParseItem(line, start, LinqItemType.Keywords, false));
            //}
            //else if (trimmedLine.StartsWith("namespace"))
            //{
            //    items.Add(ToParseItem(line, start, LinqItemType.Keywords, false));
            //}
            //else if (trimmedLine.StartsWith("public partial class"))
            //{
            //    // Reference URL for Access Modifiers: ()
            //    items.Add(ToParseItem(line, start, LinqItemType.Keywords, false));
            //}
            //else if (trimmedLine.StartsWith("class") || trimmedLine.StartsWith("abstract class"))
            //{
            //    // Reference URL for abstract class modifier: (https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/keywords/abstract)
            //    items.Add(ToParseItem(line, start, LinqItemType.Keywords, false));
            //}
            //else if (trimmedLine.StartsWith("const") || trimmedLine.StartsWith("public const"))
            //{
            //    // Reference URL for Access Modifiers: (https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/keywords/const)
            //    items.Add(ToParseItem(line, start, LinqItemType.Keywords, false));
            //}
            //else if (trimmedLine.StartsWith("struct") || trimmedLine.StartsWith("enum"))
            //{
            //    items.Add(ToParseItem(line, start, LinqItemType.Keywords, false));
            //}
            //else if (trimmedLine.StartsWith("public async") || trimmedLine.StartsWith("private async"))
            //{
            //    // Reference URL for Access Modifiers: (https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/keywords/async)
            //    items.Add(ToParseItem(line, start, LinqItemType.Keywords, false));
            //}
            //else if (trimmedLine.StartsWith("public event") || trimmedLine.StartsWith("private event") || trimmedLine.StartsWith("private protected event") || trimmedLine.StartsWith("internal event") || trimmedLine.StartsWith("protected internal event") || trimmedLine.StartsWith("protected event"))
            //{
            //    // Reference URL for Access Modifiers: (https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/keywords/event)
            //    items.Add(ToParseItem(line, start, LinqItemType.Keywords, false));
            //}
            //else if (trimmedLine.StartsWith("public") || trimmedLine.StartsWith("private") || trimmedLine.StartsWith("private protected") || trimmedLine.StartsWith("internal") || trimmedLine.StartsWith("protected internal") || trimmedLine.StartsWith("protected") || trimmedLine.StartsWith("public abstract"))
            //{
            //    // Reference URL for Access Modifiers: (https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/keywords/access-modifiers)
            //    items.Add(ToParseItem(line, start, LinqItemType.Keywords, false));
            //}
            //else if (trimmedLine.StartsWith("static"))
            //{
            //    items.Add(ToParseItem(line, start, LinqItemType.Keywords, false));
            //}
            //else if (trimmedLine.StartsWith("void"))
            //{
            //    items.Add(ToParseItem(line, start, LinqItemType.Keywords, false));
            //}
            //else if (trimmedLine.StartsWith("foreach"))
            //{
            //    items.Add(ToParseItem(line, start, LinqItemType.Keywords, false));
            //}
            //else if (trimmedLine.StartsWith("IEnumerable"))
            //{
            //    items.Add(ToParseItem(line, start, LinqItemType.Keywords, false));
            //}
            //else if (trimmedLine.StartsWith("Console") || trimmedLine.StartsWith("Debug"))
            //{
            //    items.Add(ToParseItem(line, start, LinqItemType.Number, false));
            //}
            //else if (trimmedLine.StartsWith("var"))
            //{
            //    items.Add(ToParseItem(line, start, LinqItemType.Keywords, false));
            //    //LinqParseItem variable = new LinqParseItem(start, line, this, LinqItemType.variable);
            //    //_currentEntry = new LinqVariableEntry(variable, this);
            //    //items.Add(_currentEntry);
            //    ////items.Add(key);
            //    ////AddVariableReferences(key);
            //}
            //else if (trimmedLine.StartsWith("string"))
            //{
            //    items.Add(ToParseItem(line, start, LinqItemType.Keywords, false));
            //}
            //else if (trimmedLine.StartsWith("int"))
            //{
            //    items.Add(ToParseItem(line, start, LinqItemType.Keywords, false));
            //}
            //else if (trimmedLine.StartsWith("double"))
            //{
            //    items.Add(ToParseItem(line, start, LinqItemType.Keywords, false));
            //}
            //else if (trimmedLine.StartsWith("Enumerable"))
            //{
            //    items.Add(ToParseItem(line, start, LinqItemType.Literal, false));
            //}
            //Punctuation
            //if (trimmedLine.StartsWith("{"))
            //{
            //    items.Add(ToParseItem(line, start, LinqItemType.Punctuation, false));
            //}
            //else if (trimmedLine.StartsWith("}"))
            //{
            //    items.Add(ToParseItem(line, start, LinqItemType.Punctuation, false));
            //}
            //else if (trimmedLine.Contains("("))
            //{
            //    items.Add(ToParseItem(line, start, LinqItemType.Punctuation, false));
            //}
            //else if (trimmedLine.Contains(")"))
            //{
            //    items.Add(ToParseItem(line, start, LinqItemType.Punctuation, false));
            //}
            //// Registry key
            //else if (trimmedLine.StartsWith("["))
            //{
            //    items.Add(ToParseItem(line, start, LinqItemType.Punctuation, false));
            //    //LinqParseItem key = new LinqParseItem(start, line, this, LinqItemType.RegistryKey);
            //    //_currentEntry = new LinqEntry(key, this);
            //    //items.Add(_currentEntry);
            //    //items.Add(key);
            //    //AddVariableReferences(key);
            //}
            //else if (trimmedLine.StartsWith("]"))
            //{
            //    items.Add(ToParseItem(line, start, LinqItemType.Punctuation, false));
            //    //LinqParseItem key = new LinqParseItem(start, line, this, LinqItemType.RegistryKey);
            //    //_currentEntry = new LinqEntry(key, this);
            //    //items.Add(_currentEntry);
            //    //items.Add(key);
            //    //AddVariableReferences(key);
            //}
            //// Property
            //else if (tokens.Count > 0 && IsMatch(_regexProperty, trimmedLine, out Match matchHeader))
            //{
            //    LinqParseItem name = ToParseItem(matchHeader, start, "name", false);
            //    LinqParseItem equals = ToParseItem(matchHeader, start, "equals", LinqItemType.Operator, false);
            //    LinqParseItem value = ToParseItem(matchHeader, start, "value");

            //    if (_currentEntry != null)
            //    {
            //        LinqProperty prop = new LinqProperty(name, value);
            //        _currentEntry.Properties.Add(prop);
            //    }

            //    items.Add(name);
            //    items.Add(equals);
            //    items.Add(value);
            //}
            //else if (trimmedLine.Length > 0 && trimmedLine.EndsWith(";", StringComparison.Ordinal))
            //{
            //    items.Add(ToParseItem(line, start, LinqItemType.Preprocessor, false));
            //}
            //// Unknown
            //else if (trimmedLine.Length > 0)
            //{
            //    // Check for line splits which is a line ending with a backslash
            //    bool lineSplit = tokens.LastOrDefault()?.Text.TrimEnd().EndsWith("\\") == true;
            //    type = lineSplit ? tokens.Last().Type : LinqItemType.Unknown;
            //    items.Add(new LinqParseItem(start, line, this, type));
            //}
            //else
            //{
            //    items.Add(ToParseItem(line, start, type, false));

            //}

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

        public int GetStateMaintenanceFlag(out int pfFlag)
        {
            throw new NotImplementedException();
        }

        public int GetStartState(out int piStartState)
        {
            throw new NotImplementedException();
        }

        public int ColorizeLine(int iLine, int iLength, IntPtr pszText, int iState, uint[] pAttributes)
        {
            throw new NotImplementedException();
        }

        public int GetStateAtEndOfLine(int iLine, int iLength, IntPtr pText, int iState)
        {
            throw new NotImplementedException();
        }

        public void CloseColorizer()
        {
            throw new NotImplementedException();
        }
    }
}
