using Application.Abstractions.Console;
using Domain.Abstractions.ConsoleSync;
using Domain.Entities.Transactions;

namespace Application.Services.Console;
public class ConsoleTransferService : IConsoleTransferService
{
    private readonly IConsoleUnitOfWork _unit;

    public ConsoleTransferService(IConsoleUnitOfWork unit)
    {
        _unit = unit;
    }

    public IGrouping<DateTime, Transfer> GetTransactionsGroupedByDate(Func<Transfer, bool> filter) => throw new NotImplementedException();

    public IGrouping<DateTime, Transfer> GetTransactionsGroupedByCategory(Func<Transfer, bool> filter) => throw new NotImplementedException();

    public IEnumerable<Transfer> GetTransactionsForTimePeriod(Func<Transfer, bool> filter) => throw new NotImplementedException();

    public bool Exists(Func<Transfer, bool> filter)
    {
        return _unit.TransfersRepository.Exists(filter);
    }

    public IReadOnlyList<Transfer> ListAll()
    {
        return _unit.TransfersRepository.ListAll();
    }

    public IReadOnlyList<Transfer> List(Func<Transfer, bool> filter)
    {
        return _unit.TransfersRepository.List(filter);
    }

    public void Add(Transfer entity)
    {
        _unit.TransfersRepository.Add(entity);
    }

    public void Update(Transfer entity)
    {
        _unit.TransfersRepository.Update(entity);
    }

    public void Delete(Transfer entity)
    {
        _unit.TransfersRepository.Delete(entity);
    }

    public Transfer FirstOrDefault(Func<Transfer, bool> filter)
    {
        return _unit.TransfersRepository.FirstOrDefault(filter);
    }

}
