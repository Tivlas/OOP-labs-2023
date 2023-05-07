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
using FinanceManagementMAUI.Const;
using FinanceManagementMAUI.Services;
using FinanceManagementMAUI.Services.PreferencesServices;
using Microsoft.VisualBasic;

namespace FinanceManagementMAUI.ViewModels
{
    public partial class AddCategoryViewModel : ObservableObject
    {
        private readonly ITransactionCategoryService _transactionCategoryService;
        private readonly IPreferencesService _preferencesService;
        private readonly IPopupService _popupService;
        private readonly IServiceProvider _serviceProvider;

        public AddCategoryViewModel(ITransactionCategoryService transactionCategoryService, IPreferencesService preferencesService,
            IPopupService popupService, IServiceProvider serviceProvider)
        {
            _transactionCategoryService = transactionCategoryService;
            _preferencesService = preferencesService;
            _popupService = popupService;
            _serviceProvider = serviceProvider;
        }

        [ObservableProperty] private string _name;
        [RelayCommand] async Task DoAddCategory() => await AddCategory();

        public async Task AddCategory()
        {
            var userId = _preferencesService.Get("id", -1);
            Name ??= Const.Constants.NoCategory;
            if (userId != -1 && await _transactionCategoryService.FirstOrDefaultAsync(tc => tc.Name == Name && tc.UserId == userId) is null)
            {
                
                var tc = new TransactionCategory(Name, userId);
                await _transactionCategoryService.AddAsync(tc);
                await _transactionCategoryService.SaveChangesAsync();
                var tcs = _serviceProvider.GetRequiredService<DisplayCategoriesViewModel>().Categories;
                await MainThread.InvokeOnMainThreadAsync(() =>
                {
                    tcs.Add(tc);
                });
                await _popupService.ShowToast("Successfully added!", ToastDuration.Short, 14);
            }
        }
    }
}
