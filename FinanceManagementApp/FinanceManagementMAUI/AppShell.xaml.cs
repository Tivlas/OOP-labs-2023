﻿using FinanceManagementMAUI.Pages;

namespace FinanceManagementMAUI;

public partial class AppShell : Shell
{
    public AppShell()
    {
        InitializeComponent();
        Routing.RegisterRoute("login", typeof(LoginPage));
        Routing.RegisterRoute("addCategory", typeof(AddCategoryPage));
        Routing.RegisterRoute("editOrRemoveCategory", typeof(EditOrRemoveCategoryPage));
    }
}
