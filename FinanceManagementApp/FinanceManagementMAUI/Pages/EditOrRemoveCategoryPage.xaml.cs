using FinanceManagementMAUI.ViewModels;

namespace FinanceManagementMAUI.Pages;

public partial class EditOrRemoveCategoryPage : ContentPage
{
    private readonly EditOrRemoveCategoryViewModel _vm;

    public EditOrRemoveCategoryPage(EditOrRemoveCategoryViewModel vm)
	{
		InitializeComponent();
        _vm = vm;
        BindingContext = _vm;
    }
}