using LINQPad;
using LINQPad.FSharpExtensions;

using Microsoft.VisualStudio.Shell.Interop;

using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Shapes;

namespace VSIXLinqPadForVS
{
    public partial class MyToolWindowControl : UserControl
    {
        OutputWindowPane pane = null;
        public IVsHierarchy Hierarchy = null;
        public ToolWindowMessenger ToolWindowMessenger = null;
        public string CapabilityValues = null;
        public string fileExtension = null;
        public Project _activeProject;
        public string _activeFile;
        public string queryResult = null;
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
            ThreadHelper.JoinableTaskFactory.RunAsync(async () =>
            {

                await DoOutputWindowsAsync();
                //await pane.ClearAsync();
                await UpdateLinqPadDumpAsync(Hierarchy);
            }).FireAndForget();
            VS.Events.SelectionEvents.SelectionChanged += SelectionChanged;
            VS.Events.DocumentEvents.BeforeDocumentWindowShow += BeforeDocumentWindowShow;
            VS.Events.SolutionEvents.OnAfterCloseSolution += OnAfterCloseSolution;

        }
        private void OnMessageReceived(object sender, string e)
        {
            ThreadHelper.JoinableTaskFactory.RunAsync(async () =>
            {
                switch (e)
                {
                    case "Dump Selected Linq Query":
                        await UpdateLinqPadDumpAsync(Hierarchy);
                        break;
                    case "Dump ToolWindow Linq Query":
                        await UpdateLinqPadDumpAsync(Hierarchy);
                        break;
                    default:
                        break;
                }
            }).FireAndForget();

        }
        private void BeforeDocumentWindowShow(DocumentView docView)
        {
            ThreadHelper.JoinableTaskFactory.RunAsync(async () =>
            {
                if (docView?.Document?.FilePath != _activeFile)
                {
                    _activeFile = docView?.Document?.FilePath;
                    var ext = System.IO.Path.GetExtension(docView?.Document?.FilePath);
                    //await UpdateFilesAsync(docView.TextBuffer.ContentType, ext);
                    fileExtension = ext;
                    //await UpdateFilesAsync(ext);
                    await UpdateLinqPadDumpAsync(Hierarchy);
                }

            }).FireAndForget();
        }
        private void OnAfterCloseSolution()
        {
            ThreadHelper.ThrowIfNotOnUIThread();
            SelectionChanged(null, null);
        }
        private void SelectionChanged(object sender, Community.VisualStudio.Toolkit.SelectionChangedEventArgs e)
        {
            ThreadHelper.JoinableTaskFactory.RunAsync(async () =>
            {
                Project project = await VS.Solutions.GetActiveProjectAsync();

                if (project != _activeProject)
                {
                    _activeProject = project;
                    await ThreadHelper.JoinableTaskFactory.SwitchToMainThreadAsync();
                }
            }).FireAndForget();
        }

        public async Task UpdateLinqPadDumpAsync(IVsHierarchy hierarchy)
        {
            //await pane.ClearAsync();
            await pane.WriteLineAsync($"2 * 25 = {(2 * 25).Dump<int>()}");
        }

        private async Task DoOutputWindowsAsync()
        {
            pane = await VS.Windows.CreateOutputWindowPaneAsync("LinqPad Dump");
            return;
        }
        //private void RunLinqPadQuery(string currentSelection)
        //{
        //    string tempQueryPath = System.IO.Path.GetTempFileName() + ".linq";
        //    File.WriteAllText(tempQueryPath, $"<Query Kind='Statements' />\r\n{currentSelection}\r\nresult.Dump();");
        //    var queryResult1 = Util.Run(tempQueryPath, QueryResultFormat.Text).AsString().Dump();
        //    //var queryResult1 = Util.Run(tempQueryPath, QueryResultFormat.Text);
        //    queryResult1.Dump();
        //    //queryResult1.AsString().Dump();
        //    //queryResult1.AsString().DumpTrace();
        //    //queryResult = queryResult1;
        //    pane.WriteLine($"{currentSelection} \r\n\r\nCurrent Selection Query Results are:\r\n{queryResult1}");
        //}

