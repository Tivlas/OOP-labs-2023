using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Abstractions.NotConsole;
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
    [ObservableProperty] private string _amoutOfMoney = "0";
    [ObservableProperty] private TransactionCategory _category;
    [ObservableProperty] private SimpleAccount _selectedSimpleAccount;
    private readonly ISimpleTransactionService _simpleTransactionService;
    private readonly IPopupService _popupService;
    private readonly IPreferencesService _preferencesService;
    private readonly MutualSimpleTransactionBinding _mutualSimpleTransactionBinding;

    public MutualTransactionCategoryBindings MutualTransactionCategoryBindings { get; }

    public AddSimpleTransactionViewModel(ISimpleTransactionService simpleTransactionService,
        IPopupService popupService, IPreferencesService preferencesService,
        MutualTransactionCategoryBindings mutualTransactionCategoryBindings,
        MutualSimpleTransactionBinding mutualSimpleTransactionBinding)
    {
        _simpleTransactionService = simpleTransactionService;
        _popupService = popupService;
        _preferencesService = preferencesService;
        MutualTransactionCategoryBindings = mutualTransactionCategoryBindings;
        _mutualSimpleTransactionBinding = mutualSimpleTransactionBinding;
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
            money = Calculator.Calc(AmoutOfMoney);
        }
        catch (Exception)
        {
            await _popupService.Alert("Wrong input", "Amount of money must be a number greater than 0 or expression (eg: 5 * 3 - 4)", "Ok");
        }
        if(money <= 0)
        {
            await _popupService.Alert("Wrong input", "Amount of money must be a number greater than 0", "Ok");
        }
        if (Category is null)
        {
            await _popupService.Alert("Error", "Select transaction category", "Ok");
        }
        else
        {
            var transaction = new SimpleTransaction(SelectedSimpleAccount.Id, Date, IsIncome, money, Category,
                Comment, _preferencesService.Get("id", -1));
            await _simpleTransactionService.AddAsync(transaction);
            await _simpleTransactionService.SaveChangesAsync();
            _mutualSimpleTransactionBinding.Add(transaction);
        }
    }
}
