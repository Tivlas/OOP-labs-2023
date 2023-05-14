using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using FinanceManagementMAUI.Models;
using FinanceManagementMAUI.Pages;
using FinanceManagementMAUI.Services;
using FinanceManagementMAUI.Services.CurrencyRateServices;

namespace FinanceManagementMAUI.ViewModels;
public partial class CurrencyConverterViewModel : ObservableObject
{
    private readonly ICurrencyService _currencyService;
    private readonly IRateService _rateService;
    private readonly IPopupService _popupService;
    private Currency? _fromCurrency = null;
    private Currency? _toCurrency = null;

    [ObservableProperty] private DateTime _selectedDate = DateTime.Today;
    [ObservableProperty] private string _resultText = "0";
    [ObservableProperty] private string _entryText;
    [ObservableProperty] private Currency? _upperPickerItem;
    [ObservableProperty] private Currency? _lowerPickerItem;
    [ObservableProperty] private bool _showActivityIndicator;
    public ObservableCollection<Currency> Currencies { get; set; } = new();

    public CurrencyConverterViewModel(ICurrencyService currencyService, IRateService rateService,
        IPopupService popupService)
    {
        _currencyService = currencyService;
        _rateService = rateService;
        _popupService = popupService;
    }

    private bool CheckAndAlertIfNoInternetConnection()
    {
        NetworkAccess access = Connectivity.Current.NetworkAccess;
        if (access != NetworkAccess.Internet)
        {
            _popupService.Alert("Error", "No internet connection", "Ok");
            return false;
        }
        return true;
    }

    [RelayCommand]
    void DoLoadCurrencies()
    {
        if (CheckAndAlertIfNoInternetConnection() && Currencies.Count() == 0)
        {
            Task.Run(LoadCurrencies);
        }
    }
    private async Task LoadCurrencies()
    {
        ShowActivityIndicator = true;
        var currencies = await _currencyService.GetCurrenciesAsync();
        await MainThread.InvokeOnMainThreadAsync(() =>
        {
            Currencies.Add(new Currency { Cur_Abbreviation = "BYN", Cur_Scale = 1 });
            foreach (var cur in currencies)
            {
                Currencies.Add(cur);
            }
        });
        ShowActivityIndicator = false;
    }

    private async Task Convert()
    {
        _fromCurrency = UpperPickerItem;
        _toCurrency = LowerPickerItem;

        if (_fromCurrency is null || _toCurrency is null)
        {
            ResultText = "ERROR";
            return;
        }

        decimal number;
        if (!decimal.TryParse(EntryText, out number))
        {
            ResultText = "ERROR";
            return;
        }

        decimal? result = number;
        if (!(_fromCurrency?.Cur_Name == _toCurrency?.Cur_Name))
        {
            ShowActivityIndicator = true;
            Rate fromRate = _fromCurrency?.Cur_Abbreviation == "BYN" ?
                new Rate { Cur_OfficialRate = 1, Cur_Scale = 1 } :
                await _rateService.GetRateAsync(SelectedDate, _fromCurrency);

            Rate toRate = _toCurrency!.Cur_Abbreviation == "BYN" ?
                new Rate { Cur_OfficialRate = 1, Cur_Scale = 1 } :
                await _rateService.GetRateAsync(SelectedDate, _toCurrency);

            ShowActivityIndicator = false;
            decimal? BYN = number * fromRate?.Cur_OfficialRate / fromRate?.Cur_Scale;

            result = BYN / toRate?.Cur_OfficialRate * toRate?.Cur_Scale;
        }
        ResultText = $"{result:0.##}";
    }


    [RelayCommand]
    void DoDateSelected()
    {
        if (CheckAndAlertIfNoInternetConnection())
        {
            Task.Run(DateSelected);
        }
    }
    private async Task DateSelected()
    {
        if (UpperPickerItem is not null && LowerPickerItem is not null)
        {
            await Convert();
        }
    }


    [RelayCommand]
    void DoUpperPickerChanged()
    {
        if (CheckAndAlertIfNoInternetConnection())
        {
            Task.Run(UpperPickerChanged);
        }
    }
    private async Task UpperPickerChanged()
    {
        if (LowerPickerItem is not null)
        {
            await Convert();
        }
    }


    [RelayCommand]
    void DoLowerPickerChanged()
    {
        if (CheckAndAlertIfNoInternetConnection())
        {
            Task.Run(LowerPickerChanged);
        }
    }
    private async Task LowerPickerChanged()
    {
        if (UpperPickerItem is not null)
        {
            await Convert();
        }
    }


    [RelayCommand]
    void DoEntryTextChanged()
    {
        if (CheckAndAlertIfNoInternetConnection())
        {
            Task.Run(EntryTextChanged);
        }
    }
    private async Task EntryTextChanged()
    {
        await Convert();
    }
}
