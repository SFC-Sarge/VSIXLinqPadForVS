using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Emit;
using Microsoft.VisualStudio.Shell.Interop;
using Microsoft.VisualStudio.TextManager.Interop;

using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Shapes;

using VSIXLinqPadForVS.Options;

using OutputWindowPane = Community.VisualStudio.Toolkit.OutputWindowPane;
using Path = System.IO.Path;
using Process = System.Diagnostics.Process;
using Project = Community.VisualStudio.Toolkit.Project;

namespace VSIXLinqPadForVS.ToolWindows
{
    public partial class MyToolWindowControl : UserControl
    {
        OutputWindowPane _pane = null;
        public ToolWindowMessenger ToolWindowMessenger = null;
        public Project _activeProject;
        public string _activeFile;
        public string _myNamespace = null;
        public string queryResult = null;
        public string dirLPRun7 = null;
        public string fileLPRun7 = null;

        public MyToolWindowControl(Project activeProject, ToolWindowMessenger toolWindowMessenger)
        {
            ThreadHelper.ThrowIfNotOnUIThread();

            InitializeComponent();
            if (toolWindowMessenger == null)
            {
                toolWindowMessenger = new ToolWindowMessenger();
            }
            ToolWindowMessenger = toolWindowMessenger;
            toolWindowMessenger.MessageReceived += OnMessageReceived;
            VS.Events.SolutionEvents.OnAfterCloseSolution += OnAfterCloseSolution;

            dirLPRun7 = Path.GetDirectoryName(typeof(MyToolWindow).Assembly.Location);
            fileLPRun7 = Path.Combine(dirLPRun7, Constants.solutionToolWindowsFolderName, Constants.lPRun7Executable);
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
                await _pane.ClearAsync();
                LinqPadResults.Children.Clear();
            }).FireAndForget();
        }

        public async Task RunLinqStatementAsync()
        {
            await ThreadHelper.JoinableTaskFactory.SwitchToMainThreadAsync();
            ThreadHelper.JoinableTaskFactory.RunAsync(async () =>
            {
                await _pane.ClearAsync();
                LinqPadResults.Children.Clear();
                DocumentView docView = await VS.Documents.GetActiveDocumentViewAsync();
                _activeFile = docView?.Document?.FilePath;
                _activeProject = await VS.Solutions.GetActiveProjectAsync();
                _myNamespace = _activeProject.Name;
                if (docView?.TextView == null)
                {
                    TextBlock NothingSelectedResult = new() { Text = Constants.noActiveDocument, TextWrapping = TextWrapping.Wrap, Margin = new Thickness(0, 0, 0, 5) };
                    LinqPadResults.Children.Add(NothingSelectedResult);
                    await _pane.WriteLineAsync(Constants.noActiveDocument);
                    return;
                }
                if (docView.TextView.Selection != null && !docView.TextView.Selection.IsEmpty)
                {
                    await _pane.WriteLineAsync(Constants.runningSelectQuery);
                    TextBlock runningQueryResult = new() { Text = Constants.runningSelectQuery, TextWrapping = TextWrapping.Wrap, Margin = new Thickness(0, 0, 0, 5) };
                    LinqPadResults.Children.Add(runningQueryResult);
                    await ThreadHelper.JoinableTaskFactory.SwitchToMainThreadAsync();
                    try
                    {
                        string currentSelection = docView.TextView.Selection.StreamSelectionSpan.GetText().Trim().Replace("  ", "").Trim();
                        //try
                        //{
                        //    //Move comment parameter to calling program once CompileResult method call is added to it.
                        //    string comment = "Aggregated numbers by multiplication:";

                        //    CompileResult(currentSelection, comment);
                        //}
                        //catch (Exception ex)
                        //{
                        //    TextBlock exceptionResult = new() { Text = ex.Message, TextWrapping = TextWrapping.Wrap, Margin = new Thickness(0, 0, 0, 5) };
                        //    LinqPadResults.Children.Add(exceptionResult);
                        //    await _pane.WriteLineAsync($"{Constants.exceptionIn} {fileLPRun7} {Constants.exceptionCall} {exceptionResult.Text}");

                        //}
                        string tempQueryPath = $"{Path.GetTempFileName()}{Constants.LinqExt}";
                        string queryString = $"{Constants.queryKindStatement}\r\n{currentSelection}\r\n{Constants.resultDump};".Trim();
                        File.WriteAllText(tempQueryPath, queryString);

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
                        await _pane.ClearAsync();
                        LinqPadResults.Children.Clear();
                        if (AdvancedOptions.Instance.UseLinqPadDumpWindow == true)
                        {
                            //await _pane.WriteLineAsync($"{currentSelection} \r\n\r\n{Constants.currentSelectionQuery} = {queryResult}");
                            await _pane.WriteLineAsync($"{Constants.currentSelectionQuery} = {queryResult}");
                        }
                        if (AdvancedOptions.Instance.EnableToolWindowResults == true)
                        {
                            //TextBlock selectedQueryResult = new TextBlock { Text = $"{currentSelection} \r\n\r\n{Constants.currentSelectionQuery} = {queryResult}", TextWrapping = TextWrapping.Wrap, Margin = new Thickness(0, 0, 0, 5) };
                            TextBlock selectedQueryResult = new() { Text = $"{Constants.currentSelectionQuery} = {queryResult}", TextWrapping = TextWrapping.Wrap, Margin = new Thickness(0, 0, 0, 5) };
                            LinqPadResults.Children.Add(selectedQueryResult);
                            Line line = new() { Margin = new Thickness(0, 0, 0, 20) };
                            LinqPadResults.Children.Add(line);
                        }
                        tempQueryPath = $"{Path.GetTempFileName()}{Constants.LinqExt}";
                        //System.IO.File.WriteAllText(tempQueryPath, $"{currentSelection} \r\n\r\n{Constants.currentSelectionQuery} = {queryResult}".Trim());
                        File.WriteAllText(tempQueryPath, $"{currentSelection}".Trim());

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
                        TextBlock exceptionResult = new() { Text = ex.Message, TextWrapping = TextWrapping.Wrap, Margin = new Thickness(0, 0, 0, 5) };
                        LinqPadResults.Children.Add(exceptionResult);
                        await _pane.WriteLineAsync($"{Constants.exceptionIn} {fileLPRun7} {Constants.exceptionCall} {exceptionResult.Text}");
                    }
                }
                else
                {
                    TextBlock NothingSelectedResult = new() { Text = Constants.noActiveDocument, TextWrapping = TextWrapping.Wrap, Margin = new Thickness(0, 0, 0, 5) };
                    LinqPadResults.Children.Add(NothingSelectedResult);
                    await _pane.WriteLineAsync(Constants.noActiveDocument);
                }
            }).FireAndForget();
        }
        public async Task RunLinqMethodAsync()
        {
            await ThreadHelper.JoinableTaskFactory.SwitchToMainThreadAsync();
            ThreadHelper.JoinableTaskFactory.RunAsync(async () =>
            {
                await _pane.ClearAsync();
                LinqPadResults.Children.Clear();
                DocumentView docView = await VS.Documents.GetActiveDocumentViewAsync();
                _activeFile = docView?.Document?.FilePath;
                _activeProject = await VS.Solutions.GetActiveProjectAsync();
                _myNamespace = _activeProject.Name;
                if (docView?.TextView == null)
                {
                    TextBlock NothingSelectedResult = new() { Text = Constants.noActiveDocumentMethod, TextWrapping = TextWrapping.Wrap, Margin = new Thickness(0, 0, 0, 5) };
                    LinqPadResults.Children.Add(NothingSelectedResult);
                    await _pane.WriteLineAsync(Constants.noActiveDocumentMethod);
                    return;
                }
                if (docView.TextView.Selection != null && !docView.TextView.Selection.IsEmpty)
                {
                    await _pane.WriteLineAsync(Constants.runningSelectQueryMethod);
                    TextBlock runningQueryResult = new() { Text = Constants.runningSelectQueryMethod, TextWrapping = TextWrapping.Wrap, Margin = new Thickness(0, 0, 0, 5) };
                    LinqPadResults.Children.Add(runningQueryResult);

                    try
                    {
                        string currentSelection = docView.TextView.Selection.StreamSelectionSpan.GetText().Trim().Replace("  ", "").Trim();
                        await ThreadHelper.JoinableTaskFactory.SwitchToMainThreadAsync();
                        string tempQueryPath = $"{Path.GetTempFileName()}{Constants.LinqExt}";
                        string methodName = currentSelection.Substring(0, currentSelection.IndexOf("\r"));
                        string methodNameComplete = methodName.Substring(methodName.LastIndexOf(" ") + 1, methodName.LastIndexOf(")") - methodName.LastIndexOf(" "));
                        string methodCallLine = "{\r\n" + $"{methodNameComplete}" + ";\r\n}";
                        string queryString = $"{Constants.queryKindMethod}\r\nvoid Main()\r\n{methodCallLine}\r\n{currentSelection}".Trim();
                        File.WriteAllText(tempQueryPath, queryString);
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
                        await _pane.ClearAsync();
                        LinqPadResults.Children.Clear();
                        if (AdvancedOptions.Instance.UseLinqPadDumpWindow == true)
                        {
                            //await _pane.WriteLineAsync($"{queryString} \r\n\r\n{Constants.currentSelectionQueryMethod} =\r\n{queryResult}");
                            await _pane.WriteLineAsync($"{Constants.currentSelectionQueryMethod} =\r\n{queryResult}");
                        }
                        if (AdvancedOptions.Instance.EnableToolWindowResults == true)
                        {
                            //TextBlock selectedQueryResult = new TextBlock { Text = $"{queryString} \r\n\r\n{Constants.currentSelectionQueryMethod} = {queryResult}", TextWrapping = TextWrapping.Wrap, Margin = new Thickness(0, 0, 0, 5) };
                            TextBlock selectedQueryResult = new() { Text = $"{Constants.currentSelectionQueryMethod} = {queryResult}", TextWrapping = TextWrapping.Wrap, Margin = new Thickness(0, 0, 0, 5) };
                            LinqPadResults.Children.Add(selectedQueryResult);
                            Line line = new() { Margin = new Thickness(0, 0, 0, 20) };
                            LinqPadResults.Children.Add(line);
                        }

                        tempQueryPath = $"{Path.GetTempFileName()}{Constants.LinqExt}";
                        //System.IO.File.WriteAllText(tempQueryPath, $"{queryString} \r\n\r\n{Constants.currentSelectionQueryMethod} = {queryResult}".Trim());
                        File.WriteAllText(tempQueryPath, $"{queryString}".Trim());

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
                        TextBlock exceptionResult = new() { Text = ex.Message, TextWrapping = TextWrapping.Wrap, Margin = new Thickness(0, 0, 0, 5) };
                        LinqPadResults.Children.Add(exceptionResult);
                        await _pane.WriteLineAsync($"{Constants.exceptionIn} {fileLPRun7} {Constants.exceptionCall} {exceptionResult.Text}");
                    }
                }
                else
                {
                    TextBlock NothingSelectedResult = new() { Text = Constants.noActiveDocumentMethod, TextWrapping = TextWrapping.Wrap, Margin = new Thickness(0, 0, 0, 5) };
                    LinqPadResults.Children.Add(NothingSelectedResult);
                    await _pane.WriteLineAsync(Constants.noActiveDocumentMethod);
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
                await _pane.ClearAsync();
                LinqPadResults.Children.Clear();
                DocumentView docView = await VS.Documents.GetActiveDocumentViewAsync();
                _activeFile = docView?.Document?.FilePath;
                _activeProject = await VS.Solutions.GetActiveProjectAsync();
                _myNamespace = _activeProject.Name;
                if (docView?.TextView == null)
                {
                    TextBlock NothingSelectedResult = new() { Text = Constants.noActiveDocument, TextWrapping = TextWrapping.Wrap, Margin = new Thickness(0, 0, 0, 5) };
                    LinqPadResults.Children.Add(NothingSelectedResult);
                    await _pane.WriteLineAsync(Constants.noActiveDocument);
                    return;
                }
                if (docView.TextView.Selection != null && !docView.TextView.Selection.IsEmpty)
                {
                    await _pane.WriteLineAsync(Constants.runningSelectQuery);
                    TextBlock runningQueryResult = new() { Text = Constants.runningSelectQuery, TextWrapping = TextWrapping.Wrap, Margin = new Thickness(0, 0, 0, 5) };
                    LinqPadResults.Children.Add(runningQueryResult);

                    try
                    {
                        await ThreadHelper.JoinableTaskFactory.SwitchToMainThreadAsync();
                        string currentSelection = docView.TextView.Selection.StreamSelectionSpan.GetText().Trim().Replace("  ", "").Trim();
                        string tempQueryPath = $"{Path.GetTempFileName()}{Constants.LinqExt}";
                        if (!currentSelection.StartsWith("<Query Kind="))
                        {
                            string methodName = currentSelection.Substring(0, currentSelection.IndexOf("\r"));
                            string methodNameComplete = methodName.Substring(methodName.LastIndexOf(" ") + 1, methodName.LastIndexOf(")") - methodName.LastIndexOf(" "));
                            string methodCallLine = "{\r\n" + $"{methodNameComplete}" + ";\r\n}";
                            string queryString = $"{Constants.queryKindMethod}\r\nvoid Main()\r\n{methodCallLine}\r\n{currentSelection}".Trim();
                            File.WriteAllText(tempQueryPath, queryString);
                        }
                        else if (currentSelection.StartsWith("<Query Kind="))
                        {
                            File.WriteAllText(tempQueryPath, currentSelection);
                        }
                        else
                        {
                            string queryString = $"{Constants.queryKindStatement}\r\n{currentSelection}\r\n{Constants.resultDump};";
                            File.WriteAllText(tempQueryPath, queryString);
                        }

                        using System.Diagnostics.Process process = new();
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

                        await _pane.ClearAsync();
                        LinqPadResults.Children.Clear();
                        if (AdvancedOptions.Instance.UseLinqPadDumpWindow == true)
                        {
                            //await _pane.WriteLineAsync($"{currentSelection} \r\n\r\n{Constants.currentSelectionQuery} = {queryResult}");
                            await _pane.WriteLineAsync($"{currentSelection} \r\n\r\n{Constants.currentSelectionQuery} = {queryResult}");
                        }
                        if (AdvancedOptions.Instance.EnableToolWindowResults == true)
                        {
                            //TextBlock selectedQueryResult = new TextBlock { Text = $"{currentSelection} \r\n\r\n{Constants.currentSelectionQuery} = {queryResult}", TextWrapping = TextWrapping.Wrap, Margin = new Thickness(0, 0, 0, 5) };
                            TextBlock selectedQueryResult = new() { Text = $"{Constants.currentSelectionQuery} = {queryResult}", TextWrapping = TextWrapping.Wrap, Margin = new Thickness(0, 0, 0, 5) };
                            LinqPadResults.Children.Add(selectedQueryResult);
                            Line line = new() { Margin = new Thickness(0, 0, 0, 20) };
                            LinqPadResults.Children.Add(line);
                        }
                        tempQueryPath = $"{Path.GetTempFileName()}{Constants.LinqExt}";
                        //System.IO.File.WriteAllText(tempQueryPath, $"{currentSelection} \r\n\r\n{Constants.currentSelectionQuery} = {queryResult}".Trim());
                        File.WriteAllText(tempQueryPath, $"{currentSelection}".Trim());

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
                        TextBlock exceptionResult = new() { Text = ex.Message, TextWrapping = TextWrapping.Wrap, Margin = new Thickness(0, 0, 0, 5) };
                        LinqPadResults.Children.Add(exceptionResult);
                        await _pane.WriteLineAsync($"{Constants.exceptionIn} {fileLPRun7} {Constants.exceptionCall} {exceptionResult.Text}");
                    }
                }
                else
                {
                    TextBlock NothingSelectedResult = new() { Text = Constants.noActiveDocument, TextWrapping = TextWrapping.Wrap, Margin = new Thickness(0, 0, 0, 5) };
                    LinqPadResults.Children.Add(NothingSelectedResult);
                    await _pane.WriteLineAsync(Constants.noActiveDocument);
                }
            }).FireAndForget();
        }

        private async Task DoOutputWindowsAsync()
        {
            _pane = await VS.Windows.CreateOutputWindowPaneAsync(Constants.linpPadDump);
            return;
        }
        public async Task<string> GetTemplateFilePathAsync(Project project, string nameSpace, string file)
        {
            var name = Path.GetFileName(file);
            var safeName = name.StartsWith(".") ? name : Path.GetFileNameWithoutExtension(file);
            //var relative = PackageUtilities.MakeRelative(project.GetRootFolder(), System.IO.Path.GetDirectoryName(file) ?? "");

            var templateFile = Path.GetDirectoryName(file);

            var template = await ReplaceTokensAsync(project, safeName, nameSpace, templateFile);
            return NormalizeLineEndings(template);
        }

        private async Task<string> ReplaceTokensAsync(Project project, string name, string nameSpace, string templateFile)
        {
            if (string.IsNullOrEmpty(templateFile))
            {
                return templateFile;
            }

            using (var reader = new StreamReader(templateFile))
            {
                var content = await reader.ReadToEndAsync();

                return content.Replace("{namespace}", nameSpace)
                              .Replace("{itemname}", name);
            }
        }
        private string NormalizeLineEndings(string content)
        {
            if (string.IsNullOrEmpty(content))
            {
                return content;
            }

            return Regex.Replace(content, @"\r\n|\n\r|\n|\r", "\r\n");
        }
        private void CompileResult(string selectedCode, string comment)
        {
            //string code = "using System; using System.Linq; class Program{ private static void Main(string[] args) { Sample_Aggregate_Lambda_Simple(); } private static void Sample_Aggregate_Lambda_Simple() { var result = new int[] { 1, 2, 3, 4, 5 }.Aggregate((a, b) => a * b); Console.WriteLine(\"Aggregated numbers by multiplication: \" + result); } }";
            //Move code parameter to calling program once CompileResult method call is added to it.
            string code = $"using System; using System.Linq; using System.Linq.Expressions; class Program{{ private static void Main() {{ GetResults(); }} private static void GetResults() {{ {selectedCode} Console.WriteLine(\"{comment}\", result); }} }}";

            var syntaxTree = CSharpSyntaxTree.ParseText(code, new CSharpParseOptions(LanguageVersion.CSharp10));
            string basePath = Path.GetDirectoryName(typeof(System.Runtime.GCSettings).GetTypeInfo().Assembly.Location);

            var root = syntaxTree.GetRoot() as CompilationUnitSyntax;
            var references = root.Usings;

            var referencePaths = new List<string> {
                    typeof(object).GetTypeInfo().Assembly.Location,
                    typeof(Console).GetTypeInfo().Assembly.Location,
                    Path.Combine(basePath, "System.Runtime.dll"),
                    Path.Combine(basePath, "System.Runtime.Extensions.dll"),
                    Path.Combine(basePath, "mscorlib.dll"),
                    Path.Combine(basePath, "Microsoft.CSharp.dll"),
                    Path.Combine(basePath, "System.Linq.Expressions.dll"),
                    Path.Combine(basePath, "netstandard.dll")
                };

            referencePaths.AddRange(references.Select(x => Path.Combine(basePath, $"{x.Name}.dll")));

            var executableReferences = new List<PortableExecutableReference>();

            foreach (var reference in referencePaths)
            {
                if (File.Exists(reference))
                {
                    executableReferences.Add(MetadataReference.CreateFromFile(reference));
                }
            }

            var compilation = CSharpCompilation.Create(Path.GetRandomFileName(), new[] { syntaxTree }, executableReferences, new CSharpCompilationOptions(OutputKind.WindowsApplication));

            using var memoryStream = new MemoryStream();
            EmitResult compilationResult = compilation.Emit(memoryStream);

            if (!compilationResult.Success)
            {
                var errors = compilationResult.Diagnostics.Where(diagnostic =>
                   diagnostic.IsWarningAsError ||
                   diagnostic.Severity == DiagnosticSeverity.Error)?.ToList() ?? new List<Diagnostic>();

                foreach (var error in errors)
                {
                    Console.WriteLine(error.GetMessage());
                }
            }
            else
            {
                memoryStream.Seek(0, SeekOrigin.Begin);
                //var assemblyContext = AssemblyLoadContext.Default.LoadFromAssemblyPath(Path.GetRandomFileName());
                //AssemblyLoadContext.Default.LoadFromAssemblyPath(Path.GetRandomFileName());
                //var assembly = AssemblyLoadContext.Default.LoadFromStream(memoryStream);
                ////AssemblyLoadContext assemblyContext = new AssemblyLoadContext(Path.GetRandomFileName(), true);
                ////Assembly assembly = assemblyContext.LoadFromStream(memoryStream);

                //var entryPoint = compilation.GetEntryPoint(CancellationToken.None);
                //var type = assembly.GetType($"{entryPoint.ContainingNamespace.MetadataName}.{entryPoint.ContainingType.MetadataName}");
                //var instance = assembly.CreateInstance(type.FullName);
                //var method = type.GetMethod(entryPoint.MetadataName, BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Static);
                //method.Invoke(instance, BindingFlags.InvokeMethod, Type.DefaultBinder, new object[] { new string[] { "abc" } }, null);

                //assemblyContext.Unload();
            }
        }

    }
}