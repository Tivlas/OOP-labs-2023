using FinanceManagementMAUI.Pages;

namespace FinanceManagementMAUI;

public partial class AppShell : Shell
{
    public AppShell()
    {
        InitializeComponent();
        Routing.RegisterRoute(nameof(LoginPage), typeof(LoginPage));
        Routing.RegisterRoute(nameof(AddCategoryPage), typeof(AddCategoryPage));
        Routing.RegisterRoute(nameof(EditCategoryPage), typeof(EditCategoryPage));
    }
}
