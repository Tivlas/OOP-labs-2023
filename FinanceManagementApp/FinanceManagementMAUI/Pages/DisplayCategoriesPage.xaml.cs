using System.ComponentModel.DataAnnotations;

namespace FinanceManagementMAUI.Pages;

public partial class DisplayCategoriesPage : ContentPage
{
    private readonly DisplayColumnAttribute _vm;

    public DisplayCategoriesPage(DisplayColumnAttribute vm)
	{
		InitializeComponent();
        _vm = vm;
        BindingContext = _vm;
    }
}