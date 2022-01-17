﻿global using Community.VisualStudio.Toolkit;
global using Microsoft.VisualStudio.Shell;
global using System;
global using Task = System.Threading.Tasks.Task;
using Microsoft.VisualStudio;
using Microsoft.VisualStudio.Shell.Interop;
using System.ComponentModel.Design;
using System.Runtime.InteropServices;
using System.Threading;
using VSIXLinqPadForVS.Commands;
using VSIXLinqPadForVS.Editor;
using VSIXLinqPadForVS.Options;
using VSIXLinqPadForVS.ToolWindows;
using VSIXLinqPadForVS.LinqEditor;

namespace VSIXLinqPadForVS
{
    [PackageRegistration(UseManagedResourcesOnly = true, AllowsBackgroundLoading = true)]
    [InstalledProductRegistration(Vsix.Name, Vsix.Description, Vsix.Version)]
    [ProvideToolWindow(typeof(MyToolWindow.Pane), Style = VsDockStyle.Tabbed, Window = WindowGuids.SolutionExplorer)]
    [ProvideToolWindowVisibility(typeof(MyToolWindow.Pane), VSConstants.UICONTEXT.SolutionHasSingleProject_string)]
    [ProvideToolWindowVisibility(typeof(MyToolWindow.Pane), VSConstants.UICONTEXT.SolutionHasMultipleProjects_string)]
    [ProvideToolWindowVisibility(typeof(MyToolWindow.Pane), VSConstants.UICONTEXT.NoSolution_string)]
    [ProvideToolWindowVisibility(typeof(MyToolWindow.Pane), VSConstants.UICONTEXT.EmptySolution_string)]
    [ProvideFileIcon(Constants.PkgDefExt, "KnownMonikers.RegistrationScript")]
    [ProvideFileIcon(Constants.PkgUndefExt, "KnownMonikers.RegistrationScript")]
    [ProvideMenuResource("Menus.ctmenu", 1)]
    [Guid(PackageGuids.VSIXLinqPadForVSString)]

    [ProvideLanguageService(typeof(LanguageFactory), Constants.PkgdefLanguageName, 0, ShowHotURLs = false, DefaultToNonHotURLs = true, EnableLineNumbers = true, EnableAsyncCompletion = true, EnableCommenting = true, ShowCompletion = true, AutoOutlining = true, CodeSense = true)]
    [ProvideLanguageEditorOptionPage(typeof(OptionsProvider.AdvancedOptions), Constants.PkgdefLanguageName, "", "Advanced", null, 0)]
    [ProvideLanguageExtension(typeof(LanguageFactory), Constants.PkgDefExt)]
    [ProvideLanguageExtension(typeof(LanguageFactory), Constants.PkgUndefExt)]

    [ProvideEditorFactory(typeof(LanguageFactory), 738, CommonPhysicalViewAttributes = (int)__VSPHYSICALVIEWATTRIBUTES.PVA_SupportsPreview, TrustLevel = __VSEDITORTRUSTLEVEL.ETL_AlwaysTrusted)]
    [ProvideEditorExtension(typeof(LanguageFactory), Constants.PkgDefExt, 65535, NameResourceID = 738)]
    [ProvideEditorExtension(typeof(LanguageFactory), Constants.PkgUndefExt, 65535, NameResourceID = 738)]
    [ProvideEditorLogicalView(typeof(LanguageFactory), VSConstants.LOGVIEWID.TextView_string, IsTrusted = true)]
    [ProvideEditorExtension(typeof(LanguageFactory), Constants.PkgDefExt, 50)]

    [ProvideLanguageService(typeof(LinqLanguageFactory), Constants.LinqLanguageName, 0, ShowHotURLs = false, DefaultToNonHotURLs = true, EnableLineNumbers = true, EnableAsyncCompletion = true, EnableCommenting = true, ShowCompletion = true, AutoOutlining = true, CodeSense = true)]
    [ProvideLanguageEditorOptionPage(typeof(OptionsProvider.AdvancedOptions), Constants.LinqLanguageName, "", "Advanced", null, 0)]
    [ProvideLanguageExtension(typeof(LinqLanguageFactory), Constants.LinqExt)]

    [ProvideEditorFactory(typeof(LinqLanguageFactory), 739, CommonPhysicalViewAttributes = (int)__VSPHYSICALVIEWATTRIBUTES.PVA_SupportsPreview, TrustLevel = __VSEDITORTRUSTLEVEL.ETL_AlwaysTrusted)]
    [ProvideEditorExtension(typeof(LinqLanguageFactory), Constants.LinqExt, 65536, NameResourceID = 739)]
    [ProvideEditorLogicalView(typeof(LinqLanguageFactory), VSConstants.LOGVIEWID.TextView_string, IsTrusted = true)]

    public sealed class VSIXLinqPadForVSPackage : ToolkitPackage
    {
        protected override async Task InitializeAsync(CancellationToken cancellationToken, IProgress<ServiceProgressData> progress)
        {
            LanguageFactory language = new(this);
            RegisterEditorFactory(language);
            LinqLanguageFactory Linqlanguage = new(this);
            RegisterEditorFactory(Linqlanguage);

            AddService(typeof(ToolWindowMessenger), (_, _, _) => Task.FromResult<object>(new ToolWindowMessenger()));
            ((IServiceContainer)this).AddService(typeof(LanguageFactory), language, true);
            ((IServiceContainer)this).AddService(typeof(LinqLanguageFactory), Linqlanguage, true);

            await this.RegisterCommandsAsync();
            await Formatting.InitializeAsync();
            await Commenting2.InitializeAsync();
            await LinqFormatting.InitializeAsync();
            await LinqCommenting2.InitializeAsync();

            this.RegisterToolWindows();
        }
    }
}