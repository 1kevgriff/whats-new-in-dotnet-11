namespace WinFormsVisualStyles;

public sealed class MainForm : Form
{
    public MainForm()
    {
        Text = "WinForms visual styles in .NET 11";
        AutoSize = true;
        AutoSizeMode = AutoSizeMode.GrowAndShrink;
        Padding = new Padding(12);

        // "Before": this panel overrides the app default and uses the classic renderer.
        GroupBox before = CreateSamplePanel("Before (Classic)");
        before.VisualStylesMode = VisualStylesMode.Classic;

        // "After": this panel inherits VisualStylesMode.Net11 from the application default.
        GroupBox after = CreateSamplePanel("After (Net11, inherited)");

        // Before/after toggle: flip the left panel between Classic and Net11 at run time.
        CheckBox flip = new()
        {
            Text = "Use .NET 11 styles on the left panel",
            Appearance = Appearance.ToggleSwitch,
            AutoSize = true,
        };
        flip.CheckedChanged += (_, _) =>
        {
            before.VisualStylesMode = flip.Checked ? VisualStylesMode.Net11 : VisualStylesMode.Classic;
            before.Text = flip.Checked ? "Before panel, now Net11" : "Before (Classic)";
        };

        // Layout-based UI: control metrics differ between renderers, so avoid pixel-perfect positions.
        TableLayoutPanel layout = new()
        {
            ColumnCount = 2,
            RowCount = 2,
            AutoSize = true,
            Dock = DockStyle.Fill,
        };
        layout.Controls.Add(flip, 0, 0);
        layout.SetColumnSpan(flip, 2);
        layout.Controls.Add(before, 0, 1);
        layout.Controls.Add(after, 1, 1);

        Controls.Add(layout);
    }

    // The renderer covers Button, CheckBox, RadioButton, GroupBox, TextBox, and RichTextBox.
    private static GroupBox CreateSamplePanel(string title)
    {
        FlowLayoutPanel flow = new()
        {
            FlowDirection = FlowDirection.TopDown,
            AutoSize = true,
            Dock = DockStyle.Fill,
            WrapContents = false,
        };
        flow.Controls.Add(new Button { Text = "Button", AutoSize = true });
        flow.Controls.Add(new CheckBox { Text = "CheckBox", Checked = true, AutoSize = true });
        flow.Controls.Add(new CheckBox { Text = "Toggle switch", Appearance = Appearance.ToggleSwitch, Checked = true, AutoSize = true });
        flow.Controls.Add(new RadioButton { Text = "RadioButton", Checked = true, AutoSize = true });
        flow.Controls.Add(new TextBox { Text = "TextBox", Width = 180 });
        flow.Controls.Add(new RichTextBox { Text = "RichTextBox", Width = 180, Height = 50 });

        GroupBox group = new() { Text = title, AutoSize = true, Padding = new Padding(8) };
        group.Controls.Add(flow);
        return group;
    }
}
