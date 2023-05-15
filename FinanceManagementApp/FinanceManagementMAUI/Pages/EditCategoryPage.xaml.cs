using FinanceManagementMAUI.ViewModels;

namespace FinanceManagementMAUI.Pages;

public partial class EditCategoryPage : ContentPage
{
    private readonly EditCategoryViewModel _vm;

    public EditCategoryPage(EditCategoryViewModel vm)
    {
        InitializeComponent();
        _vm = vm;
        BindingContext = _vm;
    }
}