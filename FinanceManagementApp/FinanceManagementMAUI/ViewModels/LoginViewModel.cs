using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;

namespace FinanceManagementMAUI.ViewModels;
public partial class LoginViewModel : ObservableObject
{
    [ObservableProperty] private string _email;
}
