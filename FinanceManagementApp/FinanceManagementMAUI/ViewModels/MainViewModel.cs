using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using FinanceManagementMAUI.Services.PreferencesServices;

namespace FinanceManagementMAUI.ViewModels;
public partial class MainViewModel : ObservableObject
{
    private readonly IPreferencesService _preferencesService;

    public MainViewModel(IPreferencesService preferencesService)
    {
        _preferencesService = preferencesService;
    }

    [RelayCommand] async Task DoCheckIfAlreadyLoggedIn() => await CheckIfAlreadyLoggedIn();

    private async Task CheckIfAlreadyLoggedIn()
    {
        int id = _preferencesService.Get("id", -1);
        if (id == -1)
        {
            await App.Current.MainPage.DisplayAlert("No account", "You are not logged in", "Log in");
            await Shell.Current.GoToAsync("login");
        }
    }
}
