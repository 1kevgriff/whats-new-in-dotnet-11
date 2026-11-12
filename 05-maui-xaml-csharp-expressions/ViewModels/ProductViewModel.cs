using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace MauiXamlExpressions.ViewModels;

public sealed class ProductViewModel : INotifyPropertyChanged
{
    private string _productName = "Shed heater";
    private int _quantity = 2;
    private decimal _unitPrice = 49.99m;

    public string ProductName { get => _productName; set => Set(ref _productName, value); }
    public int Quantity { get => _quantity; set => Set(ref _quantity, value); }
    public decimal UnitPrice { get => _unitPrice; set => Set(ref _unitPrice, value); }

    public event PropertyChangedEventHandler? PropertyChanged;

    private void Set<T>(ref T field, T value, [CallerMemberName] string? name = null)
    {
        if (EqualityComparer<T>.Default.Equals(field, value)) return;
        field = value;
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }
}
