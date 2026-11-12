# 03: WinForms visual styles

.NET 11 adds an opt-in modern renderer for WinForms. Upgrading does not restyle your app. You opt in once, before the first window is created:

```csharp
Application.SetDefaultVisualStylesMode(VisualStylesMode.Net11);
```

You can also set `VisualStylesMode` on a single control. Child controls inherit it.

The form shows the same controls twice: the left panel forces `VisualStylesMode.Classic`, the right panel inherits `Net11`. The toggle switch at the top flips the left panel between the two modes. `Appearance.ToggleSwitch` is new for `CheckBox`.

## Run

Windows only.

```bash
dotnet run --project 03-winforms-visual-styles
```

## Gotcha

The new renderer covers `Button`, `CheckBox`, `RadioButton`, `GroupBox`, `TextBox`, and `RichTextBox`, not every control. Control metrics can differ from classic WinForms, so prefer `TableLayoutPanel` and `FlowLayoutPanel` over pixel-perfect layouts.

## Docs

- [What's new in Windows Forms for .NET 11: Visual styles](https://learn.microsoft.com/dotnet/desktop/winforms/whats-new/net110#visual-styles-and-appearance)
- [Deferred form reveal](https://learn.microsoft.com/dotnet/desktop/winforms/whats-new/net110#deferred-form-reveal)
