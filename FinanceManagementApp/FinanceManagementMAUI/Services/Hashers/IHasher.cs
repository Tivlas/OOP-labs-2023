namespace FinanceManagementMAUI.Services
{
    public interface IHasher
    {
        string Hash(string value);
        bool Verify(string hashedValue, string originalValue);
    }
}
