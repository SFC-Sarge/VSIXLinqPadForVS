# LinqPadForVS

LinqPad for Visual Studio is a Visual Studio 2022 Extension, that allows developer to run a selected linq query or linq method in the active document, and display those results in a Visual Studio ToolWindow.

This works by using LPRun7-x64.exe to run the query and return the script and results of the script to the Toolwindow called:

>**My Linq Query Tool Window**

It also currently creates a Output window called:

>**LinqPad Dump**

The Toolwindow Toolbar button calls the following using process:

`LPRun7-x64.exe -fx=6.0 (selected query to run)   // Run query under .NET 6.0`


 ```
{
   using Process process = new();
    process.StartInfo = new ProcessStartInfo()
    {
        UseShellExecute = false,
        CreateNoWindow = true,
        WindowStyle = ProcessWindowStyle.Hidden,
        FileName = fileLPRun7,
        Arguments = $"{fileLPRun7Args} {tempQueryPath}",
        RedirectStandardError = true,
        RedirectStandardOutput = true
    };
    process.Start();
    queryResult = await process.StandardOutput.ReadToEndAsync();
    process.WaitForExit();
  }
  ```


and returns the query results to the **My Linq Query Tool Window** and **LinqPad Dump** windows.

> Note: You must have [Community.VisualStudio.Toolkit](https://github.com/VsixCommunity/Community.VisualStudio.Toolkit) installed for your extension project.
