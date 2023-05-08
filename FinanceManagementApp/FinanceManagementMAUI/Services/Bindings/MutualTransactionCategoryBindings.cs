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
public class MutualTransactionCategoryBindings
{
    private readonly ITransactionCategoryService _transactionCategoryService;
    private readonly IPreferencesService _preferencesService;

    public MutualTransactionCategoryBindings(ITransactionCategoryService transactionCategoryService, IPreferencesService preferencesService)
    {
        _transactionCategoryService = transactionCategoryService;
        _preferencesService = preferencesService;
        MainThread.BeginInvokeOnMainThread(async () =>
        {
            var transactionCategories = await _transactionCategoryService.ListAsync(tc => tc.UserId == _preferencesService.Get("id", -1));
            foreach(var tc in transactionCategories)
            {
                TransactionCategories.Add(tc);
            }
        });
    }

    public ObservableCollection<TransactionCategory> TransactionCategories { get; set; } = new();
}
