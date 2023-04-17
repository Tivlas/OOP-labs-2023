using Domain.Abstractions.ConsoleSync;
using Domain.Entities.Transactions;
using Persistence.Data;

namespace Persistence.Repository;
public class ConsoleTransactionsRepository<T> : ConsoleEntityRepository<T>, IConsoleTransactionRepository<T> where T : TransactionBase
{
    public ConsoleTransactionsRepository(IDbEmulatorContext context) : base(context)
    {

    }

    public IEnumerable<IGrouping<TKey, T>> GetGroupedTransactions<TKey>(Func<T, TKey> keySelector)
    {
        return _entities.GroupBy(keySelector);
    }
}
