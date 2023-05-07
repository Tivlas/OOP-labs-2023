using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Abstractions.NotConsole;
using CommunityToolkit.Mvvm.ComponentModel;
using FinanceManagementMAUI.Services;

namespace FinanceManagementMAUI.ViewModels
{
    public partial class AddSimpleAccountViewModel : ObservableObject
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly ISimpleAccountService _simpleAccountService;
        private readonly IPopupService _popupService;

        public AddSimpleAccountViewModel(IServiceProvider serviceProvider,ISimpleAccountService simpleAccountService,
            IPopupService popupService)
        {
            _serviceProvider = serviceProvider;
            _simpleAccountService = simpleAccountService;
            _popupService = popupService;
        }

        [ObservableProperty] private string _name;
        [ObservableProperty] private string _currencyName;
        [ObservableProperty] private string _balance;
    }
}
