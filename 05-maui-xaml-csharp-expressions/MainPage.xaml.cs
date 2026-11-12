using MauiXamlExpressions.ViewModels;

namespace MauiXamlExpressions;

public partial class MainPage : ContentPage
{
    private readonly ProductViewModel _viewModel = new();

    public MainPage()
    {
        BindingContext = _viewModel;
        InitializeComponent();
    }

    public string GetStatusText() => $"Page loaded at {DateTime.Now:t}";

    public void AddOne() => _viewModel.Quantity++;

    public void Save() => SemanticScreenReader.Announce($"Saved {_viewModel.ProductName}");
}
