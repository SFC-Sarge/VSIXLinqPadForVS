using System.Linq;
using System.Windows;
using System.Windows.Controls;
using LINQPad;

namespace VSIXLinqPadForVS
{
    public partial class MyToolWindowControl : UserControl
    {
        OutputWindowPane pane = null;
        string[] names = { "Bob", "Ned", "Amy", "Bill" };
      public MyToolWindowControl()
        {
            DoOutputWindowsAsync();
            InitializeComponent();
        }

        private async void Button1_Click(object sender, RoutedEventArgs e)
        {
            var result = await VS.MessageBox.ShowAsync("LinqPadForVS", "Button clicked");
            await pane.WriteLineAsync($"2 * 25 = {(2 * 25).Dump<int>()}");
            await pane.WriteLineAsync($"2 * 50 = {(2 * 50).Dump<int>()}");
            await pane.WriteLineAsync($"2 * 75 = {(2 * 75).Dump<int>()}");
            await pane.WriteLineAsync($"button1 was clicked: Result: {result}");
            await pane.WriteLineAsync($"Does any of the names: {names[0]}, {names[1]}, {names[2]}, or {names[3]} StartsWith(B)? {names.Any(n => n.StartsWith("B")).Dump()}");
        }
        private async Task DoOutputWindowsAsync()
        {
            pane = await VS.Windows.CreateOutputWindowPaneAsync("LinqPad Dump");
            return;
        }
    }
}