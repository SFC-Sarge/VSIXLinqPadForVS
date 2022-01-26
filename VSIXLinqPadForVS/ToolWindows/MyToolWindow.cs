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
// File Name        : MyToolWindow.cs
// License          : Open Source Apache License Version 2.0. Eee included License file.
// <summary></summary>
// ***********************************************************************
using Microsoft.VisualStudio.Imaging;

using System.ComponentModel.Design;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

/// <summary>
/// The ToolWindows namespace.
/// </summary>
/// <remarks>
///  	<para><b>History:</b></para>
///  	<list type="table">
///  		<listheader>
///  			<devName>Developer\Date\Time</devName>
///  			<devCompany>Developer Company</devCompany>
///  			<devPhone>Developer Phone</devPhone>
///  			<devEmail>Developer Email</devEmail>
///  			<devMachine>Developer On</devMachine>
///  			<description>Description</description>
///  		</listheader>
///  		<item>
///  				<devName>
/// 		Developer: Danny C. McNaught
/// 		<para>Date: Wednesday, January 26, 2022</para>
/// 		<para>Time: 17:17</para>
/// 	</devName>
///  			<devCompany>Computer Question</devCompany>
///  			<devPhone>#-###-###-####</devPhone>
///  				<devEmail>
/// 		<a href="mailto:danny.mcnaught@dannymcnaught.com">mailto:danny.mcnaught@dannymcnaught.com</a>
/// 		<para><a href="mailto:">mailto:</a></para>
/// 		<para><a href="mailto:">mailto:</a></para>
/// 	</devEmail>
///  			<devMachine>WINDOWS11DEV</devMachine>
///  			<description>Created XML Comment</description>
///  		</item>
///  		<item>
///  				<devName>
/// 		Developer: Danny C. McNaught
/// 		<para>Date: Wednesday, January 26, 2022</para>
/// 		<para>Time: 17:17</para>
/// 	</devName>
///  			<devCompany>Computer Question</devCompany>
///  			<devPhone>#-###-###-####</devPhone>
///  				<devEmail>
/// 		<a href="mailto:danny.mcnaught@dannymcnaught.com">mailto:danny.mcnaught@dannymcnaught.com</a>
/// 		<para><a href="mailto:">mailto:</a></para>
/// 		<para><a href="mailto:">mailto:</a></para>
/// 	</devEmail>
///  			<devMachine>WINDOWS11DEV</devMachine>
///  			<description>Updated XML Comment</description>
///  		</item>
///  	</list>
/// </remarks>
namespace VSIXLinqPadForVS.ToolWindows
{
    /// <summary>Class MyToolWindow</summary>
    /// <remarks>
    /// <para><b>History:</b></para>
    /// <list type="table">
    /// <item>
    /// <description><b>Code Changed by:</b><para>Danny C. McNaught</para><para><para><a href="mailto:danny.mcnaught@dannymcnaught.com">danny.mcnaught@dannymcnaught.com</a></para></para></description>
    /// </item>
    /// <item>
    /// <description><b>Code changed on Visual Studio Host Machine:</b><para>WINDOWS11DEV</para></description>
    /// </item>
    /// <item>
    /// <description><b>Code Change Date and Time:</b><para>Wednesday, January 26, 2022 17:17</para><b>Code Changes:</b><para>Created XML Comment</para></description>
    /// </item>
    /// </list>
    /// </remarks>
    public class MyToolWindow : BaseToolWindow<MyToolWindow>
    {
        /// <summary>Gets the title to show in the tool window.</summary>
        /// <param name="toolWindowId">The ID of the tool window for a multi-instance tool window.</param>
        /// <returns>System.String.</returns>
        /// <remarks><para>
        ///   <b>History:</b>
        /// </para>
        /// <list type="table">
        ///   <item>
        ///     <description>
        ///       <b>Code Changed by:</b>
        ///       <para>Danny C. McNaught</para>
        ///       <para>
        ///         <para>
        ///           <a href="mailto:danny.mcnaught@dannymcnaught.com">danny.mcnaught@dannymcnaught.com</a>
        ///         </para>
        ///       </para>
        ///     </description>
        ///   </item>
        ///   <item>
        ///     <description>
        ///       <b>Code changed on Visual Studio Host Machine:</b>
        ///       <para>WINDOWS11DEV</para>
        ///     </description>
        ///   </item>
        ///   <item>
        ///     <description>
        ///       <b>Code Change Date and Time:</b>
        ///       <para>Wednesday, January 26, 2022 16:41</para>
        ///       <b>Code Changes:</b>
        ///       <para>Created XML Comment</para>
        ///     </description>
        ///   </item>
        /// </list></remarks>
        /// <remarks>
        /// <para><b>History:</b></para>
        /// <list type="table">
        /// <item>
        /// <description><b>Code Changed by:</b><para>Danny C. McNaught</para><para><para><a href="mailto:danny.mcnaught@dannymcnaught.com">danny.mcnaught@dannymcnaught.com</a></para></para></description>
        /// </item>
        /// <item>
        /// <description><b>Code changed on Visual Studio Host Machine:</b><para>WINDOWS11DEV</para></description>
        /// </item>
        /// <item>
        /// <description><b>Code Change Date and Time:</b><para>Wednesday, January 26, 2022 17:17</para><b>Code Changes:</b><para>Created XML Comment</para></description>
        /// </item>
        /// </list>
        /// </remarks>
        public override string GetTitle(int toolWindowId) => "My Linq Query Tool Window";

