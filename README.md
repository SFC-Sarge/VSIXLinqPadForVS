# LinqPadForVS

LinqPad Results for Visual Studio Extension that allows LinqPad to run a select linq query or linq method in the active document, then display those results in a Visual Studio ToolWindow.

This works by using LPRun7-x64.exe to run the query and return the script and results of the script to the Toolwindow called:

>**My Linq Query Tool Window**

It also currently creates a Output window called:

>**LinqPad Dump**

The Toolwindow Toolbar button calls run the following:

`LPRun7-x64.exe -fx=6.0 (selected query to run)   // Run query under .NET 6.0`

and returns the results to the **My Linq Query Tool Window**.

> Note: You must have [Community.VisualStudio.Toolkit](https://github.com/VsixCommunity/Community.VisualStudio.Toolkit) installed for your extension project.
