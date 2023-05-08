using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Abstractions.NotConsole;
using Domain.Entities.Transactions;
using FinanceManagementMAUI.Services.PreferencesServices;

namespace FinanceManagementMAUI.Services.Bindings;
public class MutualSimpleTransactionBinding
{
    private readonly ISimpleTransactionService _simpleTransactionService;
    private readonly IPreferencesService _preferencesService;

    public MutualSimpleTransactionBinding(ISimpleTransactionService simpleTransactionService, IPreferencesService preferencesService)
    {
        _simpleTransactionService = simpleTransactionService;
        _preferencesService = preferencesService;
        MainThread.BeginInvokeOnMainThread(async () =>
        {
            var transactions = await _simpleTransactionService.ListAsync(st => st.UserId == _preferencesService.Get("id", -1));
            foreach(var t in transactions)
            {
                SimpleTransactions.Add(t);
            }
        });
    }

    public ObservableCollection<SimpleTransaction> SimpleTransactions { get; set; } = new();
}
