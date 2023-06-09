﻿using Application.Abstractions.NotConsole;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Domain.Entities;
using FinanceManagementMAUI.Services;
using FinanceManagementMAUI.Services.Bindings;
using FinanceManagementMAUI.Services.PreferencesServices;

namespace FinanceManagementMAUI.ViewModels;
public partial class LoginViewModel : ObservableObject
{
    private readonly IUserService _userService;
    private readonly IPasswordValidator _passwordValidator;
    private readonly IEmailValidator _emailValidator;
    private readonly IPasswordHasher _passwordHasher;
    private readonly IPopupService _popupService;
    private readonly IEmailVerifier _emailVerifier;
    private readonly IPreferencesService _preferencesService;
    private readonly IServiceProvider _serviceProvider;
    [ObservableProperty] private string _email;
    [ObservableProperty] private string _password;
    [ObservableProperty] private bool _showActivityIndicator;
    [ObservableProperty] private bool _isEmailValid = true;
    [ObservableProperty] private bool _isPasswordValid = true;

    public LoginViewModel()
    {

    }

    public LoginViewModel(IUserService userService, IPasswordValidator passwordValidator,
        IEmailValidator emailValidator, IPasswordHasher passwordHasher,
        IPopupService popupService, IEmailVerifier emailVerifier, IPreferencesService preferencesService,
        IServiceProvider serviceProvider)
    {
        _userService = userService;
        _passwordValidator = passwordValidator;
        _emailValidator = emailValidator;
        _passwordHasher = passwordHasher;
        _popupService = popupService;
        _emailVerifier = emailVerifier;
        _preferencesService = preferencesService;
        _serviceProvider = serviceProvider;
    }

    [RelayCommand] async Task DoLogin() => await Login();
    [RelayCommand] async Task DoCreateAccount() => await CreateAccount();

    private void SetValidFlags()
    {
        IsEmailValid = _emailValidator.IsValid(Email);
        IsPasswordValid = _passwordValidator.IsValid(Password);
    }

    private void SetFlagsToTrue()
    {
        // чтобы текст в entry не оставался красным
        IsEmailValid = true;
        IsPasswordValid = true;
    }

    private bool BothFlagsValid()
    {
        return IsPasswordValid && IsEmailValid;
    }

    public async Task Login()
    {
        SetValidFlags();
        if (BothFlagsValid())
        {
            var user = await _userService.FirstOrDefaultAsync(u => u.Email == Email);
            if (user is null)
            {
                await _popupService.Alert(":(", "Invalid email or password", "Ok");
            }
            else if (!_passwordHasher.Verify(user.Password, Password))
            {
                await _popupService.Alert(":(", "Invalid email or password", "Ok");
            }
            else
            {
                _preferencesService.SetUserData(user.Id, user.Email);
                ReloadUserCollections();
                await Shell.Current.GoToAsync("//MainPage");
            }
        }
        else
        {
            await Task.Delay(600);
            SetFlagsToTrue();
        }
    }

    public async Task CreateAccount()
    {
        SetValidFlags();
        if (BothFlagsValid())
        {
            var user = await _userService.FirstOrDefaultAsync(u => u.Email == Email);
            if (user is null)
            {
                ShowActivityIndicator = true;
                //bool valid = await _emailVerifier.Verify(Email); // Закоментил, тк в месяц ограничение на количество проверок
                bool valid = true;
                ShowActivityIndicator = false;
                if (!valid)
                {
                    await _popupService.Alert("Wrong email address", "Such email does not exist!", "Ok");
                }
                else
                {
                    var u = new User(Email, _passwordHasher.Hash(Password));
                    await _userService.AddAsync(u);
                    await _userService.SaveChangesAsync();
                    _preferencesService.SetUserData(u.Id, u.Email);
                    await Shell.Current.GoToAsync("//MainPage");
                }
            }
            else
            {
                await _popupService.Alert("Error", "Already exists", "Ok");
            }
        }
        else
        {
            await Task.Delay(600);
            SetFlagsToTrue();
        }
    }

    private void ReloadUserCollections()
    {
        var categories = _serviceProvider.GetRequiredService<MutualTransactionCategoryBindings>();
        var accs = _serviceProvider.GetRequiredService<MutualSimpleAccountsBinding>();
        var transactions = _serviceProvider.GetRequiredService<MutualSimpleTransactionBinding>();
        Task.Run(categories.Reload);
        Task.Run(accs.Reload);
        Task.Run(transactions.Reload);
    }
}
