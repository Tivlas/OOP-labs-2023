using Application.Abstractions.Console;
using Domain.Entities.Transactions;

namespace Application.Services.Console;
public class ConsoleSimpleTransactionService : IConsoleSimpleTransactionService
{
    public IGrouping<DateTime, SimpleTransaction> GetTransactionsGroupedByDate(Func<SimpleTransaction, bool> filter) => throw new NotImplementedException();
    public IGrouping<DateTime, SimpleTransaction> GetTransactionsGroupedByCategory(Func<SimpleTransaction, bool> filter) => throw new NotImplementedException();
    public IEnumerable<SimpleTransaction> GetTransactionsForTimePeriod(Func<SimpleTransaction, bool> filter) => throw new NotImplementedException();
    public bool Exists(Func<SimpleTransaction, bool> filter) => throw new NotImplementedException();
    public IReadOnlyList<SimpleTransaction> ListAll() => throw new NotImplementedException();
    public IReadOnlyList<SimpleTransaction> List(Func<SimpleTransaction, bool> filter) => throw new NotImplementedException();
    public void Add(SimpleTransaction entity) => throw new NotImplementedException();
    public void Update(SimpleTransaction entity) => throw new NotImplementedException();
    public void Delete(SimpleTransaction entity) => throw new NotImplementedException();
    public SimpleTransaction FirstOrDefault(Func<SimpleTransaction, bool> filter) => throw new NotImplementedException();
}
