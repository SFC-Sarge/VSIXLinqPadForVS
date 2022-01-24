namespace VSIXLinqPadForVS.LinqParser
{
    public enum LinqItemType
    {
        Comment,
        Reference,
        RegistryKey,
        String,
        Literal,
        Operator,
        Unknown,
        Entry,
        Property,
        Preprocessor,
        Identifier,
        CSharp_Keywords,
        CSharp_ContextualKeywords,
        Number,
        WhiteSpace,
        Punctuation
    }
}
