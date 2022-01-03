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
                await UpdateLinqPadDumpAsync(Hierarchy);
            }).FireAndForget();
            VS.Events.SelectionEvents.SelectionChanged += SelectionChanged;
            VS.Events.DocumentEvents.BeforeDocumentWindowShow += BeforeDocumentWindowShow;
            VS.Events.SolutionEvents.OnAfterCloseSolution += OnAfterCloseSolution;

        }

        //private async void Button1_Click(object sender, RoutedEventArgs e)
        //{
        //    var result = await VS.MessageBox.ShowAsync("LinqPadForVS", "Button clicked");
        //    await pane.WriteLineAsync($"2 * 25 = {(2 * 25).Dump<int>()}");
        //    await pane.WriteLineAsync($"2 * 50 = {(2 * 50).Dump<int>()}");
        //    await pane.WriteLineAsync($"2 * 75 = {(2 * 75).Dump<int>()}");
        //    await pane.WriteLineAsync($"button1 was clicked: Result: {result}");
        //    await pane.WriteLineAsync($"Does any of the names: {names[0]}, {names[1]}, {names[2]}, or {names[3]} StartsWith(B)? {names.Any(n => n.StartsWith("B")).Dump()}");
        //}
        private async Task DoOutputWindowsAsync()
        {
            pane = await VS.Windows.CreateOutputWindowPaneAsync("LinqPad Dump");
            return;
        }
        public async Task UpdateLinqPadDumpAsync(IVsHierarchy hierarchy)
        {
            LinqPadResults.Children.Clear();
            await ThreadHelper.JoinableTaskFactory.SwitchToMainThreadAsync();
            //await pane.WriteLineAsync($"2 * 25 = {(2 * 25).Dump<int>()}");
            //await pane.WriteLineAsync($"2 * 50 = {(2 * 50).Dump<int>()}");
            //await pane.WriteLineAsync($"2 * 75 = {(2 * 75).Dump<int>()}");
            DocumentView docView = await VS.Documents.GetActiveDocumentViewAsync();
            if (docView?.TextView == null) return; //not a text window
            if (docView.TextView.Selection != null && !docView.TextView.Selection.IsEmpty)
            {
                var currentSelection = docView.TextView.Selection;
                var tempFilePath = System.IO.Path.GetTempFileName() + ".linq";
                //File.WriteAllText(tempFilePath, currentSelection.StreamSelectionSpan.GetText());
                File.WriteAllText(tempFilePath, "Environment.MachineName.ToUpper()");
                string result = null;
                //RunLinqPadQuery(currentSelection.StreamSelectionSpan.GetText());
                //ThreadHelper.JoinableTaskFactory.RunAsync(async () =>
                //{
                //    result = await Util.Run(@"C:\temp\Aggregate_Lambda_Simple.linq", QueryResultFormat.Text).AsStringAsync().Dump();
                //    //var selectedQueryResult = new TextBlock { Text = (await Util.Run(@"C:\temp\Aggregate_Lambda_Simple.linq", QueryResultFormat.Text).AsStringAsync().Dump()), TextWrapping = TextWrapping.Wrap, Margin = new Thickness(0, 0, 0, 5) };
                //}).GetAwaiter();
                var testResult = new TextBlock { Text = "Hello", TextWrapping = TextWrapping.Wrap, Margin = new Thickness(0, 0, 0, 5) };
                LinqPadResults.Children.Add(testResult);
               await Task.Run(async () =>
                {

                    //result = await Util.Run(@"C:\temp\Aggregate_Lambda_Simple.linq", QueryResultFormat.Text).AsStringAsync().Dump();
                    ThreadHelper.JoinableTaskFactory.RunAsync(async () =>
                    {
                        result = await Util.Run(@"C:\temp\Aggregate_Lambda_Simple.linq", QueryResultFormat.Text).AsStringAsync().Dump();
                        //var selectedQueryResult = new TextBlock { Text = (await Util.Run(@"C:\temp\Aggregate_Lambda_Simple.linq", QueryResultFormat.Text).AsStringAsync().Dump()), TextWrapping = TextWrapping.Wrap, Margin = new Thickness(0, 0, 0, 5) };
                    }).FireAndForget();
                    await ThreadHelper.JoinableTaskFactory.SwitchToMainThreadAsync();

                });
                var selectedQueryResult = new TextBlock { Text = result, TextWrapping = TextWrapping.Wrap, Margin = new Thickness(0, 0, 0, 5) };
                LinqPadResults.Children.Add(selectedQueryResult);

                var line = new Line { Margin = new Thickness(0, 0, 0, 20) };
                LinqPadResults.Children.Add(line);
            }

            //var text = new TextBlock { Text = $"2 * 25 = {(2 * 25).Dump<int>()}", TextWrapping = TextWrapping.Wrap, Margin = new Thickness(0, 0, 0, 5) };
            //LinqPadResults.Children.Add(text);

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
        //private void AfterDocumentWindowOpened(DocumentView docView)
        //{
        //    ThreadHelper.JoinableTaskFactory.RunAsync(async () =>
        //    {
        //        if (docView?.Document?.FilePath != _activeFile)
        //        {
        //            _activeFile = docView?.Document?.FilePath;
        //            var ext = System.IO.Path.GetExtension(docView?.Document?.FilePath);
        //            //await UpdateFilesAsync(docView.TextBuffer.ContentType, ext);
        //            var selectedText = docView.TextBuffer.CurrentSnapshot;
        //            fileExtension = ext;
        //            //await UpdateFilesAsync(ext);
        //            await UpdateLinqPadDumpAsync(Hierarchy);

        //        }

        //    }).FireAndForget();
        //}

        private void RunLinqPadQuery(string currentSelection)
        {
            ThreadHelper.JoinableTaskFactory.RunAsync(async () =>
             {
                 var tempFilePath = System.IO.Path.GetTempFileName() + ".linq";
                 File.WriteAllText(tempFilePath, currentSelection);
                 var run = Util.Run(tempFilePath, QueryResultFormat.Text);
                 //var run = Util.Run(tempFilePath, QueryResultFormat.Text);
                 queryResult = await run.AsStringAsync();
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
    }
}