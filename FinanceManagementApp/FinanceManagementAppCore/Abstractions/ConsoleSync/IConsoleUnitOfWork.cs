using Domain.Cards;
using Domain.Entities;
using Domain.Entities.Accounts;
using Domain.Entities.Transactions;

namespace Domain.Abstractions.ConsoleSync;
public interface IConsoleUnitOfWork
{
    IConsoleEntityRepository<SimpleAccount> SimpleAccountsRepository { get; }
    IConsoleEntityRepository<Card> CardsRepository { get; }
    IConsoleTransactionRepository<SimpleTransaction> SimpleTransactionsRepository { get; }
    IConsoleEntityRepository<Transfer> TransfersRepository { get; }
    IConsoleEntityRepository<TransactionCategory> TransactionCategoriesRepository { get; }
    IConsoleEntityRepository<User> UsersRepository { get; }
}
