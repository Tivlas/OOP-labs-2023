using Application.Abstractions.Console;
using Domain.Entities.Transactions;

namespace Application.Services.Console;
public class ConsoleTransactionCategoryService : IConsoleTransactionCategoryService
{
    public bool Exists(Func<TransactionCategory, bool> filter) => throw new NotImplementedException();
    public IReadOnlyList<TransactionCategory> ListAll() => throw new NotImplementedException();
    public IReadOnlyList<TransactionCategory> List(Func<TransactionCategory, bool> filter) => throw new NotImplementedException();
    public void Add(TransactionCategory entity) => throw new NotImplementedException();
    public void Update(TransactionCategory entity) => throw new NotImplementedException();
    public void Delete(TransactionCategory entity) => throw new NotImplementedException();
    public TransactionCategory FirstOrDefault(Func<TransactionCategory, bool> filter) => throw new NotImplementedException();
}
