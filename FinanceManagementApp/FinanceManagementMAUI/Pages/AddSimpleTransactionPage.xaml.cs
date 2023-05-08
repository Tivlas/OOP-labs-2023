using FinanceManagementMAUI.ViewModels;

namespace FinanceManagementMAUI.Pages;

public partial class AddSimpleTransactionPage : ContentPage
{
    private readonly AddSimpleTransactionViewModel _vm;

    public AddSimpleTransactionPage(AddSimpleTransactionViewModel vm)
	{
		InitializeComponent();
        _vm = vm;
        BindingContext = _vm;
    }
}