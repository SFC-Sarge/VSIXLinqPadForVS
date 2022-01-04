# LinqPadForVS

LinqPad for Visual Studio is a Visual Studio 2022 Extension, that allows developer to run a selected linq query or linq method in the active document, and display those results in a Visual Studio ToolWindow.

This works by using LPRun7-x64.exe to run the query and return the script and results of the script to the Toolwindow called:

>**My Linq Query Tool Window**

It also currently creates a Output window called:

>**LinqPad Dump**


I had to use LPRun7-x64.exe instead of LinqPad.Util.Run found in NuGet packages LinqPad (.net 4.8) and LinqPad.Runtime (.net core 3.0) because even thou I could get the code to run in an .net 4.8 or .net core 6.0 application and returned correct query results, but when using the same code in a Viusal Studio extension, the results returned are always an empty string result for the query.

I do not see this issue when using LPRun7-x64.exe to replace the Nuget package. 

>Note: I will revisit this issue once the [LinqPad](https://www.linqpad.net/) author releases a NuGet package that supports Visual Studio 2022 x64.

![LinqPad NuGet Packages](https://user-images.githubusercontent.com/67446778/148120404-9e35b180-89d9-4bf3-9bf4-b80765460768.png)


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

### LPRun7 Info

![LPRun7](https://user-images.githubusercontent.com/67446778/148120780-69d97423-63e8-4a08-8638-a9ceb6dd0f39.png)


> Note: You must have [Community.VisualStudio.Toolkit](https://github.com/VsixCommunity/Community.VisualStudio.Toolkit) installed for your extension project.
