using System.IO;
using Microsoft.VisualStudio;
using Microsoft.VisualStudio.Shell;
using Microsoft.VisualStudio.Shell.Interop;
using Microsoft.VisualStudio.TextManager.Interop;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Shapes;
using System.Threading.Tasks;

namespace VSIXLinqPadForVS
{
    public partial class MyToolWindowControl : UserControl
    {
        private const string noActiveDocument = "No Active Document View or Linq Selection!\r\nPlease Select Linq Statement in Active Document,\r\nthen try again!";
        private const string runningSelectQuery = "Running Selected Linq Query.\r\nPlease Wait!";
        private const string resultDump = "result.Dump()";
        private const string noActiveDocumentMethod = "No Active Document View or Linq Method Selection!\r\nPlease Select Linq Method in Active Document,\r\nthen try again!";
        private const string currentSelectionQueryMethod = "Current Selection Query Method Results";
        private const string currentSelectionQuery = "Current Selection Query Results";
        private const string runningSelectQueryMethod = "Running Selected Linq Query Method.\r\nPlease Wait!";
        private const string queryKindStatement = "<Query Kind='Statements' />";
        private const string queryKindMethod = "<Query Kind='Program' />";
        private const string linqExtension = ".cs";
        private const string exceptionIn = "Exception in ";
        private const string exceptionCall = "Call. ";
        private const string fileLPRun7Args = "-fx=6.0";
        private const string linpPadDump = "LinqPad Dump";
        private const string runSelectedLinqStatement = "Run Selected Linq Statement.";
        private const string runSelectedLinqMethod = "Run Selected Linq Method.";
        private const string runEditorLinqQuery = "Run Editor Linq Query.";
        private const string lPRun7Executable = "LPRun7-x64.exe";
        private const string solutionToolWindowsFolderName = "ToolWindows";
        OutputWindowPane pane = null;
        public ToolWindowMessenger ToolWindowMessenger = null;
        public Project _activeProject;
        public string _activeFile;
        public string queryResult = null;
        public string dirLPRun7 = null;
        public string fileLPRun7 = null;

        public MyToolWindowControl(Project activeProject, ToolWindowMessenger toolWindowMessenger)
        {
            ThreadHelper.ThrowIfNotOnUIThread();

            _activeProject = activeProject;

            InitializeComponent();
            if (toolWindowMessenger == null)
            {
                toolWindowMessenger = new ToolWindowMessenger();
            }
            ToolWindowMessenger = toolWindowMessenger;
            toolWindowMessenger.MessageReceived += OnMessageReceived;
            VS.Events.SolutionEvents.OnAfterCloseSolution += OnAfterCloseSolution;
            dirLPRun7 = System.IO.Path.GetDirectoryName(typeof(MyToolWindow).Assembly.Location);
            fileLPRun7 = System.IO.Path.Combine(dirLPRun7, solutionToolWindowsFolderName, lPRun7Executable);
            ThreadHelper.JoinableTaskFactory.RunAsync(async () =>
            {
                await DoOutputWindowsAsync();
            }).FireAndForget();
        }
        private void OnMessageReceived(object sender, string e)
        {
            ThreadHelper.JoinableTaskFactory.RunAsync(async () =>
            {
                switch (e)
                {
                    case runSelectedLinqStatement:
                        await RunLinqStatementAsync();
                        break;
                    case runSelectedLinqMethod:
                        await RunLinqMethodAsync();
                        break;
                    case runEditorLinqQuery:
                        await RunEditorLinqQueryAsync();
                        break;
                    default:
                        break;
                }
            }).FireAndForget();
        }
        private void OnAfterCloseSolution()
        {
            ThreadHelper.ThrowIfNotOnUIThread();
            ThreadHelper.JoinableTaskFactory.RunAsync(async () =>
            {
                await pane.ClearAsync();
                LinqPadResults.Children.Clear();
            }).FireAndForget();
        }

