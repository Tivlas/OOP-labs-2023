using Application.Abstractions.Console;
using Domain.Abstractions.ConsoleSync;
using Domain.Entities.Transactions;

namespace Application.Services.Console;
public class ConsoleTransactionCategoryService : IConsoleTransactionCategoryService
{
    IConsoleUnitOfWork _unit;

    public ConsoleTransactionCategoryService(IConsoleUnitOfWork unit)
    {
        _unit = unit;
    }

    public bool Exists(Func<TransactionCategory, bool> filter)
    {
        return _unit.TransactionCategoriesRepository.Exists(filter);
    }

    public IReadOnlyList<TransactionCategory> ListAll()
    {
        return _unit.TransactionCategoriesRepository.ListAll();
    }

    public IReadOnlyList<TransactionCategory> List(Func<TransactionCategory, bool> filter)
    {
        return _unit.TransactionCategoriesRepository.List(filter);
    }

    public void Add(TransactionCategory entity)
    {
        _unit.TransactionCategoriesRepository.Add(entity);
    }

    public void Update(TransactionCategory entity)
    {
        _unit.TransactionCategoriesRepository.Update(entity);
    }

    public void Delete(TransactionCategory entity)
    {
        _unit.TransactionCategoriesRepository.Delete(entity);
    }

    public TransactionCategory FirstOrDefault(Func<TransactionCategory, bool> filter)
    {
        return _unit.TransactionCategoriesRepository.FirstOrDefault(filter);
    }
}
