using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Abstractions.NotConsole;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
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
    [ObservableProperty] private decimal _amoutOfMoney;
    [ObservableProperty] private TransactionCategory _category;
    [ObservableProperty] private SimpleAccount _selectedSimpleAccount;
    private readonly ISimpleTransactionService _simpleTransactionService;
    private readonly ITransactionCategoryService _transactionCategoryService;
    private readonly IPopupService _popupService;
    private readonly IPreferencesService _preferencesService;
    private readonly IServiceProvider _serviceProvider;

    public ObservableCollection<TransactionCategory> Categories { get; set; } = new();
    public MutualTransactionCategoryBindings MutualTransactionCategoryBindings { get; }

    public AddSimpleTransactionViewModel(ISimpleTransactionService simpleTransactionService, 
        IPopupService popupService, IPreferencesService preferencesService,
        IServiceProvider serviceProvider, MutualTransactionCategoryBindings mutualTransactionCategoryBindings)
    {
        _simpleTransactionService = simpleTransactionService;
        _popupService = popupService;
        _preferencesService = preferencesService;
        _serviceProvider = serviceProvider;
        MutualTransactionCategoryBindings = mutualTransactionCategoryBindings;
    }

    public void ApplyQueryAttributes(IDictionary<string, object> query)
    {
        SelectedSimpleAccount = query["SimpleAccount"] as SimpleAccount;
    }
}
