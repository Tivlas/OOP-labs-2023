using Domain.Entities;
using Domain.Entities.Accounts;
using Domain.Entities.Transactions;

namespace Domain.Abstractions.NotForConsoleAsync;
public interface IUnitOfWork
{
    IEntityRepository<SimpleAccount> SimpleAccountsRepository { get; }
    IEntityRepository<SimpleTransaction> SimpleTransactionsRepository { get; }
    IEntityRepository<TransactionCategory> TransactionCategoriesRepository { get; }
    IEntityRepository<User> UsersRepository { get; }

    public Task RemoveDatbaseAsync();
    public Task CreateDatabaseAsync();
    public Task SaveAllAsync();
}
