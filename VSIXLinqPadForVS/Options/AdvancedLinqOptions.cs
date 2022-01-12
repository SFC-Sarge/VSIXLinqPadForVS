using System.ComponentModel;
using System.Runtime.InteropServices;

namespace VSIXLinqPadForVS
{
    internal partial class OptionsProvider
    {
        [ComVisible(true)]
        public class AdvancedLinqOptions : BaseOptionPage<VSIXLinqPadForVS.AdvancedLinqOptions> { }
    }

    public class AdvancedLinqOptions : BaseOptionModel<AdvancedLinqOptions>
    {
        [Category("Preview Window")]
        [DisplayName("Open Linq Query and result in Visual Studio Preview Tab")]
        [Description("Determines if the VS preview tab window should render the Linq query and results.")]
        [DefaultValue(true)]
        public bool OpenInVSPreviewTab { get; set; } = true;

        [Category("Preview Window")]
        [DisplayName("Enable LinqPad Dump Windows for Linq Query and results")]
        [Description("Determines if the LinqPad Dump Windows is enabled and displays the Linq query and results.")]
        [DefaultValue(false)]
        public bool UseLinqPadDumpWindow { get; set; } = false;

        [Category("Preview Window")]
        [DisplayName("Enable Tool Window for Linq Query and results")]
        [Description("Determines if the Tool Window is enabled and displays the Linq query and results.")]
        [DefaultValue(false)]
        public bool EnableToolWindowResults { get; set; } = false;

    }
}
