using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Abstractions.NotConsole;
using CommunityToolkit.Maui.Core;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Domain.Entities.Transactions;
using FinanceManagementMAUI.Services;
using FinanceManagementMAUI.Services.PreferencesServices;

namespace FinanceManagementMAUI.ViewModels
{
    public partial class AddCategoryViewModel : ObservableObject
    {
        private readonly ITransactionCategoryService _transactionCategoryService;
        private readonly IPreferencesService _preferencesService;
        private readonly IPopupService _popupService;

        public AddCategoryViewModel(ITransactionCategoryService transactionCategoryService, IPreferencesService preferencesService,
            IPopupService popupService)
        {
            _transactionCategoryService = transactionCategoryService;
            _preferencesService = preferencesService;
            _popupService = popupService;
        }

        [ObservableProperty] private string _name;
        [RelayCommand] async Task DoAddCategory() => await AddCategory();

        public async Task AddCategory()
        {
            var userId = _preferencesService.Get("id", -1);
            if (userId != -1 && await _transactionCategoryService.FirstOrDefaultAsync(tc => tc.Name == Name) is null)
            {
                var tc = new TransactionCategory(Name, userId);
                await _transactionCategoryService.AddAsync(tc);
                await _transactionCategoryService.SaveChangesAsync();
                await _popupService.ShowToast("Successfully added!", ToastDuration.Long, 14);
            }
        }
    }
}
