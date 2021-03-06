//***********************************************************************
// Assembly         : VSIXLinqPadForVS
// Author UserID    : sfcsarge
// Author Full Name : Danny C. McNaught
// Author Phone     : #-###-###-####
// Company Name     : Computer Question// Created          : 01-26-2022
//
// Created By       : Danny C. McNaught
// Last Modified By : Danny C. McNaught
// Last Modified On : 01-26-2022
// Change Request # :
// Version Number   :
// Description      :
// File Name        : LinqPreProcSymbols.cs
// License          : Open Source Apache License Version 2.0. Eee included License file.
// <summary></summary>
// ***********************************************************************
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
