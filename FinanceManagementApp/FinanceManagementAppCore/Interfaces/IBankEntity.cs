namespace FinanceManagementAppCore.Interfaces
{
    public interface IBankEntity : IEntity
    {
        string Name { get; set; }
        decimal Balance { get; set; }
        string CurrencyName { get; set; }
        int UserId { get; init; }

        IEnumerable<(string PropName, object propValue)> GetInfo();
    }
}