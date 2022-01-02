using System.Linq;
using System.Windows;
using System.Windows.Controls;
using Community.VisualStudio.Toolkit;
using Microsoft.Internal.VisualStudio.PlatformUI;
using Microsoft.VisualStudio.Shell.Interop;
using System.Windows.Shapes;
using LINQPad;
using Microsoft.VisualStudio.VCProjectEngine;

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

        //string[] names = { "Bob", "Ned", "Amy", "Bill" };
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
            await pane.WriteLineAsync($"2 * 25 = {(2 * 25).Dump<int>()}");
            await pane.WriteLineAsync($"2 * 50 = {(2 * 50).Dump<int>()}");
            await pane.WriteLineAsync($"2 * 75 = {(2 * 75).Dump<int>()}");

            //foreach (ProjectType pt in _projectTypes.ProjectTypes)
            //{
            //    var capability = pt.CapabilityExpression;

            //    if ((_activeProject == null && !string.IsNullOrEmpty(capability)) || _activeProject != null && string.IsNullOrEmpty(capability))
            //    {
            //        continue;
            //    }
            //    else if (!string.IsNullOrEmpty(capability) && hierarchy?.IsCapabilityMatch(capability) == false)
            //    {
            //        continue;
            //    }
            //    if (General.Instance.CreateCapabilitiesFile)
            //    {
            //        //The following capabilities line allows you to check the projects capabilities so they can be added to projectTypes.json.
            //        if (!string.IsNullOrEmpty(CapabilityValues) && !string.IsNullOrEmpty(pt.CapabilitiesFileName))
            //        {
            //            WriteCapabilitiesToFile(CapabilityValues, pt.CapabilitiesFileName);
            //        }
            //    }

            var text = new TextBlock { Text = $"2 * 25 = {(2 * 25).Dump<int>()}", TextWrapping = TextWrapping.Wrap, Margin = new Thickness(0, 0, 0, 5) };
            LinqPadResults.Children.Add(text);

            //foreach (Link link in pt.Links)
            //{
            //    var h = new Hyperlink
            //    {
            //        NavigateUri = new Uri(link.Url)
            //    };

            //    h.RequestNavigate += OnRequestNavigate;
            //    ProjectTypes.MaxWidth = pt.Text.ToString().Length + 200;
            //    h.Inlines.Add(link.Text);
            //    var textBlock = new TextBlock { Text = "- ", Margin = new Thickness(15, 0, 0, 0) };
            //    textBlock.Inlines.Add(h);

            //    ProjectTypes.Children.Add(textBlock);
            //}

            var line = new Line { Margin = new Thickness(0, 0, 0, 20) };
            LinqPadResults.Children.Add(line);
                // read settings
                //if (General.Instance.MultipleProjectsOption)
                //{
                //    continue;
                //}
                //break;
            //}
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


    }
}