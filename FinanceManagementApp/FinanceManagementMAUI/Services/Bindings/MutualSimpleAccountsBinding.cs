using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Abstractions.NotConsole;
using Domain.Entities.Accounts;
using Domain.Entities.Transactions;
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
}
