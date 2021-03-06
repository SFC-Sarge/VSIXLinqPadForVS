namespace VSIXLinqPadForVS
{
    using System;

    internal sealed partial class PackageGuids
    {
        public const string EditorFactoryString = "7f7100ee-7a1b-473f-88df-36bfa8d65a3b";
        public static Guid EditorFactory = new Guid(EditorFactoryString);

        public const string VSIXLinqPadForVSString = "a8059166-5701-4a3b-ab88-bf1a8831dcf1";
        public static Guid VSIXLinqPadForVS = new Guid(VSIXLinqPadForVSString);
    }
    internal sealed partial class PackageIds
    {
        public const int MyCommand = 0x0100;
        public const int TWindowToolbar = 0x1000;
        public const int TWindowToolbarGroup = 0x1050;
        public const int DisplayLinqPadStatementsResults = 0x0111;
        public const int DisplayLinqPadMethodResults = 0x0112;
        public const int EditorLinqPad = 0x0114;
        public const int EditorGroup = 0x0001;
    }
}