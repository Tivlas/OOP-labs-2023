using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FinanceManagementMAUI.Models;

namespace FinanceManagementMAUI.Services.CurrencyRateServices;
public interface ICurrencyService
{
    Task<IEnumerable<Currency>> GetCurrenciesAsync();
}
