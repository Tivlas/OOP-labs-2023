using System.Collections.ObjectModel;
using Application.Abstractions.NotConsole;
using Domain.Entities.Accounts;
using FinanceManagementMAUI.Services.PreferencesServices;

namespace FinanceManagementMAUI.Services.Bindings;
public class MutualSimpleAccountsBinding
{
    private readonly ISimpleAccountService _simpleAccountService;
    private readonly IPreferencesService _preferencesService;

    public MutualSimpleAccountsBinding(ISimpleAccountService simpleAccountService, IPreferencesService preferencesService)
    {
        _simpleAccountService = simpleAccountService;
        _preferencesService = preferencesService;
        MainThread.BeginInvokeOnMainThread(async () =>
        {
            var accounts = await _simpleAccountService.ListAsync(a => a.UserId == _preferencesService.Get("id", -1));
            foreach (var a in accounts)
            {
                SimpleAccounts.Add(a);
            }
        });
    }

    public ObservableCollection<SimpleAccount> SimpleAccounts { get; set; } = new();

    public void Add(SimpleAccount acc)
    {
        MainThread.BeginInvokeOnMainThread(() =>
        {
            SimpleAccounts.Add(acc);
        });
    }

    public void Edit(SimpleAccount acc, int index)
    {
        MainThread.BeginInvokeOnMainThread(() =>
        {
            SimpleAccounts[index] = acc;
        });
    }

    public void Remove(SimpleAccount acc)
    {
        MainThread.BeginInvokeOnMainThread(() =>
        {
            SimpleAccounts.Remove(acc);
        });
    }
}
