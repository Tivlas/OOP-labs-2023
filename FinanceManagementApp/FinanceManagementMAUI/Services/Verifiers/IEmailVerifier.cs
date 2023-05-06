namespace FinanceManagementMAUI.Services
{
    public interface IEmailVerifier
    {
        Task<bool> Verify(string email);
    }
}
