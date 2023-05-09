using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Abstractions.NotConsole;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Domain.Entities.Transactions;
using FinanceManagementMAUI.Services.Bindings;
using FinanceManagementMAUI.Services.PreferencesServices;

namespace FinanceManagementMAUI.ViewModels;
public partial class DisplaySimpleTransactionsViewModel : ObservableObject
{
    private readonly MutualSimpleAccountsBinding _mutualSimpleAccountsBinding;
    private readonly ISimpleAccountService _simpleAccountService;
    private readonly ISimpleTransactionService _simpleTransactionService;
    private readonly IPreferencesService _preferencesService;

    public DisplaySimpleTransactionsViewModel(MutualSimpleTransactionBinding mutualSimpleTransactionBinding,
        MutualSimpleAccountsBinding mutualSimpleAccountsBinding,
        ISimpleAccountService simpleAccountService, ISimpleTransactionService simpleTransactionService,
        IPreferencesService preferencesService)
    {
        MutualSimpleTransactionBinding = mutualSimpleTransactionBinding;
        _mutualSimpleAccountsBinding = mutualSimpleAccountsBinding;
        _simpleAccountService = simpleAccountService;
        _simpleTransactionService = simpleTransactionService;
        _preferencesService = preferencesService;
    }

    public MutualSimpleTransactionBinding MutualSimpleTransactionBinding { get; }

    [RelayCommand] async Task DoRemove(SimpleTransaction transaction) => await Remove(transaction);
    async Task Remove(SimpleTransaction transaction)
    {
        MutualSimpleTransactionBinding.Remove(transaction);
        var accToEdit = await _simpleAccountService.FirstOrDefaultAsync(acc => acc.UserId == _preferencesService.Get("id", -1) &&
        acc.Id == transaction.SimpleAccountId);
        if(accToEdit is null)
        {
            return;
        }
        int index = _mutualSimpleAccountsBinding.SimpleAccounts.IndexOf(accToEdit);
        if (transaction.IsIncome)
        {
            accToEdit.Balance -= transaction.AmountOfMoney;
        }
        else
        {
            accToEdit.Balance += transaction.AmountOfMoney;
        }
        _mutualSimpleAccountsBinding.Edit(accToEdit, index);
        await _simpleAccountService.UpdateAsync(accToEdit);
        await _simpleAccountService.SaveChangesAsync();

        await _simpleTransactionService.DeleteAsync(transaction);
        await _simpleTransactionService.SaveChangesAsync();
    }
}
