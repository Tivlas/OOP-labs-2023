using FinanceManagementMAUI.ViewModels;

namespace FinanceManagementMAUI.Pages;

public partial class DisplaySimpleAccountsPage : ContentPage
{
    private readonly DisplaySimpleAccountsViewModel _vm;

    public DisplaySimpleAccountsPage(DisplaySimpleAccountsViewModel vm)
    {
        InitializeComponent();
        _vm = vm;
        BindingContext = _vm;
    }
}