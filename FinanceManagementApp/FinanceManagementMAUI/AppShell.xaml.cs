using FinanceManagementMAUI.Pages;

namespace FinanceManagementMAUI;

public partial class AppShell : Shell
{
    public AppShell()
    {
        InitializeComponent();
        Routing.RegisterRoute(nameof(LoginPage), typeof(LoginPage));
        Routing.RegisterRoute(nameof(StatisticsPage), typeof(StatisticsPage));

        Routing.RegisterRoute(nameof(AddCategoryPage), typeof(AddCategoryPage));
        Routing.RegisterRoute(nameof(AddSimpleAccountPage), typeof(AddSimpleAccountPage));
        Routing.RegisterRoute(nameof(AddSimpleTransactionPage), typeof(AddSimpleTransactionPage));

        Routing.RegisterRoute(nameof(EditCategoryPage), typeof(EditCategoryPage));
        Routing.RegisterRoute(nameof(EditSimpleAccountPage), typeof(EditSimpleAccountPage));
        Routing.RegisterRoute(nameof(EditSimpleTransactionPage), typeof(EditSimpleTransactionPage));
    }
}
