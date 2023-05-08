using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Abstractions.NotConsole;
using Application.Services.NotConsole;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Domain.Entities.Accounts;
using Domain.Entities.Transactions;
using FinanceManagementMAUI.Pages;
using FinanceManagementMAUI.Services.PreferencesServices;

namespace FinanceManagementMAUI.ViewModels;
public partial class DisplaySimpleAccountsViewModel : ObservableObject
{
    private readonly ISimpleAccountService _simpleAccountService;
    private readonly IPreferencesService _preferencesService;
    public ObservableCollection<SimpleAccount> SimpleAccounts { get; set; } = new();

    public DisplaySimpleAccountsViewModel(ISimpleAccountService simpleAccountService, IPreferencesService preferencesService)
    {
        _simpleAccountService = simpleAccountService;
        _preferencesService = preferencesService;
    }

    [RelayCommand] async Task DoGetSimpleAccounts() => await GetSimpleAccounts();
    async Task GetSimpleAccounts()
    {
        SimpleAccounts.Clear();
        var tcs = await _simpleAccountService.ListAsync(sa => sa.UserId == _preferencesService.Get("id", -1));
        await MainThread.InvokeOnMainThreadAsync(() =>
        {
            foreach (var tc in tcs)
            {
                SimpleAccounts.Add(tc);
            }
        });
    }

    [RelayCommand] async Task DoRemove(SimpleAccount simpleAccount) => await RemoveCategory(simpleAccount);
    async Task RemoveCategory(SimpleAccount simpleAccount)
    {
        await MainThread.InvokeOnMainThreadAsync(() =>
        {
            SimpleAccounts.Remove(simpleAccount);
        });
        await _simpleAccountService.DeleteAsync(simpleAccount);
        await _simpleAccountService.SaveChangesAsync();
    }

    [RelayCommand] async Task DoAddSimpleAccount() => await AddSimpleAccount();

    async Task AddSimpleAccount()
    {
        await Shell.Current.GoToAsync(nameof(AddSimpleAccountPage));
    }

    [RelayCommand] async Task DoEdit(SimpleAccount account) => await Edit(account);

    async Task Edit(SimpleAccount account)
    {
        IDictionary<string, object> parameters = new Dictionary<string, object>()
                {
                    {"SimpleAccount", (account)}
                };

        await Shell.Current.GoToAsync(nameof(EditSimpleAccountPage), parameters);
    }

    [RelayCommand] async Task DoAddSimpleTransaction(SimpleAccount account) => await AddSimpleTransaction(account);

    async Task AddSimpleTransaction(SimpleAccount account)
    {
        IDictionary<string, object> parameters = new Dictionary<string, object>()
                {
                    {"SimpleAccount", (account)}
                };

        await Shell.Current.GoToAsync(nameof(AddSimpleTransactionPage), parameters);
    }
}
