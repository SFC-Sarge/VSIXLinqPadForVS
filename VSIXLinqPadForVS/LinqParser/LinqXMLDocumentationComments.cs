//***********************************************************************
// Assembly         : VSIXLinqPadForVS
// Author UserID    : sfcsarge
// Author Full Name : Danny C. McNaught
// Author Phone     : #-###-###-####
// Created          : 01-26-2022
//
// Created By       : Danny C. McNaught
// Last Modified By : Danny C. McNaught
// Last Modified On : 01-26-2022
// Change Request # :
// Version Number   :
// Description      :
// File Name        : LinqXMLDocumentationComments.cs
// Copyright        : Open Source Apache License Version 2.0
// <summary></summary>
// ***********************************************************************
using System.Linq;

namespace VSIXLinqPadForVS.LinqParser
{
    public class LinqXMLDocumentationComments
    {
        // XML Documentation Comments /// Reference URL: (https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/xmldoc/)
        public static readonly string[] XMLDocumentationComments = "/// /** */".Split().ToArray();

        public enum CSharp_XMLDocumentationComments
        {
            SinglelineDelimiter,
            MultilineDelimiterStart,
            MultilineDelimiterEnd,
        }
    }
}
