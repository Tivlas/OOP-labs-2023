using FinanceManagementMAUI.ViewModels;

namespace FinanceManagementMAUI.Pages;

public partial class AddCategoryPage : ContentPage
{
    private readonly AddCategoryViewModel _vm;

    public AddCategoryPage(AddCategoryViewModel vm)
	{
		InitializeComponent();
        _vm = vm;
        BindingContext = _vm;
    }
}