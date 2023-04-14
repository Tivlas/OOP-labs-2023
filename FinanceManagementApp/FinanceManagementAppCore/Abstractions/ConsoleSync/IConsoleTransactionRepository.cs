using Domain.Entities.Transactions;

namespace Domain.Abstractions.ConsoleSync;
public interface IConsoleTransactionRepository<T> : IConsoleEntityRepository<T> where T : TransactionBase
{
    IGrouping<DateTime, T> GetTransactionsGroupedByDate(Func<T, bool> filter);

    IGrouping<DateTime, T> GetTransactionsGroupedByCategory(Func<T, bool> filter);

    IEnumerable<T> GetTransactionsForTimePeriod(Func<T, bool> filter);
}
