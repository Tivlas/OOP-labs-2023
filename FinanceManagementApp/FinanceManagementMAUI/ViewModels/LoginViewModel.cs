using Application.Abstractions.NotConsole;
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
    [ObservableProperty] private string _email;
    [ObservableProperty] private string _password;
    [ObservableProperty] private bool _showActivityIndicator;
    [ObservableProperty] private bool _isEmailValid = true;
    [ObservableProperty] private bool _isPasswordValid = true;

    public LoginViewModel(IUserService userService, IPasswordValidator passwordValidator, IEmailValidator emailValidator)
    {
        _userService = userService;
        _passwordValidator = passwordValidator;
        _emailValidator = emailValidator;
    }

    [RelayCommand] async Task DoLogin() => await Login();

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
            ShowActivityIndicator = true;
            await _userService.AddAsync(new User(Email, Password));
            ShowActivityIndicator = false;
        }
    }
}
