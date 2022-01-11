using System.Diagnostics;
using VSIXLinqPadForVS;

namespace LinqEditor2022
{
    [Command(PackageIds.ShowMarkdownReference)]
    internal sealed class ShowMarkdownReferenceCommand : BaseCommand<ShowMarkdownReferenceCommand>
    {
        protected override void Execute(object sender, EventArgs e) =>
            Process.Start("https://www.markdownguide.org/cheat-sheet/");
    }
}
