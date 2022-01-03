using LINQPad;

using Microsoft.VisualStudio.Shell.Interop;

using System.IO;
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
                    await UpdateLinqPadDumpAsync(Hierarchy);
                }
            }).FireAndForget();
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            // VS.MessageBox.Show("VSIXRunSelectedQuery", "Button clicked");
            ThreadHelper.JoinableTaskFactory.RunAsync(async () =>
            {
                LinqPadResults.Children.Clear();
                await pane.ClearAsync();
                DocumentView docView = await VS.Documents.GetActiveDocumentViewAsync();
                if (docView?.TextView == null) return; //not a text window
                if (docView.TextView.Selection != null && !docView.TextView.Selection.IsEmpty)
                {
                    var currentSelection = docView.TextView.Selection.StreamSelectionSpan.GetText().Trim().Replace("  ", "").Trim();
                    await ThreadHelper.JoinableTaskFactory.SwitchToMainThreadAsync();
                    var queryResult = RunLinqPadQueryAsync(currentSelection);
                    var selectedQueryResult = new TextBlock { Text = await queryResult, TextWrapping = TextWrapping.Wrap, Margin = new Thickness(0, 0, 0, 5) };
                    LinqPadResults.Children.Add(selectedQueryResult);
                    var line = new Line { Margin = new Thickness(0, 0, 0, 20) };
                    LinqPadResults.Children.Add(line);
                }
            }).FireAndForget();


        }
        public async Task UpdateLinqPadDumpAsync(IVsHierarchy hierarchy)
        {
            await pane.WriteLineAsync($"2 * 25 = {(2 * 25).Dump<int>()}");
            await pane.WriteLineAsync($"2 * 50 = {(2 * 50).Dump<int>()}");
            await pane.WriteLineAsync($"2 * 75 = {(2 * 75).Dump<int>()}");
        }

        private async Task DoOutputWindowsAsync()
        {
            pane = await VS.Windows.CreateOutputWindowPaneAsync("LinqPad Dump");
            return;
        }

        private async System.Threading.Tasks.Task<string> RunLinqPadQueryAsync(string currentSelection)
        {
            //ThreadHelper.JoinableTaskFactory.RunAsync(async () =>
            //{
            string tempQueryPath = System.IO.Path.GetTempFileName() + ".linq";
            File.WriteAllText(tempQueryPath, $"<Query Kind='Statements' />\r\n{currentSelection}\r\nresult.Dump();");
            await ThreadHelper.JoinableTaskFactory.SwitchToMainThreadAsync();
            var queryResult1 = await Util.Run(tempQueryPath, QueryResultFormat.Text).AsStringAsync().Dump();
            await pane.WriteLineAsync($"{currentSelection} \r\n\r\nCurrent Selection Query Results are:\r\n{queryResult1}");
            return $"{currentSelection} \r\n\r\nCurrent Selection Query Results are:\r\n{queryResult1}";

            //}).FireAndForget();

        }
    }
}