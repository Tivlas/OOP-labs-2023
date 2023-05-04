using FinanceManagementMAUI.ViewModels;

namespace FinanceManagementMAUI.Pages;

public partial class LoginPage : ContentPage
{
    private readonly LoginViewModel _vm;

    public LoginPage(LoginViewModel vm)
	{
		InitializeComponent();
        _vm = vm;
        BindingContext = _vm;
    }
}