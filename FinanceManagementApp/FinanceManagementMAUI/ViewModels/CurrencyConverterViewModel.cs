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
using FinanceManagementMAUI.Services.CurrencyRateServices;

namespace FinanceManagementMAUI.ViewModels;
public partial class CurrencyConverterViewModel : ObservableObject
{
    private readonly ICurrencyService _currencyService;
    private readonly IRateService _rateService;
    private Currency? _fromCurrency = null;
    private Currency? _toCurrency = null;

    [ObservableProperty] private DateTime _selectedDate = DateTime.Today;
    [ObservableProperty] private string _resultText = "0";
    [ObservableProperty] private string _entryText;
    [ObservableProperty] private Currency? _upperPickerItem;
    [ObservableProperty] private Currency? _lowerPickerItem;
    [ObservableProperty] private bool _showActivityIndicator;
    public ObservableCollection<Currency> Currencies { get; set; } = new();

    public CurrencyConverterViewModel(ICurrencyService currencyService, IRateService rateService)
    {
        _currencyService = currencyService;
        _rateService = rateService;
    }

    [RelayCommand]
    void DoLoadCurrencies()
    {
        Task.Run(LoadCurrencies);
    }
    private async Task LoadCurrencies()
    {
        ShowActivityIndicator = true;
        var currencies = await _currencyService.GetCurrenciesAsync();
        await MainThread.InvokeOnMainThreadAsync(() =>
        {
            foreach (var cur in currencies)
            {
                Currencies.Add(cur);
            }
            Currencies.Add(new Currency { Cur_Abbreviation = "BYN", Cur_Scale = 1 });
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
            Rate fromRate = _fromCurrency?.Cur_Abbreviation == "BYN" ?
                new Rate { Cur_OfficialRate = 1, Cur_Scale = 1 } :
                await _rateService.GetRateAsync(SelectedDate, _fromCurrency);

            Rate toRate = _toCurrency!.Cur_Abbreviation == "BYN" ?
                new Rate { Cur_OfficialRate = 1, Cur_Scale = 1 } :
                await _rateService.GetRateAsync(SelectedDate, _toCurrency);


            decimal? BYN = number * fromRate?.Cur_OfficialRate / fromRate?.Cur_Scale;

            result = BYN / toRate?.Cur_OfficialRate * toRate?.Cur_Scale;
        }
        ResultText = $"{result:0.##}";
    }


    [RelayCommand] async Task DoDateSelected() => await DateSelected();
    private async Task DateSelected()
    {
        if (UpperPickerItem is not null && LowerPickerItem is not null)
        {
            await Convert();
        }
    }


    [RelayCommand] async Task DoUpperPickerChanged() => await UpperPickerChanged();
    private async Task UpperPickerChanged()
    {
        if (LowerPickerItem is not null)
        {
            await Convert();
        }
    }


    [RelayCommand] async Task DoLowerPickerChanged() => await LowerPickerChanged();
    private async Task LowerPickerChanged()
    {
        if (UpperPickerItem is not null)
        {
            await Convert();
        }
    }


    [RelayCommand] async Task DoEntryTextChanged() => await EntryTextChanged();
    private async Task EntryTextChanged()
    {
        await Convert();
    }
}
