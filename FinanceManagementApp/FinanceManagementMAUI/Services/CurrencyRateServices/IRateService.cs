using FinanceManagementMAUI.Models;

namespace FinanceManagementMAUI.Services.CurrencyRateServices;
public interface IRateService
{
    Task<Rate> GetRateAsync(DateTime date, Currency currency);
}
