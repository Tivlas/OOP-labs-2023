using System.Net.Http.Json;
using FinanceManagementMAUI.Models;

namespace FinanceManagementMAUI.Services.CurrencyRateServices;
public class RateService : IRateService
{
    private HttpClient _client;

    public RateService(HttpClient client)
    {
        _client = client;
    }

    public async Task<Rate> GetRateAsync(DateTime date, Currency currency)
    {
        var result = _client.Send(new HttpRequestMessage(HttpMethod.Get, $"{currency.Cur_ID}?ondate={date:yyyy-MM-dd}"));
        var response = await HttpContentJsonExtensions.ReadFromJsonAsync<Rate>(result.Content);
        return response!;
    }
}
