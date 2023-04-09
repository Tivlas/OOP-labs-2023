namespace FinanceManagementAppCore.Interfaces
{
    public interface IEntity
    {
        int Id { get; }

        IEnumerable<(string PropName, object propValue)> GetInfo();

    }
}