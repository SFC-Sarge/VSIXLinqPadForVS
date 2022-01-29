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
// File Name        : LinqOperators.cs
// License          : Open Source Apache License Version 2.0. Eee included License file.
// <summary></summary>
// ***********************************************************************
using System.Linq;

namespace VSIXLinqPadForVS.LinqParser
{
    public class LinqSeparators
    {
        // Query Keywords reference URL: (https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/keywords/query-keywords)
        public static readonly string[] Separators = "\r \n \r\n".Split().ToArray();
    }
}
