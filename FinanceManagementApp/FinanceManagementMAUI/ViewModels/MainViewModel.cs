using Application.Abstractions.NotConsole;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using FinanceManagementMAUI.Pages;
using FinanceManagementMAUI.Services;
using FinanceManagementMAUI.Services.PreferencesServices;

namespace FinanceManagementMAUI.ViewModels;
public partial class MainViewModel : ObservableObject
{
    private readonly IPreferencesService _preferencesService;
    private readonly IUserService _userService;
    private readonly IPopupService _popupService;

    public MainViewModel(IPreferencesService preferencesService, IUserService userService,
        IPopupService popupService)
    {
        _preferencesService = preferencesService;
        _userService = userService;
        _popupService = popupService;
    }

    [RelayCommand] async Task DoCheckIfAlreadyLoggedIn() => await CheckIfAlreadyLoggedIn();
    private async Task CheckIfAlreadyLoggedIn()
    {
        if (await _userService.FirstOrDefaultAsync(u => u.Id == _preferencesService.Get("id", -1)) is null)
        {
            await _popupService.Alert("No account", "You are not logged in", "Log in");
            await Shell.Current.GoToAsync(nameof(LoginPage));
        }
    }

    [RelayCommand] async Task DoAddCategory() => await AddCategory();
    async Task AddCategory()
    {
        await Shell.Current.GoToAsync(nameof(AddCategoryPage));
    }

    [RelayCommand] async Task DoAddCard() => await AddCard();
    async Task AddCard()
    {
        await Shell.Current.GoToAsync(nameof(AddSimpleAccountPage));
    }


    [RelayCommand] async Task DoShowStatistics() => await ShowStatistics();
    async Task ShowStatistics()
    {
        await Shell.Current.GoToAsync(nameof(StatisticsPage));
    }

    [RelayCommand] async Task DoLogout() => await Logout();
    async Task Logout()
    {
        _preferencesService.RemoveUserData();
        await Shell.Current.GoToAsync(nameof(LoginPage));
    }
}
