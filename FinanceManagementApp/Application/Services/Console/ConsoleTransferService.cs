using Application.Abstractions.Console;
using Domain.Entities.Transactions;

namespace Application.Services.Console;
public class ConsoleTransferService : IConsoleTransferService
{
    public IGrouping<DateTime, Transfer> GetTransactionsGroupedByDate(Func<Transfer, bool> filter) => throw new NotImplementedException();
    public IGrouping<DateTime, Transfer> GetTransactionsGroupedByCategory(Func<Transfer, bool> filter) => throw new NotImplementedException();
    public IEnumerable<Transfer> GetTransactionsForTimePeriod(Func<Transfer, bool> filter) => throw new NotImplementedException();
    public bool Exists(Func<Transfer, bool> filter) => throw new NotImplementedException();
    public IReadOnlyList<Transfer> ListAll() => throw new NotImplementedException();
    public IReadOnlyList<Transfer> List(Func<Transfer, bool> filter) => throw new NotImplementedException();
    public void Add(Transfer entity) => throw new NotImplementedException();
    public void Update(Transfer entity) => throw new NotImplementedException();
    public void Delete(Transfer entity) => throw new NotImplementedException();
    public Transfer FirstOrDefault(Func<Transfer, bool> filter) => throw new NotImplementedException();
}
