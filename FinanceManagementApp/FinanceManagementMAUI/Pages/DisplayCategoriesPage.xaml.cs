using FinanceManagementMAUI.ViewModels;

namespace FinanceManagementMAUI.Pages;

public partial class DisplayCategoriesPage : ContentPage
{
    private readonly DisplayCategoriesViewModel _vm;

    public DisplayCategoriesPage(DisplayCategoriesViewModel vm)
    {
        InitializeComponent();
        _vm = vm;
        BindingContext = _vm;
    }
}