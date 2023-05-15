using FinanceManagementMAUI.ViewModels;

namespace FinanceManagementMAUI.Pages;

public partial class AboutPage : ContentPage
{
    private readonly AboutViewModel _vm;

    public AboutPage(AboutViewModel vm)
	{
		InitializeComponent();
        _vm = vm;
        BindingContext = _vm;
    }
}