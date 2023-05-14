using FinanceManagementMAUI.ViewModels;

namespace FinanceManagementMAUI.Pages;

public partial class StatisticsPage : ContentPage
{
    private readonly StatisticsViewModel _vm;

    public StatisticsPage(StatisticsViewModel vm)
	{
		InitializeComponent();
        _vm = vm;
        BindingContext = _vm;
    }
}