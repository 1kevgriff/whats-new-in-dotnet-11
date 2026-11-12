namespace WinFormsVisualStyles;

static class Program
{
    [STAThread]
    static void Main()
    {
        ApplicationConfiguration.Initialize();

        // The one-liner: opt the whole app in to the .NET 11 renderer before the first window is created.
        Application.SetDefaultVisualStylesMode(VisualStylesMode.Net11);

        // Optional: keep forms hidden until layout and theming settle (no unstyled flash on startup).
        Application.SetDefaultFormRevealMode(FormRevealMode.Deferred);

        Application.Run(new MainForm());
    }
}
