using System.Windows.Shapes;

namespace VSIXLinqPadForVS
{
    [Command(PackageIds.EditorLinqPad)]
    internal sealed class LinqQueryEditor : BaseCommand<LinqQueryEditor>
{
        private const string runEditorLinqQuery = "Run Editor Linq Query.";

        protected override async Task ExecuteAsync(OleMenuCmdEventArgs e)
        {
            await ThreadHelper.JoinableTaskFactory.SwitchToMainThreadAsync();
            ThreadHelper.JoinableTaskFactory.RunAsync(async () =>
            {
                await ThreadHelper.JoinableTaskFactory.SwitchToMainThreadAsync();
                ToolWindowMessenger messenger = await Package.GetServiceAsync<ToolWindowMessenger, ToolWindowMessenger>();
                messenger.Send(runEditorLinqQuery);
            }).FireAndForget();
        }
    }
}
