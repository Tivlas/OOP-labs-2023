using FinanceManagementMAUI.ViewModels;

namespace FinanceManagementMAUI.Pages;

public partial class EditSimpleAccountPage : ContentPage
{
    private readonly EditSimpleAccountViewModel _vm;

    public EditSimpleAccountPage(EditSimpleAccountViewModel vm)
	{
		InitializeComponent();
        _vm = vm;
        BindingContext = _vm;
    }
}