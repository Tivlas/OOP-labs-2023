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

namespace FinanceManagementMAUI.ViewModels
{
    public partial class EditCategoryViewModel : ObservableObject, IQueryAttributable
    {
        private readonly ITransactionCategoryService _transactionCategoryService;
        private readonly IServiceProvider _serviceProvider;
        private readonly IPopupService _popupService;
        [ObservableProperty] private TransactionCategory _selectedCategory;
        [ObservableProperty] private string _newName;

        public EditCategoryViewModel(ITransactionCategoryService transactionCategoryService, IServiceProvider serviceProvider,
            IPopupService popupService)
        {
            _transactionCategoryService = transactionCategoryService;
            _serviceProvider = serviceProvider;
            _popupService = popupService;
        }

        public void ApplyQueryAttributes(IDictionary<string, object> query)
        {
            SelectedCategory = query["TransactionCategory"] as TransactionCategory;
        }

        [RelayCommand] async Task DoEditCategory() => await EditCategory();
        async Task EditCategory()
        {
            var tcs = _serviceProvider.GetRequiredService<DisplayCategoriesViewModel>().Categories;
            var tcToEdit = await _transactionCategoryService.FirstOrDefaultAsync(tc => tc.Name == SelectedCategory.Name);
            if (tcToEdit is not null)
            {
                tcToEdit.Name = NewName ?? Constants.NoCategory;
                await _transactionCategoryService.UpdateAsync(tcToEdit);
                await _transactionCategoryService.SaveChangesAsync();
            }
            await MainThread.InvokeOnMainThreadAsync(() =>
            {
                int index = tcs.IndexOf(SelectedCategory);
                SelectedCategory.Name = NewName ?? Constants.NoCategory;
                tcs[index] = SelectedCategory;

            });
            await _popupService.ShowToast("Successfully edited", ToastDuration.Short, 14);
        }
    }
}
