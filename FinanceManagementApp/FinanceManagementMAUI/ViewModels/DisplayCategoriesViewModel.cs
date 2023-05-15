using Application.Abstractions.NotConsole;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Domain.Entities.Transactions;
using FinanceManagementMAUI.Pages;
using FinanceManagementMAUI.Services.Bindings;
using FinanceManagementMAUI.Services.PreferencesServices;

namespace FinanceManagementMAUI.ViewModels
{
    public partial class DisplayCategoriesViewModel : ObservableObject
    {
        private readonly ITransactionCategoryService _transactionCategoryService;
        private readonly IPreferencesService _preferencesService;
        public MutualTransactionCategoryBindings MutualTransactionCategoryBindings { get; }

        public DisplayCategoriesViewModel(ITransactionCategoryService transactionCategoryService,
            IPreferencesService preferencesService, MutualTransactionCategoryBindings mutualTransactionCategoryBindings)
        {
            _transactionCategoryService = transactionCategoryService;
            _preferencesService = preferencesService;
            MutualTransactionCategoryBindings = mutualTransactionCategoryBindings;
        }

        [RelayCommand] async Task DoAddCategory() => await AddCategory();

        async Task AddCategory()
        {
            await Shell.Current.GoToAsync(nameof(AddCategoryPage));
        }

        [RelayCommand] async Task DoEdit(TransactionCategory category) => await Edit(category);

        async Task Edit(TransactionCategory category)
        {
            IDictionary<string, object> parameters = new Dictionary<string, object>()
                {
                    {"TransactionCategory", (category)}
                };

            await Shell.Current.GoToAsync(nameof(EditCategoryPage), parameters);
        }

        [RelayCommand] async Task DoRemove(TransactionCategory category) => await RemoveCategory(category);
        async Task RemoveCategory(TransactionCategory category)
        {
            MutualTransactionCategoryBindings.Remove(category);
            await _transactionCategoryService.DeleteAsync(category);
            await _transactionCategoryService.SaveChangesAsync();
        }
    }
}
