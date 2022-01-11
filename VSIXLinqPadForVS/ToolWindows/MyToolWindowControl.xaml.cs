using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Shapes;
using Microsoft.VisualStudio.Shell.Interop;
using Microsoft.VisualStudio.TextManager.Interop;

namespace VSIXLinqPadForVS
{
    public partial class MyToolWindowControl : UserControl
    {
        OutputWindowPane pane = null;
        public ToolWindowMessenger ToolWindowMessenger = null;
        public Project _activeProject;
        public string _activeFile;
        public string queryResult = null;
        public string dirLPRun7 = null;
        public string fileLPRun7 = null;
        public Guid myEditor = new Guid(PackageGuids.EditorFactoryString);


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
            fileLPRun7 = System.IO.Path.Combine(dirLPRun7, Constants.solutionToolWindowsFolderName, Constants.lPRun7Executable);
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
                    case Constants.runSelectedLinqStatement:
                        await RunLinqStatementAsync();
                        break;
                    case Constants.runSelectedLinqMethod:
                        await RunLinqMethodAsync();
                        break;
                    case Constants.runEditorLinqQuery:
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
                    TextBlock NothingSelectedResult = new TextBlock { Text = Constants.noActiveDocument, TextWrapping = TextWrapping.Wrap, Margin = new Thickness(0, 0, 0, 5) };
                    LinqPadResults.Children.Add(NothingSelectedResult);
                    await pane.WriteLineAsync(Constants.noActiveDocument);
                    return;
                }
                if (docView.TextView.Selection != null && !docView.TextView.Selection.IsEmpty)
                {
                    await pane.WriteLineAsync(Constants.runningSelectQuery);
                    TextBlock runningQueryResult = new TextBlock { Text = Constants.runningSelectQuery, TextWrapping = TextWrapping.Wrap, Margin = new Thickness(0, 0, 0, 5) };
                    LinqPadResults.Children.Add(runningQueryResult);

                    try
                    {
                        string currentSelection = docView.TextView.Selection.StreamSelectionSpan.GetText().Trim().Replace("  ", "").Trim();
                        await ThreadHelper.JoinableTaskFactory.SwitchToMainThreadAsync();
                        string tempQueryPath = $"{System.IO.Path.GetTempFileName()}{Constants.FileExtension}";
                        string queryString = $"{Constants.queryKindStatement}\r\n{currentSelection}\r\n{Constants.resultDump};".Trim();
                        System.IO.File.WriteAllText(tempQueryPath, queryString);

                        using Process process = new();
                        process.StartInfo = new ProcessStartInfo()
                        {
                            UseShellExecute = false,
                            CreateNoWindow = true,
                            WindowStyle = ProcessWindowStyle.Hidden,
                            FileName = fileLPRun7,
                            Arguments = $"{Constants.fileLPRun7Args} {tempQueryPath}",
                            RedirectStandardError = true,
                            RedirectStandardOutput = true
                        };
                        process.Start();
                        queryResult = await process.StandardOutput.ReadToEndAsync();
                        process.WaitForExit();
                        await pane.ClearAsync();
                        LinqPadResults.Children.Clear();
                        if (AdvancedOptions.Instance.UseLinqPadDumpWindow == true)
                        {
                            await pane.WriteLineAsync($"{currentSelection} \r\n\r\n{Constants.currentSelectionQuery} = {queryResult}");
                        }
                        if (AdvancedOptions.Instance.EnableToolWindowResults == true)
                        {
                            TextBlock selectedQueryResult = new TextBlock { Text = $"{currentSelection} \r\n\r\n{Constants.currentSelectionQuery} = {queryResult}", TextWrapping = TextWrapping.Wrap, Margin = new Thickness(0, 0, 0, 5) };
                            LinqPadResults.Children.Add(selectedQueryResult);
                            Line line = new Line { Margin = new Thickness(0, 0, 0, 20) };
                            LinqPadResults.Children.Add(line);
                        }
                        tempQueryPath = $"{System.IO.Path.GetTempFileName()}{Constants.FileExtension}";
                        System.IO.File.WriteAllText(tempQueryPath, $"{currentSelection} \r\n\r\n{Constants.currentSelectionQuery} = {queryResult}".Trim());

                        if (AdvancedOptions.Instance.OpenInVSPreviewTab == true)
                        {
                            await VS.Documents.OpenInPreviewTabAsync(tempQueryPath);
                        }
                        else
                        {
                            await VS.Documents.OpenAsync(tempQueryPath);
                        }
                    }
                    catch (Exception ex)
                    {
                        TextBlock exceptionResult = new TextBlock { Text = ex.Message, TextWrapping = TextWrapping.Wrap, Margin = new Thickness(0, 0, 0, 5) };
                        LinqPadResults.Children.Add(exceptionResult);
                        await pane.WriteLineAsync($"{Constants.exceptionIn} {fileLPRun7} {Constants.exceptionCall} {exceptionResult.Text}");
                    }
                }
                else
                {
                    TextBlock NothingSelectedResult = new TextBlock { Text = Constants.noActiveDocument, TextWrapping = TextWrapping.Wrap, Margin = new Thickness(0, 0, 0, 5) };
                    LinqPadResults.Children.Add(NothingSelectedResult);
                    await pane.WriteLineAsync(Constants.noActiveDocument);
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
                    TextBlock NothingSelectedResult = new TextBlock { Text = Constants.noActiveDocumentMethod, TextWrapping = TextWrapping.Wrap, Margin = new Thickness(0, 0, 0, 5) };
                    LinqPadResults.Children.Add(NothingSelectedResult);
                    await pane.WriteLineAsync(Constants.noActiveDocumentMethod);
                    return;
                }
                if (docView.TextView.Selection != null && !docView.TextView.Selection.IsEmpty)
                {
                    await pane.WriteLineAsync(Constants.runningSelectQueryMethod);
                    TextBlock runningQueryResult = new TextBlock { Text = Constants.runningSelectQueryMethod, TextWrapping = TextWrapping.Wrap, Margin = new Thickness(0, 0, 0, 5) };
                    LinqPadResults.Children.Add(runningQueryResult);

                    try
                    {
                        string currentSelection = docView.TextView.Selection.StreamSelectionSpan.GetText().Trim().Replace("  ", "").Trim();
                        await ThreadHelper.JoinableTaskFactory.SwitchToMainThreadAsync();
                        string tempQueryPath = $"{System.IO.Path.GetTempFileName()}{Constants.FileExtension}";
                        string methodName = currentSelection.Substring(0, currentSelection.IndexOf("\r"));
                        string methodNameComplete = methodName.Substring(methodName.LastIndexOf(" ") + 1, methodName.LastIndexOf(")") - methodName.LastIndexOf(" "));
                        string methodCallLine = "{\r\n" + $"{methodNameComplete}" + ";\r\n}";
                        string queryString = $"{Constants.queryKindMethod}\r\nvoid Main()\r\n{methodCallLine}\r\n{currentSelection}".Trim();
                        System.IO.File.WriteAllText(tempQueryPath, queryString);
                        using Process process = new();
                        process.StartInfo = new ProcessStartInfo()
                        {
                            UseShellExecute = false,
                            CreateNoWindow = true,
                            WindowStyle = ProcessWindowStyle.Hidden,
                            FileName = fileLPRun7,
                            Arguments = $"{Constants.fileLPRun7Args} {tempQueryPath}",
                            RedirectStandardError = true,
                            RedirectStandardOutput = true
                        };
                        process.Start();
                        queryResult = await process.StandardOutput.ReadToEndAsync();
                        process.WaitForExit();
                        await pane.ClearAsync();
                        LinqPadResults.Children.Clear();
                        if (AdvancedOptions.Instance.UseLinqPadDumpWindow == true)
                        {
                            await pane.WriteLineAsync($"{queryString} \r\n\r\n{Constants.currentSelectionQueryMethod} =\r\n{queryResult}");
                        }
                        if (AdvancedOptions.Instance.EnableToolWindowResults == true)
                        {
                            TextBlock selectedQueryResult = new TextBlock { Text = $"{queryString} \r\n\r\n{Constants.currentSelectionQueryMethod} = {queryResult}", TextWrapping = TextWrapping.Wrap, Margin = new Thickness(0, 0, 0, 5) };
                            LinqPadResults.Children.Add(selectedQueryResult);
                            Line line = new Line { Margin = new Thickness(0, 0, 0, 20) };
                            LinqPadResults.Children.Add(line);
                        }

                        tempQueryPath = $"{System.IO.Path.GetTempFileName()}{Constants.FileExtension}";
                        System.IO.File.WriteAllText(tempQueryPath, $"{queryString} \r\n\r\n{Constants.currentSelectionQueryMethod} = {queryResult}".Trim());

                        if (AdvancedOptions.Instance.OpenInVSPreviewTab == true)
                        {
                            await VS.Documents.OpenInPreviewTabAsync(tempQueryPath);
                        }
                        else
                        {
                            await VS.Documents.OpenAsync(tempQueryPath);
                        }
                    }
                    catch (Exception ex)
                    {
                        TextBlock exceptionResult = new TextBlock { Text = ex.Message, TextWrapping = TextWrapping.Wrap, Margin = new Thickness(0, 0, 0, 5) };
                        LinqPadResults.Children.Add(exceptionResult);
                        await pane.WriteLineAsync($"{Constants.exceptionIn} {fileLPRun7} {Constants.exceptionCall} {exceptionResult.Text}");
                    }
                }
                else
                {
                    TextBlock NothingSelectedResult = new TextBlock { Text = Constants.noActiveDocumentMethod, TextWrapping = TextWrapping.Wrap, Margin = new Thickness(0, 0, 0, 5) };
                    LinqPadResults.Children.Add(NothingSelectedResult);
                    await pane.WriteLineAsync(Constants.noActiveDocumentMethod);
                }
            }).FireAndForget();
        }
        public async Task<DocumentView> OpenDocumentWithSpecificEditorAsync(string file, Guid editorType, Guid LogicalView)
        {
            await ThreadHelper.JoinableTaskFactory.SwitchToMainThreadAsync();
            VsShellUtilities.OpenDocumentWithSpecificEditor(ServiceProvider.GlobalProvider, file, editorType, LogicalView, out _, out _, out IVsWindowFrame frame);
            IVsTextView nativeView = VsShellUtilities.GetTextView(frame);
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
                    TextBlock NothingSelectedResult = new TextBlock { Text = Constants.noActiveDocument, TextWrapping = TextWrapping.Wrap, Margin = new Thickness(0, 0, 0, 5) };
                    LinqPadResults.Children.Add(NothingSelectedResult);
                    await pane.WriteLineAsync(Constants.noActiveDocument);
                    return;
                }
                if (docView.TextView.Selection != null && !docView.TextView.Selection.IsEmpty)
                {
                    await pane.WriteLineAsync(Constants.runningSelectQuery);
                    TextBlock runningQueryResult = new TextBlock { Text = Constants.runningSelectQuery, TextWrapping = TextWrapping.Wrap, Margin = new Thickness(0, 0, 0, 5) };
                    LinqPadResults.Children.Add(runningQueryResult);

                    try
                    {
                        await ThreadHelper.JoinableTaskFactory.SwitchToMainThreadAsync();
                        string currentSelection = docView.TextView.Selection.StreamSelectionSpan.GetText().Trim().Replace("  ", "").Trim();
                        string tempQueryPath = $"{System.IO.Path.GetTempFileName()}{Constants.FileExtension}";
                        if (!currentSelection.StartsWith("<Query Kind="))
                        {
                            string methodName = currentSelection.Substring(0, currentSelection.IndexOf("\r"));
                            string methodNameComplete = methodName.Substring(methodName.LastIndexOf(" ") + 1, methodName.LastIndexOf(")") - methodName.LastIndexOf(" "));
                            string methodCallLine = "{\r\n" + $"{methodNameComplete}" + ";\r\n}";
                            string queryString = $"{Constants.queryKindMethod}\r\nvoid Main()\r\n{methodCallLine}\r\n{currentSelection}".Trim();
                            System.IO.File.WriteAllText(tempQueryPath, queryString);
                        }
                        else if (currentSelection.StartsWith("<Query Kind="))
                        {
                            System.IO.File.WriteAllText(tempQueryPath, currentSelection);
                        }
                        else
                        {
                            string queryString = $"{Constants.queryKindStatement}\r\n{currentSelection}\r\n{Constants.resultDump};";
                            System.IO.File.WriteAllText(tempQueryPath, queryString);
                        }

                        using Process process = new();
                        process.StartInfo = new ProcessStartInfo()
                        {
                            UseShellExecute = false,
                            CreateNoWindow = true,
                            WindowStyle = ProcessWindowStyle.Hidden,
                            FileName = fileLPRun7,
                            Arguments = $"{Constants.fileLPRun7Args} {tempQueryPath}",
                            RedirectStandardError = true,
                            RedirectStandardOutput = true
                        };
                        process.Start();
                        queryResult = await process.StandardOutput.ReadToEndAsync();
                        process.WaitForExit();

                        await pane.ClearAsync();
                        LinqPadResults.Children.Clear();
                        if (AdvancedOptions.Instance.UseLinqPadDumpWindow == true)
                        {
                            await pane.WriteLineAsync($"{currentSelection} \r\n\r\n{Constants.currentSelectionQuery} = {queryResult}");
                        }
                        if (AdvancedOptions.Instance.EnableToolWindowResults == true)
                        {
                            TextBlock selectedQueryResult = new TextBlock { Text = $"{currentSelection} \r\n\r\n{Constants.currentSelectionQuery} = {queryResult}", TextWrapping = TextWrapping.Wrap, Margin = new Thickness(0, 0, 0, 5) };
                            LinqPadResults.Children.Add(selectedQueryResult);
                            Line line = new Line { Margin = new Thickness(0, 0, 0, 20) };
                            LinqPadResults.Children.Add(line);
                        }
                        tempQueryPath = $"{System.IO.Path.GetTempFileName()}{Constants.FileExtension}";
                        System.IO.File.WriteAllText(tempQueryPath, $"{currentSelection} \r\n\r\n{Constants.currentSelectionQuery} = {queryResult}".Trim());

                        //await OpenDocumentWithSpecificEditorAsync(tempQueryPath, myEditor, myEditorView);
                        if (AdvancedOptions.Instance.OpenInVSPreviewTab == true)
                        {
                            await VS.Documents.OpenInPreviewTabAsync(tempQueryPath);
                        }
                        else
                        {
                            await VS.Documents.OpenAsync(tempQueryPath);
                        }
                    }
                    catch (Exception ex)
                    {
                        TextBlock exceptionResult = new TextBlock { Text = ex.Message, TextWrapping = TextWrapping.Wrap, Margin = new Thickness(0, 0, 0, 5) };
                        LinqPadResults.Children.Add(exceptionResult);
                        await pane.WriteLineAsync($"{Constants.exceptionIn} {fileLPRun7} {Constants.exceptionCall} {exceptionResult.Text}");
                    }
                }
                else
                {
                    TextBlock NothingSelectedResult = new TextBlock { Text = Constants.noActiveDocument, TextWrapping = TextWrapping.Wrap, Margin = new Thickness(0, 0, 0, 5) };
                    LinqPadResults.Children.Add(NothingSelectedResult);
                    await pane.WriteLineAsync(Constants.noActiveDocument);
                }
            }).FireAndForget();
        }

        private async Task DoOutputWindowsAsync()
        {
            pane = await VS.Windows.CreateOutputWindowPaneAsync(Constants.linpPadDump);
            return;
        }

    }
}