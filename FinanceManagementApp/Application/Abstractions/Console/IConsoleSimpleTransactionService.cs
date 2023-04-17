using Domain.Entities.Transactions;

namespace Application.Abstractions.Console;
public interface IConsoleSimpleTransactionService : IBaseConsoleService<SimpleTransaction>
{
    IEnumerable<IGrouping<TKey, SimpleTransaction>> GetGroupedTransactions<TKey>(Func<SimpleTransaction, TKey> keySelector);
}
