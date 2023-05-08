using FinanceManagementMAUI.ViewModels;

namespace FinanceManagementMAUI.Pages;

public partial class DisplaySimpleTransactionsPage : ContentPage
{
    private readonly DisplaySimpleTransactionsViewModel _vm;

    public DisplaySimpleTransactionsPage(DisplaySimpleTransactionsViewModel vm)
	{
		InitializeComponent();
        _vm = vm;
        BindingContext = _vm;
    }
}