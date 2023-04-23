namespace Domain.Entities.Interfaces
{
    public interface IBankEntity : INamedEntity, IRelatedToUser
    {
        decimal Balance { get; set; }
        string CurrencyName { get; set; }
    }
}