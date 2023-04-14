using Domain.Cards;
using Domain.Entities.Accounts;
using Domain.Entities.Transactions;

namespace Domain.Abstractions;
public interface IUnitOfWork
{
    IEntityRepository<SimpleAccount> SimpleAccountsRepository { get; }
    IEntityRepository<Card> CardsRepository { get; }
    IEntityRepository<SimpleTransaction> SimpleTransactionsRepository { get; }
    IEntityRepository<Transfer> TransfersRepository { get; }
    IEntityRepository<TransactionCategory> TransactionCategoriesRepository { get; }
}
