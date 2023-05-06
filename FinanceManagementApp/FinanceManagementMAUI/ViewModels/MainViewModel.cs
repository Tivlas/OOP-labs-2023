using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace FinanceManagementMAUI.ViewModels;
public partial class MainViewModel : ObservableObject
{
    [RelayCommand] async Task DoCheckIfAlreadyLoggedIn() => await CheckIfAlreadyLoggedIn();

    private async Task CheckIfAlreadyLoggedIn()
    {
        int id = Preferences.Default.Get("id", -1);
        if (id == -1)
        {
            await Shell.Current.GoToAsync("login");
        }
    }
}
