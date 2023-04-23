using Domain.Entities;
using Domain.Entities.Accounts;
using Domain.Entities.Transactions;

namespace Domain.Abstractions.ConsoleSync;
public interface IConsoleUnitOfWork
{
    IConsoleEntityRepository<SimpleAccount> SimpleAccountsRepository { get; }
    IConsoleEntityRepository<SimpleTransaction> SimpleTransactionsRepository { get; }
    IConsoleEntityRepository<TransactionCategory> TransactionCategoriesRepository { get; }
    IConsoleEntityRepository<User> UsersRepository { get; }
}
