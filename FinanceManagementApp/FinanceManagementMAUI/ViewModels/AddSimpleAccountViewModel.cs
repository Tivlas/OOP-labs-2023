using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Abstractions.NotConsole;
using Application.Services.NotConsole;
using CommunityToolkit.Maui.Core;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Domain.Entities.Accounts;
using FinanceManagementMAUI.Services;
using FinanceManagementMAUI.Services.Bindings;
using FinanceManagementMAUI.Services.PreferencesServices;

namespace FinanceManagementMAUI.ViewModels
{
    public partial class AddSimpleAccountViewModel : ObservableObject
    {
        private readonly ISimpleAccountService _simpleAccountService;
        private readonly IPopupService _popupService;
        private readonly IPreferencesService _preferencesService;
        private readonly MutualSimpleAccountsBinding _mutualSimpleAccountsBinding;

        public AddSimpleAccountViewModel(ISimpleAccountService simpleAccountService, IPopupService popupService,
            IPreferencesService preferencesService, MutualSimpleAccountsBinding mutualSimpleAccountsBinding)
        {
            _simpleAccountService = simpleAccountService;
            _popupService = popupService;
            _preferencesService = preferencesService;
            _mutualSimpleAccountsBinding = mutualSimpleAccountsBinding;
        }

        [ObservableProperty] private string _name;
        [ObservableProperty] private string _currencyName;
        [ObservableProperty] private string _balance;

        [RelayCommand] async Task DoAddCard() => await AddCard();

        async Task AddCard()
        {
            var userId = _preferencesService.Get("id", -1);
            if (string.IsNullOrWhiteSpace(Name) || string.IsNullOrWhiteSpace(CurrencyName))
            {
                await _popupService.Alert("Wrong input", "Name and currency name must not be empty or white space", "Ok");
            }
            else if (!decimal.TryParse(Balance, out decimal balance) || balance < 0)
            {
                await _popupService.Alert("Wrong balance input", "Enter number greater than zero", "Ok");
            }
            else if (userId != -1)
            {
                var card = new SimpleAccount(balance, CurrencyName, Name, userId);
                await _simpleAccountService.AddAsync(card);
                await _simpleAccountService.SaveChangesAsync();
                _mutualSimpleAccountsBinding.Add(card);
                await _popupService.ShowToast("Successfully added!", ToastDuration.Short, 14);
            }
        }
    }
}
