using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Application.Abstractions.NotConsole;
using Application.Services.NotConsole;
using CommunityToolkit.Maui.Core;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Domain.Entities.Accounts;
using FinanceManagementMAUI.Services;
using FinanceManagementMAUI.Services.PreferencesServices;

namespace FinanceManagementMAUI.ViewModels;
public partial class EditSimpleAccountViewModel : ObservableObject, IQueryAttributable
{
    private readonly ISimpleAccountService _simpleAccountService;
    private readonly IServiceProvider _serviceProvider;
    private readonly IPopupService _popupService;
    private readonly IPreferencesService _preferencesService;
    [ObservableProperty] private SimpleAccount _selectedAccount;
    [ObservableProperty] private string _newName;
    [ObservableProperty] private string _newCurrencyName;
    [ObservableProperty] private string _newBalance;
    public EditSimpleAccountViewModel(ISimpleAccountService simpleAccountService, IServiceProvider serviceProvider,
            IPopupService popupService, IPreferencesService preferencesService)
    {
        _simpleAccountService = simpleAccountService;
        _serviceProvider = serviceProvider;
        _popupService = popupService;
        _preferencesService = preferencesService;
    }

    public void ApplyQueryAttributes(IDictionary<string, object> query)
    {
        SelectedAccount = query["SimpleAccount"] as SimpleAccount;
        NewName = SelectedAccount.Name;
        NewBalance = SelectedAccount.Balance.ToString();
        NewCurrencyName = SelectedAccount.CurrencyName;
    }

    [RelayCommand] async Task DoEditCategory() => await EditCategory();
    async Task EditCategory()
    {
        if (string.IsNullOrWhiteSpace(NewName) || string.IsNullOrWhiteSpace(NewCurrencyName))
        {
            await _popupService.Alert("Wrong input", "Name and currency name must not be empty or white space", "Ok");
        }
        else if (!decimal.TryParse(NewBalance, out decimal balance) || balance < 0)
        {
            await _popupService.Alert("Wrong balance input", "Enter number greater than zero", "Ok");
        }
        else
        {
            var accToEdit = await _simpleAccountService.FirstOrDefaultAsync(sa => sa.UserId == _preferencesService.Get("id", -1) &&
            sa.Name == SelectedAccount.Name);
            if (accToEdit is not null)
            {
                accToEdit.Name = NewName;
                accToEdit.CurrencyName = NewCurrencyName;
                accToEdit.Balance = balance;
                await _simpleAccountService.UpdateAsync(accToEdit);
                await _simpleAccountService.SaveChangesAsync();
            }

            await MainThread.InvokeOnMainThreadAsync(() =>
            {
                var saccs = _serviceProvider.GetRequiredService<DisplaySimpleAccountsViewModel>().SimpleAccounts;
                int index = saccs.IndexOf(SelectedAccount);
                SelectedAccount.Name = NewName;
                SelectedAccount.CurrencyName = NewCurrencyName;
                SelectedAccount.Balance = balance;
                saccs[index] = SelectedAccount;
            });
            await _popupService.ShowToast("Successfully added!", ToastDuration.Short, 14);
        }
    }
}
