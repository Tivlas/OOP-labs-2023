using Domain.Entities.Transactions;

namespace Domain.Abstractions.ConsoleSync;
public interface IConsoleTransactionRepository<T> : IConsoleEntityRepository<T> where T : TransactionBase
{
    IEnumerable<IGrouping<TKey, T>> GetGroupedTransactions<TKey>(Func<T, TKey> keySelector);
}
