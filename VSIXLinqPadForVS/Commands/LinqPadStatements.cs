namespace VSIXLinqPadForVS.Commands
{
    [Command(PackageIds.DisplayLinqPadStatementsResults)]
    internal sealed class LinqPadStatements : BaseCommand<LinqPadStatements>
    {
        protected override async Task ExecuteAsync(OleMenuCmdEventArgs e)
        {
            await ThreadHelper.JoinableTaskFactory.SwitchToMainThreadAsync();
            ThreadHelper.JoinableTaskFactory.RunAsync(async () =>
            {
                await ThreadHelper.JoinableTaskFactory.SwitchToMainThreadAsync();
                ToolWindowMessenger messenger = await Package.GetServiceAsync<ToolWindowMessenger, ToolWindowMessenger>();
                messenger.Send("Run Selected Linq Statement");
            }).FireAndForget();
        }
    }
}
