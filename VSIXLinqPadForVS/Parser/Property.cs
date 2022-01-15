﻿namespace VSIXLinqPadForVS.Parser
{
    public class Property
    {
        public Property(ParseItem name, ParseItem value)
        {
            Name = name;
            Value = value;
        }

        public ParseItem Name { get; }
        public ParseItem Value { get; }

        public override string ToString()
        {
            return Name.Text + "=" + Value.Text;
        }
    }
}
