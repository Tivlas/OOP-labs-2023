using Domain.Entities.Transactions;

namespace Application.Abstractions.Console;
public interface IConsoleTransferService : IBaseConsoleService<Transfer>
{
    IGrouping<DateTime, Transfer> GetTransactionsGroupedByDate(Func<Transfer, bool> filter);

    IGrouping<DateTime, Transfer> GetTransactionsGroupedByCategory(Func<Transfer, bool> filter);

    IEnumerable<Transfer> GetTransactionsForTimePeriod(Func<Transfer, bool> filter);
}
