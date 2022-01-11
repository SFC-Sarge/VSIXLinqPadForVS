﻿using VSIXLinqPadForVS;
namespace LinqEditor2022
{
    [Command(PackageIds.OpenSettings)]
    internal sealed class OpenSettingsCommand : BaseCommand<OpenSettingsCommand>
    {
        protected override void Execute(object sender, EventArgs e)
        {
            Package.ShowOptionPage(typeof(OptionsProvider.AdvancedOptions));
        }
    }
}
