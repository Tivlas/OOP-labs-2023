using FinanceManagementMAUI.ViewModels;

namespace FinanceManagementMAUI.Pages;

public partial class EditSimpleTransactionPage : ContentPage
{
    private readonly EditSimpleTransactionViewModel _vm;

    public EditSimpleTransactionPage(EditSimpleTransactionViewModel vm)
	{
		InitializeComponent();
        _vm = vm;
        BindingContext = _vm;
    }
}