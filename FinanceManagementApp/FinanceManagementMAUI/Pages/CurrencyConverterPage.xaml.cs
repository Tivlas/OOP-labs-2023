using FinanceManagementMAUI.ViewModels;

namespace FinanceManagementMAUI.Pages;

public partial class CurrencyConverterPage : ContentPage
{
    private readonly CurrencyConverterViewModel _vm;

    public CurrencyConverterPage(CurrencyConverterViewModel vm)
	{
		InitializeComponent();
        _vm = vm;
        BindingContext = _vm;
    }
}