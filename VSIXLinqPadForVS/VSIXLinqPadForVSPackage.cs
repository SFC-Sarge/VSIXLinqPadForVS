global using Community.VisualStudio.Toolkit;

global using Microsoft.VisualStudio.Shell;

global using System;

global using Task = System.Threading.Tasks.Task;

using Microsoft.VisualStudio;
using Microsoft.VisualStudio.Shell.Interop;
using Microsoft.Win32;

using System.Runtime.InteropServices;
using System.Threading;

namespace VSIXLinqPadForVS
{
    [PackageRegistration(UseManagedResourcesOnly = true, AllowsBackgroundLoading = true)]
    [InstalledProductRegistration(Vsix.Name, Vsix.Description, Vsix.Version)]
    [ProvideToolWindow(typeof(MyToolWindow.Pane), Style = VsDockStyle.Tabbed, Window = WindowGuids.SolutionExplorer)]
    [ProvideToolWindowVisibility(typeof(MyToolWindow.Pane), VSConstants.UICONTEXT.SolutionHasSingleProject_string)]
    [ProvideToolWindowVisibility(typeof(MyToolWindow.Pane), VSConstants.UICONTEXT.SolutionHasMultipleProjects_string)]
    [ProvideToolWindowVisibility(typeof(MyToolWindow.Pane), VSConstants.UICONTEXT.NoSolution_string)]
    [ProvideToolWindowVisibility(typeof(MyToolWindow.Pane), VSConstants.UICONTEXT.EmptySolution_string)]
    [ProvideMenuResource("Menus.ctmenu", 1)]
    [ProvideService(typeof(ToolWindowMessenger), IsAsyncQueryable = true)]
    [ProvideLanguageEditorOptionPage(typeof(OptionsProvider.AdvancedOptions), Constants.LanguageName, "", "Advanced", null, 0)]
    [ProvideFileIcon(Constants.FileExtension, "KnownMonikers.RegistrationScript")]
    [ProvideLanguageService(typeof(LinqEditor), Constants.LanguageName, 0, ShowHotURLs = false, DefaultToNonHotURLs = true, EnableLineNumbers = true, EnableAsyncCompletion = true, EnableCommenting = true, ShowCompletion = true)]
    [ProvideLanguageExtension(typeof(LinqEditor), Constants.FileExtension)]
    [ProvideEditorExtension(typeof(LinqEditor), Constants.FileExtension, 50)]
    [ProvideEditorFactory(typeof(LinqEditor), 0, false, CommonPhysicalViewAttributes = (int)__VSPHYSICALVIEWATTRIBUTES.PVA_SupportsPreview, TrustLevel = __VSEDITORTRUSTLEVEL.ETL_AlwaysTrusted)]
    [ProvideEditorLogicalView(typeof(LinqEditor), VSConstants.LOGVIEWID.TextView_string, IsTrusted = true)]
    [Guid(PackageGuids.VSIXLinqPadForVSString)]
    public sealed class VSIXLinqPadForVSPackage : ToolkitPackage
    {
        internal static LinqEditor LinqEditor { get; private set; }

        protected override async Task InitializeAsync(CancellationToken cancellationToken, IProgress<ServiceProgressData> progress)
        {
            LinqEditor = new(this);
            RegisterEditorFactory(LinqEditor);

            //SetInternetExplorerRegistryKey();

            AddService(typeof(ToolWindowMessenger), (_, _, _) => Task.FromResult<object>(new ToolWindowMessenger()));
            await this.RegisterCommandsAsync();

            this.RegisterToolWindows();
        }
        // This is to enable DPI scaling in the preview browser instance
        private static void SetInternetExplorerRegistryKey()
        {
            try
            {
                using (RegistryKey featureControl = Registry.CurrentUser.OpenSubKey("Software\\Microsoft\\Internet Explorer\\Main\\FeatureControl", true))
                using (RegistryKey pixel = featureControl.CreateSubKey("FEATURE_96DPI_PIXEL", true, RegistryOptions.Volatile))
                {
                    pixel.SetValue("devenv.exe", 1, RegistryValueKind.DWord);
                }
            }
            catch (Exception ex)
            {
                ex.Log();
            }
        }
    }
}