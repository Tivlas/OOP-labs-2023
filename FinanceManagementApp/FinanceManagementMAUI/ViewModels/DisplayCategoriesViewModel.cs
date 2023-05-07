using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Abstractions.NotConsole;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Domain.Entities.Transactions;
using FinanceManagementMAUI.Pages;
using FinanceManagementMAUI.Services.PreferencesServices;

namespace FinanceManagementMAUI.ViewModels
{
    public partial class DisplayCategoriesViewModel : ObservableObject
    {
        private readonly ITransactionCategoryService _transactionCategoryService;
        private readonly IPreferencesService _preferencesService;

        public ObservableCollection<TransactionCategory> Categories { get; set; } = new();
        public DisplayCategoriesViewModel(ITransactionCategoryService transactionCategoryService,
            IPreferencesService preferencesService)
        {
            _transactionCategoryService = transactionCategoryService;
            _preferencesService = preferencesService;
        }

        [RelayCommand] async Task DoGetCategories() => await GetCategories();
        async Task GetCategories()
        {
            Categories.Clear();
            var tcs = await _transactionCategoryService.ListAsync(tc => tc.UserId == _preferencesService.Get("id", -1));
            await MainThread.InvokeOnMainThreadAsync(() =>
            {
                foreach(var tc in tcs)
                {
                    Categories.Add(tc);
                }
            });
        }

        [RelayCommand] async Task DoAddCategory() => await AddCategory();

        async Task AddCategory()
        {
            await Shell.Current.GoToAsync(nameof(AddCategoryPage));
        }
    }
}
