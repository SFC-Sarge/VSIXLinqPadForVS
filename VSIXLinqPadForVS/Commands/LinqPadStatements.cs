﻿namespace VSIXLinqPadForVS.Commands
{
    [Command(PackageIds.DisplayLinqPadStatementsResults)]
    internal sealed class LinqPadStatements : BaseCommand<LinqPadStatements>
    {
        private const string runSelectedLinqStatement = "Run Selected Linq Statement.";

        protected override async Task ExecuteAsync(OleMenuCmdEventArgs e)
        {
            await ThreadHelper.JoinableTaskFactory.SwitchToMainThreadAsync();
            ThreadHelper.JoinableTaskFactory.RunAsync(async () =>
            {
                await ThreadHelper.JoinableTaskFactory.SwitchToMainThreadAsync();
                ToolWindowMessenger messenger = await Package.GetServiceAsync<ToolWindowMessenger, ToolWindowMessenger>();
                messenger.Send(runSelectedLinqStatement);
            }).FireAndForget();
        }
    }
}
