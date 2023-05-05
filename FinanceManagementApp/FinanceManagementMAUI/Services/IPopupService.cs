using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinanceManagementMAUI.Services;
public interface IPopupService
{
    Task Alert(string title, string message, string cancel)
    {
        return App.Current.MainPage.DisplayAlert(title, message, cancel);
    }
}