        public async Task RunLinqStatementAsync()
        {
            await ThreadHelper.JoinableTaskFactory.SwitchToMainThreadAsync();
            ThreadHelper.JoinableTaskFactory.RunAsync(async () =>
            {
                await pane.ClearAsync();
                LinqPadResults.Children.Clear();
                DocumentView docView = await VS.Documents.GetActiveDocumentViewAsync();
                if (docView?.TextView == null)
                {
                    var NothingSelectedResult = new TextBlock { Text = noActiveDocument, TextWrapping = TextWrapping.Wrap, Margin = new Thickness(0, 0, 0, 5) };
                    LinqPadResults.Children.Add(NothingSelectedResult);
                    await pane.WriteLineAsync(noActiveDocument);
                    return;
                }
                if (docView.TextView.Selection != null && !docView.TextView.Selection.IsEmpty)
                {
                    await pane.WriteLineAsync(runningSelectQuery);
                    var runningQueryResult = new TextBlock { Text = runningSelectQuery, TextWrapping = TextWrapping.Wrap, Margin = new Thickness(0, 0, 0, 5) };
                    LinqPadResults.Children.Add(runningQueryResult);

                    try
                    {
                        var currentSelection = docView.TextView.Selection.StreamSelectionSpan.GetText().Trim().Replace("  ", "").Trim();
                        await ThreadHelper.JoinableTaskFactory.SwitchToMainThreadAsync();
                        string tempQueryPath = $"{System.IO.Path.GetTempFileName()}{linqExtension}";
                        string queryString = $"{queryKindStatement}\r\n{currentSelection}\r\n{resultDump};";
                        File.WriteAllText(tempQueryPath, queryString);

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
                        await pane.ClearAsync();
                        LinqPadResults.Children.Clear();
                        await pane.WriteLineAsync($"{currentSelection} \r\n\r\n{currentSelectionQuery} = {queryResult}");
                        var selectedQueryResult = new TextBlock { Text = $"{currentSelection} \r\n\r\n{currentSelectionQuery} = {queryResult}", TextWrapping = TextWrapping.Wrap, Margin = new Thickness(0, 0, 0, 5) };
                        LinqPadResults.Children.Add(selectedQueryResult);
                        var line = new Line { Margin = new Thickness(0, 0, 0, 20) };
                        LinqPadResults.Children.Add(line);
                        Guid guid_microsoft_csharp_editor = new Guid("{A6C744A8-0E4A-4FC6-886A-064283054674}");
                        await OpenDocumentWithSpecificEditorAsync(tempQueryPath, guid_microsoft_csharp_editor, Guid.Empty);

                    }
                    catch (Exception ex)
                    {
                        var exceptionResult = new TextBlock { Text = ex.Message, TextWrapping = TextWrapping.Wrap, Margin = new Thickness(0, 0, 0, 5) };
                        LinqPadResults.Children.Add(exceptionResult);
                        await pane.WriteLineAsync($"{exceptionIn} {fileLPRun7} {exceptionCall} {exceptionResult.Text}");
                    }
                }
                else
                {
                    var NothingSelectedResult = new TextBlock { Text = noActiveDocument, TextWrapping = TextWrapping.Wrap, Margin = new Thickness(0, 0, 0, 5) };
                    LinqPadResults.Children.Add(NothingSelectedResult);
                    await pane.WriteLineAsync(noActiveDocument);
                }
            }).FireAndForget();
        }
        public async Task RunLinqMethodAsync()
        {
            await ThreadHelper.JoinableTaskFactory.SwitchToMainThreadAsync();
            ThreadHelper.JoinableTaskFactory.RunAsync(async () =>
            {
            await pane.ClearAsync();
            LinqPadResults.Children.Clear();
            DocumentView docView = await VS.Documents.GetActiveDocumentViewAsync();
            if (docView?.TextView == null)
            {
                var NothingSelectedResult = new TextBlock { Text = noActiveDocumentMethod, TextWrapping = TextWrapping.Wrap, Margin = new Thickness(0, 0, 0, 5) };
                LinqPadResults.Children.Add(NothingSelectedResult);
                await pane.WriteLineAsync(noActiveDocumentMethod);
                return;
            }
            if (docView.TextView.Selection != null && !docView.TextView.Selection.IsEmpty)
            {
                await pane.WriteLineAsync(runningSelectQueryMethod);
                var runningQueryResult = new TextBlock { Text = runningSelectQueryMethod, TextWrapping = TextWrapping.Wrap, Margin = new Thickness(0, 0, 0, 5) };
                LinqPadResults.Children.Add(runningQueryResult);

                try
                {
                    var currentSelection = docView.TextView.Selection.StreamSelectionSpan.GetText().Trim().Replace("  ", "").Trim();
                        await ThreadHelper.JoinableTaskFactory.SwitchToMainThreadAsync();
                        string tempQueryPath = $"{System.IO.Path.GetTempFileName()}{linqExtension}";
                        string methodName = currentSelection.Substring(0, currentSelection.IndexOf("\r"));
                        string methodNameComplete = methodName.Substring(methodName.LastIndexOf(" ") + 1, methodName.LastIndexOf(")") - methodName.LastIndexOf(" "));
                        string methodCallLine = "{\r\n" + $"{methodNameComplete}" + ";\r\n}";
                        string queryString = $"{queryKindMethod}\r\nvoid Main()\r\n{methodCallLine}\r\n{currentSelection}";
                        File.WriteAllText(tempQueryPath, queryString);
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
                        await pane.ClearAsync();
                        LinqPadResults.Children.Clear();
                        await pane.WriteLineAsync($"{queryString} \r\n\r\n{currentSelectionQueryMethod} =\r\n{queryResult}");
                        var selectedQueryResult = new TextBlock { Text = $"{queryString} \r\n\r\n{currentSelectionQueryMethod} = {queryResult}", TextWrapping = TextWrapping.Wrap, Margin = new Thickness(0, 0, 0, 5) };
                        LinqPadResults.Children.Add(selectedQueryResult);
                        var line = new Line { Margin = new Thickness(0, 0, 0, 20) };
                        LinqPadResults.Children.Add(line);
                        Guid guid_microsoft_csharp_editor = new Guid("{A6C744A8-0E4A-4FC6-886A-064283054674}");
                        await OpenDocumentWithSpecificEditorAsync(tempQueryPath, guid_microsoft_csharp_editor, Guid.Empty);
                    }
                    catch (Exception ex)
                    {
                        var exceptionResult = new TextBlock { Text = ex.Message, TextWrapping = TextWrapping.Wrap, Margin = new Thickness(0, 0, 0, 5) };
                        LinqPadResults.Children.Add(exceptionResult);
                        await pane.WriteLineAsync($"{exceptionIn} {fileLPRun7} {exceptionCall} {exceptionResult.Text}");
                    }
                }
                else
                {
                    var NothingSelectedResult = new TextBlock { Text = noActiveDocumentMethod, TextWrapping = TextWrapping.Wrap, Margin = new Thickness(0, 0, 0, 5) };
                    LinqPadResults.Children.Add(NothingSelectedResult);
                    await pane.WriteLineAsync(noActiveDocumentMethod);
                }
            }).FireAndForget();
        }
        public async Task<DocumentView?> OpenDocumentWithSpecificEditorAsync(string file, Guid editorType, Guid LogicalView)
        {
            await ThreadHelper.JoinableTaskFactory.SwitchToMainThreadAsync();
            VsShellUtilities.OpenDocumentWithSpecificEditor(ServiceProvider.GlobalProvider, file, editorType, LogicalView, out _, out _, out IVsWindowFrame? frame);
            IVsTextView? nativeView = VsShellUtilities.GetTextView(frame);
            return await nativeView.ToDocumentViewAsync();
        }
        public async Task RunEditorLinqQueryAsync()
        {
            await ThreadHelper.JoinableTaskFactory.SwitchToMainThreadAsync();
            ThreadHelper.JoinableTaskFactory.RunAsync(async () =>
            {
                await pane.ClearAsync();
                LinqPadResults.Children.Clear();
                DocumentView docView = await VS.Documents.GetActiveDocumentViewAsync();
                if (docView?.TextView == null)
                {
                    var NothingSelectedResult = new TextBlock { Text = noActiveDocument, TextWrapping = TextWrapping.Wrap, Margin = new Thickness(0, 0, 0, 5) };
                    LinqPadResults.Children.Add(NothingSelectedResult);
                    await pane.WriteLineAsync(noActiveDocument);
                    return;
                }
                if (docView.TextView.Selection != null && !docView.TextView.Selection.IsEmpty)
                {
                    await pane.WriteLineAsync(runningSelectQuery);
                    var runningQueryResult = new TextBlock { Text = runningSelectQuery, TextWrapping = TextWrapping.Wrap, Margin = new Thickness(0, 0, 0, 5) };
                    LinqPadResults.Children.Add(runningQueryResult);

                    try
                    {
                        await ThreadHelper.JoinableTaskFactory.SwitchToMainThreadAsync();
                        var currentSelection = docView.TextView.Selection.StreamSelectionSpan.GetText().Trim().Replace("  ", "").Trim();
                        string tempQueryPath = $"{System.IO.Path.GetTempFileName()}{linqExtension}";
                        if (!currentSelection.StartsWith("<Query Kind="))
                            {
                            string methodName = currentSelection.Substring(0, currentSelection.IndexOf("\r"));
                            string methodNameComplete = methodName.Substring(methodName.LastIndexOf(" ") + 1, methodName.LastIndexOf(")") - methodName.LastIndexOf(" "));
                            string methodCallLine = "{\r\n" + $"{methodNameComplete}" + ";\r\n}";
                            string queryString = $"{queryKindMethod}\r\nvoid Main()\r\n{methodCallLine}\r\n{currentSelection}";
                            File.WriteAllText(tempQueryPath, queryString);
                        }
                        else if (currentSelection.StartsWith("<Query Kind="))
                        {
                            File.WriteAllText(tempQueryPath, currentSelection);
                        }
                        else
                        {
                            string queryString = $"{queryKindStatement}\r\n{currentSelection}\r\n{resultDump};";
                            File.WriteAllText(tempQueryPath, queryString);
                        }

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

                        await pane.ClearAsync();
                        LinqPadResults.Children.Clear();
                        await pane.WriteLineAsync($"{currentSelection} \r\n\r\n{currentSelectionQuery} = {queryResult}");
                        var selectedQueryResult = new TextBlock { Text = $"{currentSelection} \r\n\r\n{currentSelectionQuery} = {queryResult}", TextWrapping = TextWrapping.Wrap, Margin = new Thickness(0, 0, 0, 5) };
                        LinqPadResults.Children.Add(selectedQueryResult);
                        var line = new Line { Margin = new Thickness(0, 0, 0, 20) };
                        LinqPadResults.Children.Add(line);
                        Guid guid_microsoft_csharp_editor = new Guid("{A6C744A8-0E4A-4FC6-886A-064283054674}");
                        await OpenDocumentWithSpecificEditorAsync(tempQueryPath, guid_microsoft_csharp_editor, Guid.Empty);

                    }
                    catch (Exception ex)
                    {
                        var exceptionResult = new TextBlock { Text = ex.Message, TextWrapping = TextWrapping.Wrap, Margin = new Thickness(0, 0, 0, 5) };
                        LinqPadResults.Children.Add(exceptionResult);
                        await pane.WriteLineAsync($"{exceptionIn} {fileLPRun7} {exceptionCall} {exceptionResult.Text}");
                    }
                }
                else
                {
                    var NothingSelectedResult = new TextBlock { Text = noActiveDocument, TextWrapping = TextWrapping.Wrap, Margin = new Thickness(0, 0, 0, 5) };
                    LinqPadResults.Children.Add(NothingSelectedResult);
                    await pane.WriteLineAsync(noActiveDocument);
                }
            }).FireAndForget();
        }

        private async Task DoOutputWindowsAsync()
        {
            pane = await VS.Windows.CreateOutputWindowPaneAsync(linpPadDump);
            return;
        }

    }
}