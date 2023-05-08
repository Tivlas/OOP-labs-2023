using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Abstractions.NotConsole;
using CommunityToolkit.Maui.Core;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Domain.Entities;
using Domain.Entities.Accounts;
using Domain.Entities.Transactions;
using FinanceManagementMAUI.Services;
using FinanceManagementMAUI.Services.Bindings;
using FinanceManagementMAUI.Services.PreferencesServices;

namespace FinanceManagementMAUI.ViewModels;
public partial class AddSimpleTransactionViewModel : ObservableObject, IQueryAttributable
{
    [ObservableProperty] private bool _isIncome;
    [ObservableProperty] private string _comment;
    [ObservableProperty] private DateTime _date = DateTime.Today;
    [ObservableProperty] private string _amountOfMoney = "0";
    [ObservableProperty] private TransactionCategory _category;
    [ObservableProperty] private SimpleAccount _selectedSimpleAccount;
    private readonly ISimpleTransactionService _simpleTransactionService;
    private readonly IPopupService _popupService;
    private readonly IPreferencesService _preferencesService;
    private readonly ISimpleAccountService _simpleAccountService;
    private readonly MutualSimpleTransactionBinding _mutualSimpleTransactionBinding;
    private readonly MutualSimpleAccountsBinding _mutualSimpleAccountsBinding;

    public MutualTransactionCategoryBindings MutualTransactionCategoryBindings { get; }

    public AddSimpleTransactionViewModel(ISimpleTransactionService simpleTransactionService,
        IPopupService popupService, IPreferencesService preferencesService, ISimpleAccountService simpleAccountService,
        MutualTransactionCategoryBindings mutualTransactionCategoryBindings,
        MutualSimpleTransactionBinding mutualSimpleTransactionBinding, MutualSimpleAccountsBinding mutualSimpleAccountsBinding)
    {
        _simpleTransactionService = simpleTransactionService;
        _popupService = popupService;
        _preferencesService = preferencesService;
        _simpleAccountService = simpleAccountService;
        MutualTransactionCategoryBindings = mutualTransactionCategoryBindings;
        _mutualSimpleTransactionBinding = mutualSimpleTransactionBinding;
        _mutualSimpleAccountsBinding = mutualSimpleAccountsBinding;
    }

    public void ApplyQueryAttributes(IDictionary<string, object> query)
    {
        SelectedSimpleAccount = query["SimpleAccount"] as SimpleAccount;
    }

    [RelayCommand] async Task DoAddTransaction() => await AddTransaction();

    async Task AddTransaction()
    {
        Comment ??= string.Empty;
        decimal money = 0;
        try
        {
            money = Calculator.Calc(AmountOfMoney);
        }
        catch (Exception)
        {
            await _popupService.Alert("Wrong input", "Amount of money must be a number greater than 0 or expression (eg: 5 * 3 - 4)", "Ok");
        }
        if (money <= 0)
        {
            await _popupService.Alert("Wrong input", "Amount of money must be a number greater than 0", "Ok");
        }
        else if (Category is null)
        {
            await _popupService.Alert("Error", "Select transaction category", "Ok");
        }
        else
        {
            var transaction = new SimpleTransaction(SelectedSimpleAccount.Id, Date, IsIncome, money, Category.Id,
                Comment, _preferencesService.Get("id", -1));

            int index = _mutualSimpleAccountsBinding.SimpleAccounts.IndexOf(SelectedSimpleAccount);

            if (IsIncome)
            {
                SelectedSimpleAccount.Balance += money;
            }
            else
            {
                SelectedSimpleAccount.Balance -= money;
            }

            var accToEdit = await _simpleAccountService.FirstOrDefaultAsync(a => a.UserId == SelectedSimpleAccount.UserId &&
            a.Name == SelectedSimpleAccount.Name);
            if (accToEdit is not null)
            {
                accToEdit.Balance = SelectedSimpleAccount.Balance;
                await _simpleAccountService.UpdateAsync(accToEdit);
                await _simpleAccountService.SaveChangesAsync();
            }

            _mutualSimpleAccountsBinding.Edit(SelectedSimpleAccount, index);
            await _simpleTransactionService.AddAsync(transaction);
            await _simpleTransactionService.SaveChangesAsync();
            _mutualSimpleTransactionBinding.Add(transaction);
            await _popupService.ShowToast("Successfully added", ToastDuration.Short, 14);
        }
    }
}
