using FinanceManagementMAUI.Models;

namespace FinanceManagementMAUI.Services.CurrencyRateServices;
public interface ICurrencyService
{
    Task<IEnumerable<Currency>> GetCurrenciesAsync();
}
