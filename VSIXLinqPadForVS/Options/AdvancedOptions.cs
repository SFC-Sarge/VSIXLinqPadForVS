using System.ComponentModel;
using System.Runtime.InteropServices;

namespace VSIXLinqPadForVS
{
    internal partial class OptionsProvider
    {
        [ComVisible(true)]
        public class AdvancedOptions : BaseOptionPage<VSIXLinqPadForVS.AdvancedOptions> { }
    }

    public class AdvancedOptions : BaseOptionModel<AdvancedOptions>
    {
        [Category("Preview Window")]
        [DisplayName("Enable preview window")]
        [Description("Determines if the preview window should be shown.")]
        [DefaultValue(true)]
        public bool EnablePreviewWindow { get; set; } = true;

        [Category("Preview Window")]
        [DisplayName("Enable scroll sync")]
        [Description("Determines if the preview window should sync its scroll position with the editor document.")]
        [DefaultValue(true)]
        public bool EnableScrollSync { get; set; } = true;

        [Category("Preview Window")]
        [DisplayName("Dark theme support")]
        [Description("Determines if the preview window should render in dark mode when a dark Visual Studio theme is in use.")]
        [DefaultValue(true)]
        public bool EnableDarkTheme { get; set; } = true;

        [Category("Preview Window")]
        [DisplayName("Open Linq Query and result in Visual Studio Preview Tab")]
        [Description("Determines if the VS preview tab window should render the Linq query and results.")]
        [DefaultValue(true)]
        public bool OpenInVSPreviewTab { get; set; } = true;

        [Category("Preview Window")]
        [DisplayName("Enable LinqPad Dump Windows for Linq Query and results")]
        [Description("Determines if the LinqPad Dump Windows is enabled and displays the Linq query and results.")]
        [DefaultValue(true)]
        public bool UseLinqPadDumpWindow { get; set; } = true;

        [Category("Preview Window")]
        [DisplayName("Enable Tool Window for Linq Query and results")]
        [Description("Determines if the Tool Window is enabled and displays the Linq query and results.")]
        [DefaultValue(true)]
        public bool EnableToolWindowResults { get; set; } = true;

        [Category("Preview Window")]
        [DisplayName("Preview window width")]
        [Description("The width in pixels of the preview window.")]
        [DefaultValue(500)]
        [Browsable(false)] // hidden
        public int PreviewWindowWidth { get; set; } = 500;

        [Category("Validation")]
        [DisplayName("Validate file links")]
        [Description("Validates if links point to local files and folders that exist.")]
        [DefaultValue(true)]
        public bool ValidateFileLinks { get; set; } = true;
    }
}
