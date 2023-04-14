using Domain.Entities.Transactions;

namespace Application.Abstractions.Console;
public interface IConsoleTransactionService<T> : IBaseConsoleService<T> where T : TransactionBase
{
    IGrouping<DateTime, T> GetTransactionsGroupedByDate(Func<T, bool> filter);

    IGrouping<DateTime, T> GetTransactionsGroupedByCategory(Func<T, bool> filter);

    IEnumerable<T> GetTransactionsForTimePeriod(Func<T, bool> filter);
}
