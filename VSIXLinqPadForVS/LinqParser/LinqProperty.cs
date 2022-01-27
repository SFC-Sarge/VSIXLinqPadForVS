namespace VSIXLinqPadForVS.LinqParser
{
    public class LinqProperty
    {
        public LinqProperty(LinqParseItem name, LinqParseItem value)
        {
            Name = name;
            Value = value;
        }

        public LinqParseItem Name { get; }
        public LinqParseItem Value { get; }

        public override string ToString()
        {
            return Name.Text + "=" + Value.Text;
        }
    }
}
