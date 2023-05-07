namespace Domain.Entities.Interfaces
{
    public interface IEntity
    {
        int Id { get; set; }

        IEnumerable<(string PropName, object propValue)> GetInfo();

    }
}