        /// <summary>Gets the type of <see cref="T:Microsoft.VisualStudio.Shell.ToolWindowPane" /> that will be created for this tool window.</summary>
        /// <remarks>
        /// <para><b>History:</b></para>
        /// <list type="table">
        /// <item>
        /// <description><b>Code Changed by:</b><para>Danny C. McNaught</para><para><para><a href="mailto:danny.mcnaught@dannymcnaught.com">danny.mcnaught@dannymcnaught.com</a></para></para></description>
        /// </item>
        /// <item>
        /// <description><b>Code changed on Visual Studio Host Machine:</b><para>WINDOWS11DEV</para></description>
        /// </item>
        /// <item>
        /// <description><b>Code Change Date and Time:</b><para>Wednesday, January 26, 2022 17:17</para><b>Code Changes:</b><para>Created XML Comment</para></description>
        /// </item>
        /// </list>
        /// </remarks>
        public override Type PaneType => typeof(Pane);

        /// <summary>Creates the UI element that will be shown in the tool window.
        /// Use this method to create the user control or any other UI element that you want to show in the tool window.</summary>
        /// <param name="toolWindowId">The ID of the tool window instance being created for a multi-instance tool window.</param>
        /// <param name="cancellationToken">The cancellation token to use when performing asynchronous operations.</param>
        /// <returns>The UI element to show in the tool window.</returns>
        /// <remarks><para>
        ///   <b>History:</b>
        /// </para>
        /// <list type="table">
        ///   <item>
        ///     <description>
        ///       <b>Code Changed by:</b>
        ///       <para>Danny C. McNaught</para>
        ///       <para>
        ///         <para>
        ///           <a href="mailto:danny.mcnaught@dannymcnaught.com">danny.mcnaught@dannymcnaught.com</a>
        ///         </para>
        ///       </para>
        ///     </description>
        ///   </item>
        ///   <item>
        ///     <description>
        ///       <b>Code changed on Visual Studio Host Machine:</b>
        ///       <para>WINDOWS11DEV</para>
        ///     </description>
        ///   </item>
        ///   <item>
        ///     <description>
        ///       <b>Code Change Date and Time:</b>
        ///       <para>Wednesday, January 26, 2022 16:41</para>
        ///       <b>Code Changes:</b>
        ///       <para>Created XML Comment</para>
        ///     </description>
        ///   </item>
        /// </list></remarks>
        /// <remarks>
        /// <para><b>History:</b></para>
        /// <list type="table">
        /// <item>
        /// <description><b>Code Changed by:</b><para>Danny C. McNaught</para><para><para><a href="mailto:danny.mcnaught@dannymcnaught.com">danny.mcnaught@dannymcnaught.com</a></para></para></description>
        /// </item>
        /// <item>
        /// <description><b>Code changed on Visual Studio Host Machine:</b><para>WINDOWS11DEV</para></description>
        /// </item>
        /// <item>
        /// <description><b>Code Change Date and Time:</b><para>Wednesday, January 26, 2022 17:17</para><b>Code Changes:</b><para>Created XML Comment</para></description>
        /// </item>
        /// </list>
        /// </remarks>
        public override async Task<FrameworkElement> CreateAsync(int toolWindowId, CancellationToken cancellationToken)
        {
            Project project = await VS.Solutions.GetActiveProjectAsync();
            ToolWindowMessenger toolWindowMessenger = await Package.GetServiceAsync<ToolWindowMessenger, ToolWindowMessenger>();
            return new MyToolWindowControl(project, toolWindowMessenger);
        }

        /// <summary>Class Pane</summary>
        /// <remarks>
        /// <para><b>History:</b></para>
        /// <list type="table">
        /// <item>
        /// <description><b>Code Changed by:</b><para>Danny C. McNaught</para><para><para><a href="mailto:danny.mcnaught@dannymcnaught.com">danny.mcnaught@dannymcnaught.com</a></para></para></description>
        /// </item>
        /// <item>
        /// <description><b>Code changed on Visual Studio Host Machine:</b><para>WINDOWS11DEV</para></description>
        /// </item>
        /// <item>
        /// <description><b>Code Change Date and Time:</b><para>Wednesday, January 26, 2022 17:17</para><b>Code Changes:</b><para>Created XML Comment</para></description>
        /// </item>
        /// </list>
        /// </remarks>
        [Guid("a3ac07c0-309a-4679-bf3b-a2de12944d66")]
        internal class Pane : ToolWindowPane
        {
            /// <summary>Creates a new tool window pane with a null parent service provider</summary>
            /// <remarks>
            /// <para><b>History:</b></para>
            /// <list type="table">
            /// <item>
            /// <description><b>Code Changed by:</b><para>Danny C. McNaught</para><para><para><a href="mailto:danny.mcnaught@dannymcnaught.com">danny.mcnaught@dannymcnaught.com</a></para></para></description>
            /// </item>
            /// <item>
            /// <description><b>Code changed on Visual Studio Host Machine:</b><para>WINDOWS11DEV</para></description>
            /// </item>
            /// <item>
            /// <description><b>Code Change Date and Time:</b><para>Wednesday, January 26, 2022 17:17</para><b>Code Changes:</b><para>Created XML Comment</para></description>
            /// </item>
            /// </list>
            /// </remarks>
            public Pane()
            {
                BitmapImageMoniker = KnownMonikers.ToolWindow;
                ToolBar = new CommandID(PackageGuids.VSIXLinqPadForVS, PackageIds.TWindowToolbar);
            }
        }
    }
}