using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Maui.Core;

namespace FinanceManagementMAUI.Services;
public interface IPopupService
{
    Task Alert(string title, string message, string cancel)
    {
        return App.Current.MainPage.DisplayAlert(title, message, cancel);
    }

    Task ShowToast(string text, ToastDuration duration, double fontSize)
    {
        CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();
        var toast = Toast.Make(text, duration, fontSize);
        return toast.Show(cancellationTokenSource.Token);
    }
}
