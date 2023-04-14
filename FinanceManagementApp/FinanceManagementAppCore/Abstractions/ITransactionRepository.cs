using System.Linq.Expressions;
using Domain.Entities.Transactions;

namespace Domain.Abstractions;
public interface ITransactionRepository<T> : IEntityRepository<T> where T : TransactionBase
{
    Task<IGrouping<DateTime, T>> GetTransactionsGroupedByDate(Expression<Func<T, bool>> filter,
    CancellationToken cancellationToken = default);

    Task<IGrouping<DateTime, T>> GetTransactionsGroupedByCategory(Expression<Func<T, bool>> filter,
    CancellationToken cancellationToken = default);

    Task<IEnumerable<T>> GetTransactionsForTimePeriod(Expression<Func<T, bool>> filter,
    CancellationToken cancellationToken = default);
}
