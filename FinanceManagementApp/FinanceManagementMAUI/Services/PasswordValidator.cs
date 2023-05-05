namespace FinanceManagementMAUI.Services;
public class PasswordValidator : IPasswordValidator
{
    public bool IsValid(string? password)
    {
        if (string.IsNullOrWhiteSpace(password))
        {
            return false;
        }
        return true;
    }
}
