﻿using System.Net.Http.Json;
using FinanceManagementMAUI.Models;

namespace FinanceManagementMAUI.Services.CurrencyRateServices;
public class CurrencyService : ICurrencyService
{
    private HttpClient _client;

    public CurrencyService(HttpClient client)
    {
        _client = client;
    }

    public async Task<IEnumerable<Currency>> GetCurrenciesAsync()
    {
        var result = await _client.SendAsync(new HttpRequestMessage(HttpMethod.Get, _client.BaseAddress));
        var response = await HttpContentJsonExtensions.ReadFromJsonAsync<IEnumerable<Currency>>(result.Content);
        return response.Where(currency => currency.Cur_DateEnd > DateTime.Today);
    }
}
