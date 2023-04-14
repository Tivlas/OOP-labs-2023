using System.Linq.Expressions;
using Domain.Entities.Transactions;

namespace Domain.Abstractions.NotForConsoleAsync;
public interface ITransactionRepository<T> : IEntityRepository<T> where T : TransactionBase
{
    Task<IGrouping<DateTime, T>> GetTransactionsGroupedByDateAsync(Expression<Func<T, bool>> filter,
    CancellationToken cancellationToken = default);

    Task<IGrouping<DateTime, T>> GetTransactionsGroupedByCategoryAsync(Expression<Func<T, bool>> filter,
    CancellationToken cancellationToken = default);

    Task<IEnumerable<T>> GetTransactionsForTimePeriodAsync(Expression<Func<T, bool>> filter,
    CancellationToken cancellationToken = default);
}
