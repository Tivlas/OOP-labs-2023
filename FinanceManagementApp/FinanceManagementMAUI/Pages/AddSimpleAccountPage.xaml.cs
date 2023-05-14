using FinanceManagementMAUI.ViewModels;

namespace FinanceManagementMAUI.Pages;

public partial class AddSimpleAccountPage : ContentPage
{
    private readonly AddSimpleAccountViewModel _vm;

    public AddSimpleAccountPage(AddSimpleAccountViewModel vm)
    {
        InitializeComponent();
        _vm = vm;
        BindingContext = _vm;
    }
}