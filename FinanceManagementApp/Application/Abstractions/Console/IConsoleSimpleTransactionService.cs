using Domain.Entities.Transactions;

namespace Application.Abstractions.Console;
public interface IConsoleSimpleTransactionService : IBaseConsoleService<SimpleTransaction>
{
    IGrouping<DateTime, SimpleTransaction> GetTransactionsGroupedByDate(Func<SimpleTransaction, bool> filter);

    IGrouping<DateTime, SimpleTransaction> GetTransactionsGroupedByCategory(Func<SimpleTransaction, bool> filter);

    IEnumerable<SimpleTransaction> GetTransactionsForTimePeriod(Func<SimpleTransaction, bool> filter);
}
