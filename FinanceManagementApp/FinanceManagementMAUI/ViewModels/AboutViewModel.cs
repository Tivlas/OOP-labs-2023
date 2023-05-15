using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace FinanceManagementMAUI.ViewModels;
public partial class AboutViewModel : ObservableObject
{
    [RelayCommand] async Task DoLookAtSourceCode(string url) => await LookAtSourceCode(url);
    async Task LookAtSourceCode(string url)
    {
        await Launcher.OpenAsync(url);
    }
}
