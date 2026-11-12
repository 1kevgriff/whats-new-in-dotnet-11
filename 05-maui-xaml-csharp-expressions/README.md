# 05: C# expressions in .NET MAUI XAML (PREVIEW)

> **Preview.** C# expressions in XAML need XAML source generation and `EnablePreviewFeatures`. XamlC and runtime inflation don't support the syntax.

```xml
<PropertyGroup>
    <MauiXamlInflator>SourceGen</MauiXamlInflator>
    <EnablePreviewFeatures>true</EnablePreviewFeatures>
</PropertyGroup>
```

With `x:DataType` set, `MainPage.xaml` uses:

| XAML | What it does |
| --- | --- |
| `{ProductName}` | Typed binding to the binding context |
| `{$'{Quantity} at {UnitPrice:C2}'}` | Computed binding, updates when `Quantity` or `UnitPrice` change |
| `{this.GetStatusText()}` | Page method, evaluated once at initialization |
| `{(sender, e) => this.AddOne()}` | Event lambda, runs on click |

Tap **Add one** to see the computed label update. The status label does not change.

## Run

Needs the MAUI workload (`dotnet workload install maui`).

```bash
dotnet build 05-maui-xaml-csharp-expressions -t:Run -f net11.0-windows10.0.19041.0
```

Use `net11.0-android`, `net11.0-ios`, or `net11.0-maccatalyst` for other platforms.

## Gotcha

Don't override the inflator with `Runtime` or `XamlC` on a file that uses expressions. Async event lambdas aren't supported; use a named method for async work. `{this.Method()}` is not a live binding.

## Docs

- [C# expressions in XAML](https://learn.microsoft.com/dotnet/maui/xaml/csharp-expressions?view=net-maui-11.0)
- [What's new in .NET MAUI for .NET 11](https://learn.microsoft.com/dotnet/maui/whats-new/dotnet-11)
