﻿using Application.Abstractions.NotConsole;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Domain.Entities;
using FinanceManagementMAUI.Services;

namespace FinanceManagementMAUI.ViewModels;
public partial class LoginViewModel : ObservableObject
{
    private readonly IUserService _userService;
    private readonly IPasswordValidator _passwordValidator;
    private readonly IEmailValidator _emailValidator;
    private readonly IPasswordHasher _passwordHasher;
    private readonly IPopupService _popupService;
    private readonly IEmailVerifier _emailVerifier;
    [ObservableProperty] private string _email;
    [ObservableProperty] private string _password;
    [ObservableProperty] private bool _showActivityIndicator;
    [ObservableProperty] private bool _isEmailValid = true;
    [ObservableProperty] private bool _isPasswordValid = true;

    public LoginViewModel(IUserService userService, IPasswordValidator passwordValidator,
        IEmailValidator emailValidator, IPasswordHasher passwordHasher, 
        IPopupService popupService, IEmailVerifier emailVerifier)
    {
        _userService = userService;
        _passwordValidator = passwordValidator;
        _emailValidator = emailValidator;
        _passwordHasher = passwordHasher;
        _popupService = popupService;
        _emailVerifier = emailVerifier;
    }

    [RelayCommand] async Task DoLogin() => await Login();
    [RelayCommand] async Task DoCreateAccount() => await CreateAccount();

    private void SetValidFlags()
    {
        IsEmailValid = _emailValidator.IsValid(Email);
        IsPasswordValid = _passwordValidator.IsValid(Password);
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
                // TODO
            }

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
                bool valid = await _emailVerifier.Verify(Email);
                ShowActivityIndicator = false;
                if (!valid)
                {
                    await _popupService.Alert("Wrong email address", "Such email does not exist!", "Ok");
                }
                else
                {
                    await _userService.AddAsync(new User(Email, _passwordHasher.Hash(Password)));
                }
            }
        }
    }
}
