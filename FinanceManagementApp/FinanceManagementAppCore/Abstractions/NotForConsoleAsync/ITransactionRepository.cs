using System.Linq.Expressions;
using Domain.Entities.Transactions;

namespace Domain.Abstractions.NotForConsoleAsync;
public interface ITransactionRepository<T> : IEntityRepository<T> where T : TransactionBase
{
    Task<IEnumerable<IGrouping<TKey, T>>> GetTransactionsGroupedByDateAsync<TKey>(Expression<Func<T, TKey>> keySelector,
    CancellationToken cancellationToken = default);
}
