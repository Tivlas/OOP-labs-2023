namespace Domain.Entities.Interfaces
{
    public interface IEntity
    {
        int Id { get; }

        IEnumerable<(string PropName, object propValue)> GetInfo();

    }
}