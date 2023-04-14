using Domain.Cards;
using Domain.Entities.Accounts;
using Domain.Entities.Transactions;

namespace Domain.Abstractions.NotForConsoleAsync;
public interface IUnitOfWork
{
    IEntityRepository<SimpleAccount> SimpleAccountsRepository { get; }
    IEntityRepository<Card> CardsRepository { get; }
    IEntityRepository<SimpleTransaction> SimpleTransactionsRepository { get; }
    IEntityRepository<Transfer> TransfersRepository { get; }
    IEntityRepository<TransactionCategory> TransactionCategoriesRepository { get; }

    public Task RemoveDatbaseAsync();
    public Task CreateDatabaseAsync();
    public Task SaveAllAsync();
}
