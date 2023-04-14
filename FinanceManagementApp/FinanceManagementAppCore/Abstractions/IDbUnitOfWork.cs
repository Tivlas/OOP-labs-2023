namespace Domain.Abstractions;
public interface IDbUnitOfWork : IUnitOfWork
{
    public Task RemoveDatbaseAsync();
    public Task CreateDatabaseAsync();
    public Task SaveAllAsync();
}
