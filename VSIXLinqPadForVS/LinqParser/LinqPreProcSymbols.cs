namespace VSIXLinqPadForVS.LinqParser
{
    public class LinqPreProcSymbols
    {
        //private static readonly string[] PreProcSymbols = new string[3] { "PreProc_LINQPAD,", "PreProc_NETCORE,", "PreProc_DEBUG" };
        public static readonly string[] PreProcSymbols = new string[3] { "LINQPAD", "NETCORE", "DEBUG" };

        public enum CSharp_PreProcSymbols
        {
            LINQPAD,
            NETCORE,
            DEBUG
        }
    }
}
