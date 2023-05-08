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
using FinanceManagementMAUI.Services.Bindings;
using FinanceManagementMAUI.Services.PreferencesServices;

namespace FinanceManagementMAUI.ViewModels;
public partial class EditSimpleAccountViewModel : ObservableObject, IQueryAttributable
{
    private readonly ISimpleAccountService _simpleAccountService;
    private readonly IPopupService _popupService;
    private readonly IPreferencesService _preferencesService;
    private readonly MutualSimpleAccountsBinding _mutualSimpleAccountsBinding;
    [ObservableProperty] private SimpleAccount _selectedAccount;
    [ObservableProperty] private string _newName;
    [ObservableProperty] private string _newCurrencyName;
    [ObservableProperty] private string _newBalance;
    public EditSimpleAccountViewModel(ISimpleAccountService simpleAccountService,
            IPopupService popupService, IPreferencesService preferencesService, MutualSimpleAccountsBinding mutualSimpleAccountsBinding)
    {
        _simpleAccountService = simpleAccountService;
        _popupService = popupService;
        _preferencesService = preferencesService;
        _mutualSimpleAccountsBinding = mutualSimpleAccountsBinding;
    }

    public void ApplyQueryAttributes(IDictionary<string, object> query)
    {
        SelectedAccount = query["SimpleAccount"] as SimpleAccount;
        NewName = SelectedAccount.Name;
        NewBalance = SelectedAccount.Balance.ToString();
        NewCurrencyName = SelectedAccount.CurrencyName;
    }

    [RelayCommand] async Task DoEditCard() => await EditCard();
    async Task EditCard()
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
                int index = _mutualSimpleAccountsBinding.SimpleAccounts.IndexOf(SelectedAccount);
                SelectedAccount.Name = NewName;
                SelectedAccount.CurrencyName = NewCurrencyName;
                SelectedAccount.Balance = balance;
                _mutualSimpleAccountsBinding.SimpleAccounts[index] = SelectedAccount;
            });
            await _popupService.ShowToast("Successfully edited!", ToastDuration.Short, 14);
        }
    }
}
