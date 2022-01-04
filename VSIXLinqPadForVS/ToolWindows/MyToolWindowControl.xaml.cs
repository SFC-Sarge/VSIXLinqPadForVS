using System.IO;
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
            ThreadHelper.JoinableTaskFactory.RunAsync(async () =>
            {
                await DoOutputWindowsAsync();
            }).FireAndForget();
            //VS.Events.SelectionEvents.SelectionChanged += SelectionChanged;
            //VS.Events.DocumentEvents.BeforeDocumentWindowShow += BeforeDocumentWindowShow;
            VS.Events.SolutionEvents.OnAfterCloseSolution += OnAfterCloseSolution;
            dirLPRun7 = System.IO.Path.GetDirectoryName(typeof(MyToolWindow).Assembly.Location);
            fileLPRun7 = System.IO.Path.Combine(dirLPRun7, "ToolWindows", "LPRun7-x64.exe");

        }
        private void OnMessageReceived(object sender, string e)
        {
            ThreadHelper.JoinableTaskFactory.RunAsync(async () =>
            {
                switch (e)
                {
                    case "Run Selected Linq Statement":
                        await RunLinqStatementAsync();
                        break;
                    case "Run Selected Linq File":
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
                    var NothingSelectedResult = new TextBlock { Text = "No Active Document View or Linq Selection!", TextWrapping = TextWrapping.Wrap, Margin = new Thickness(0, 0, 0, 5) };
                    LinqPadResults.Children.Add(NothingSelectedResult);
                    await pane.WriteLineAsync("No Active Document View or Linq Selection!");
                    return;
                }
                if (docView.TextView.Selection != null && !docView.TextView.Selection.IsEmpty)
                {
                    await pane.WriteLineAsync("Running Selected Linq Query.\r\nPlease Wait!");
                    var runningQueryResult = new TextBlock { Text = "Running Selected Linq Query.\r\nPlease Wait!", TextWrapping = TextWrapping.Wrap, Margin = new Thickness(0, 0, 0, 5) };
                    LinqPadResults.Children.Add(runningQueryResult);

                    try
                    {
                        var currentSelection = docView.TextView.Selection.StreamSelectionSpan.GetText().Trim().Replace("  ", "").Trim();
                        await ThreadHelper.JoinableTaskFactory.SwitchToMainThreadAsync();
                        string tempQueryPath = System.IO.Path.GetTempFileName() + ".linq";
                        string queryString = $"<Query Kind='Statements' />\r\n{currentSelection}\r\nresult.Dump();";
                        File.WriteAllText(tempQueryPath, queryString);

                        using Process process = new();
                        process.StartInfo = new ProcessStartInfo()
                        {
                            UseShellExecute = false,
                            CreateNoWindow = true,
                            WindowStyle = ProcessWindowStyle.Hidden,
                            FileName = fileLPRun7,
                            Arguments = $"-fx=6.0 {tempQueryPath}",
                            RedirectStandardError = true,
                            RedirectStandardOutput = true
                        };
                        process.Start();
                        queryResult = await process.StandardOutput.ReadToEndAsync();
                        process.WaitForExit();
                        await pane.ClearAsync();
                        LinqPadResults.Children.Clear();
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
                else
                {
                    var NothingSelectedResult = new TextBlock { Text = "No Active Document View or Linq Selection!", TextWrapping = TextWrapping.Wrap, Margin = new Thickness(0, 0, 0, 5) };
                    LinqPadResults.Children.Add(NothingSelectedResult);
                    await pane.WriteLineAsync("No Active Document View or Linq Selection!");
                }
            }).FireAndForget();
        }

        private async Task DoOutputWindowsAsync()
        {
            pane = await VS.Windows.CreateOutputWindowPaneAsync("LinqPad Dump");
            return;
        }

    }
}