using Application.Abstractions.Console;
using Domain.Abstractions.ConsoleSync;
using Domain.Entities.Transactions;

namespace Application.Services.Console;
public class ConsoleSimpleTransactionService : IConsoleSimpleTransactionService
{
    IConsoleUnitOfWork _unit;

    public ConsoleSimpleTransactionService(IConsoleUnitOfWork unit)
    {
        _unit = unit;
    }

  
    public bool Exists(Func<SimpleTransaction, bool> filter)
    {
        return _unit.SimpleTransactionsRepository.Exists(filter);
    }

    public IReadOnlyList<SimpleTransaction> ListAll()
    {
        return _unit.SimpleTransactionsRepository.ListAll();
    }

    public IReadOnlyList<SimpleTransaction> List(Func<SimpleTransaction, bool> filter)
    {
        return _unit.SimpleTransactionsRepository.List(filter);
    }

    public void Add(SimpleTransaction entity)
    {
        _unit.SimpleTransactionsRepository.Add(entity);
    }

    public void Update(SimpleTransaction entity)
    {
        _unit.SimpleTransactionsRepository.Update(entity);
    }

    public void Delete(SimpleTransaction entity)
    {
        _unit.SimpleTransactionsRepository.Delete(entity);
    }

    public SimpleTransaction FirstOrDefault(Func<SimpleTransaction, bool> filter)
    {
        return _unit.SimpleTransactionsRepository.FirstOrDefault(filter);
    }
}