        //private async void RunLinqPadQuery(string currentSelection)
        //{
        //    string tempQueryPath = System.IO.Path.GetTempFileName() + ".linq";
        //    File.WriteAllText(tempQueryPath, $"<Query Kind='Statements' />\r\n{currentSelection}\r\nresult.Dump();");
        //    var queryResult1 = await Util.Run(tempQueryPath, QueryResultFormat.Text).AsStringAsync().Dump();
        //    //var queryResult1 = Util.Run(tempQueryPath, QueryResultFormat.Text);
        //    queryResult1.Dump();
        //    //queryResult1.AsString().Dump();
        //    //queryResult1.AsString().DumpTrace();
        //    //queryResult = queryResult1;
        //    //await pane.WriteLineAsync($"{currentSelection} \r\n\r\nCurrent Selection Query Results are:\r\n{queryResult1}");
        //}

        //private async System.Threading.Tasks.Task<string> RunLinqPadQueryAsync(string currentSelection)
        //{
        //    //ThreadHelper.JoinableTaskFactory.RunAsync(async () =>
        //    //{
        //    string tempQueryPath = System.IO.Path.GetTempFileName() + ".linq";
        //    File.WriteAllText(tempQueryPath, $"<Query Kind='Statements' />\r\n{currentSelection}\r\nresult.Dump();");
        //    await ThreadHelper.JoinableTaskFactory.SwitchToMainThreadAsync();
        //    var queryResult1 = await Util.Run(tempQueryPath, QueryResultFormat.Text).AsStringAsync().Dump().Dump();
        //    await pane.WriteLineAsync($"{currentSelection} \r\n\r\nCurrent Selection Query Results are:\r\n{queryResult1}");
        //    return $"{currentSelection} \r\n\r\nCurrent Selection Query Results are:\r\n{queryResult1}";

        //    //}).FireAndForget();

        //}

        private void RunQuery_Click(object sender, RoutedEventArgs e)
        {
            ThreadHelper.JoinableTaskFactory.RunAsync(async () =>
            {
                await pane.ClearAsync();
                LinqPadResults.Children.Clear();
                DocumentView docView = await VS.Documents.GetActiveDocumentViewAsync();
                if (docView?.TextView == null) return; //not a text window
                if (docView.TextView.Selection != null && !docView.TextView.Selection.IsEmpty)
                {
                    try
                    {
                        var currentSelection = docView.TextView.Selection.StreamSelectionSpan.GetText().Trim().Replace("  ", "").Trim();
                        await ThreadHelper.JoinableTaskFactory.SwitchToMainThreadAsync();
                        string tempQueryPath = System.IO.Path.GetTempFileName() + ".linq";
                        string queryString = $"<Query Kind='Statements' />\r\n{currentSelection}\r\nresult.Dump();";
                        System.IO.File.WriteAllText(tempQueryPath, queryString);

                        using Process process = new();
                        process.StartInfo = new ProcessStartInfo()
                        {
                            UseShellExecute = false,
                            CreateNoWindow = true,
                            WindowStyle = ProcessWindowStyle.Hidden,
                            FileName = @"D:\Program Files\LINQPad7-Beta\LPRun7-x64.exe",
                            Arguments = $"-fx=6.0 {tempQueryPath}",
                            RedirectStandardError = true,
                            RedirectStandardOutput = true
                        };
                        process.Start();
                        queryResult = await process.StandardOutput.ReadToEndAsync();
                        process.WaitForExit();

                        await pane.WriteLineAsync($"{currentSelection} \r\n\r\nCurrent Selection Query Results = {queryResult}");
                        var selectedQueryResult = new TextBlock { Text = $"{currentSelection} \r\n\r\nCurrent Selection Query Results = {queryResult}", TextWrapping = TextWrapping.Wrap, Margin = new Thickness(0, 0, 0, 5) };
                        LinqPadResults.Children.Add(selectedQueryResult);
                        var line = new Line { Margin = new Thickness(0, 0, 0, 20) };
                        LinqPadResults.Children.Add(line);
                    }
                    catch (Exception ex)
                    {
                        var exceptionResult = new TextBlock { Text = ex.Message, TextWrapping = TextWrapping.Wrap, Margin = new Thickness(0, 0, 0, 5) };
                        LinqPadResults.Children.Add(exceptionResult);
                        await pane.WriteLineAsync($"Exception LinqPad.Util.Run Call. {exceptionResult.Text}");
                    }
                }
            }).FireAndForget();

        }
    }
}