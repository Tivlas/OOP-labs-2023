using Application.Abstractions.NotConsole;
using CommunityToolkit.Maui.Core;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Domain.Entities.Transactions;
using FinanceManagementMAUI.Services;
using FinanceManagementMAUI.Services.Bindings;
using FinanceManagementMAUI.Services.PreferencesServices;

namespace FinanceManagementMAUI.ViewModels
{
    public partial class EditSimpleTransactionViewModel : ObservableObject, IQueryAttributable
    {
        private readonly IPopupService _popupService;
        private readonly ISimpleTransactionService _simpleTransactionService;
        private readonly IPreferencesService _preferencesService;
        private readonly MutualSimpleTransactionBinding _mutualSimpleTransactionBinding;
        private readonly ISimpleAccountService _simpleAccountService;
        private readonly MutualSimpleAccountsBinding _mutualSimpleAccountsBinding;
        [ObservableProperty] private string _newComment;
        [ObservableProperty] private DateTime _newDate;
        [ObservableProperty] private string _newAmountOfMoney;
        [ObservableProperty] private TransactionCategory _newCategory;
        [ObservableProperty] private SimpleTransaction _selectedSimpleTransaction;
        public EditSimpleTransactionViewModel(MutualTransactionCategoryBindings mutualTransactionCategoryBindings,
            IPopupService popupService, ISimpleTransactionService simpleTransactionService,
            IPreferencesService preferencesService, MutualSimpleTransactionBinding mutualSimpleTransactionBinding,
            ISimpleAccountService simpleAccountService, MutualSimpleAccountsBinding mutualSimpleAccountsBinding)
        {
            MutualTransactionCategoryBindings = mutualTransactionCategoryBindings;
            _popupService = popupService;
            _simpleTransactionService = simpleTransactionService;
            _preferencesService = preferencesService;
            _mutualSimpleTransactionBinding = mutualSimpleTransactionBinding;
            _simpleAccountService = simpleAccountService;
            _mutualSimpleAccountsBinding = mutualSimpleAccountsBinding;
        }

        public MutualTransactionCategoryBindings MutualTransactionCategoryBindings { get; }

        public void ApplyQueryAttributes(IDictionary<string, object> query)
        {
            SelectedSimpleTransaction = query["SimpleTransaction"] as SimpleTransaction;
            NewComment = SelectedSimpleTransaction.Comment;
            NewDate = SelectedSimpleTransaction.TransactionDate;
            NewAmountOfMoney = SelectedSimpleTransaction.AmountOfMoney.ToString();
        }

        [RelayCommand] async Task DoEditTransaction() => await EditTransaction();

        async Task EditTransaction()
        {
            decimal prevMoney = SelectedSimpleTransaction.AmountOfMoney;
            NewComment ??= string.Empty;
            if (!decimal.TryParse(NewAmountOfMoney, out decimal money))
            {
                await _popupService.Alert("Wrong input", "Amount of money must be a number greater than 0", "Ok");
            }
            else if (money <= 0)
            {
                await _popupService.Alert("Wrong input", "Amount of money must be a number greater than 0", "Ok");
            }
            else
            {
                if (NewCategory is null)
                {
                    NewCategory = SelectedSimpleTransaction.Category;
                }
                int trnIndex = _mutualSimpleTransactionBinding.SimpleTransactions.IndexOf(SelectedSimpleTransaction);
                SelectedSimpleTransaction.Category = NewCategory;
                SelectedSimpleTransaction.CategoryId = NewCategory.Id;
                SelectedSimpleTransaction.Comment = NewComment;
                SelectedSimpleTransaction.TransactionDate = NewDate;
                SelectedSimpleTransaction.AmountOfMoney = money;
                _mutualSimpleTransactionBinding.Edit(SelectedSimpleTransaction, trnIndex);
                var trnToEdit = await _simpleTransactionService.FirstOrDefaultAsync(t => t.Id == SelectedSimpleTransaction.Id);
                if (trnToEdit is not null)
                {
                    trnToEdit.Category = NewCategory;
                    trnToEdit.CategoryId = NewCategory.Id;
                    trnToEdit.Comment = NewComment;
                    trnToEdit.TransactionDate = NewDate;
                    trnToEdit.AmountOfMoney = money;
                    await _simpleTransactionService.UpdateAsync(trnToEdit);
                    await _simpleTransactionService.SaveChangesAsync();
                }
                else
                {
                    return;
                }

                var accToEdit = await _simpleAccountService.FirstOrDefaultAsync(a => a.Id == SelectedSimpleTransaction.SimpleAccountId);
                if (accToEdit is not null)
                {
                    decimal diff = money - prevMoney;
                    int accIndex = _mutualSimpleAccountsBinding.SimpleAccounts.IndexOf(accToEdit);
                    accToEdit.Balance += diff;
                    _mutualSimpleAccountsBinding.Edit(accToEdit, accIndex);
                    await _simpleAccountService.UpdateAsync(accToEdit);
                    await _simpleAccountService.SaveChangesAsync();
                }
                await _popupService.ShowToast("Successfully edited", ToastDuration.Short, 14);
            }
        }
    }
}
