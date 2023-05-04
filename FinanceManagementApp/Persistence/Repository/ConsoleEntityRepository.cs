using Domain.Abstractions.ConsoleSync;
using Domain.Entities.Accounts;
using Domain.Entities;
using Domain.Entities.Interfaces;
using Domain.Entities.Transactions;
using Persistence.Data;

namespace Persistence.Repository;
public class ConsoleEntityRepository<T> : IConsoleEntityRepository<T> where T : IEntity
{
    protected readonly List<T> _entities;
    protected readonly IDbEmulatorContext _context;

    public ConsoleEntityRepository(IDbEmulatorContext context)
    {
        _context = context;
        var type = typeof(T);
        if (type == typeof(SimpleAccount))
        {
            _entities = (_context.SimpleAccounts as List<T>)!;
        }
        else if (type == typeof(SimpleTransaction))
        {
            _entities = (_context.SimpleTransactions as List<T>)!;
        }
        else if (type == typeof(TransactionCategory))
        {
            _entities = (_context.TransactionCategories as List<T>)!;
        }
        else if (type == typeof(User))
        {
            _entities = (_context.Users as List<T>)!;
        }
        else
        {
            _entities = new List<T>();
        }
    }

    public IReadOnlyList<T> ListAll()
    {
        return _entities;
    }
    public IReadOnlyList<T> List(Func<T, bool> filter)
    {
        return _entities.Where(filter).ToList();
    }
    public void Add(T entity)
    {
        _entities.Add(entity);
    }
    public void Update(T entity)
    {
        var index = _entities.FindIndex(e => e.Id == entity.Id);
        if (index != -1)
        {
            _entities[index] = entity;
        }
    }
    public void Delete(T entity)
    {
        _entities.Remove(entity);
    }

    public T FirstOrDefault(Func<T, bool> filter)
    {
        return _entities.FirstOrDefault(filter)!;
    }
    public bool Exists(Func<T, bool> filter)
    {
        return _entities.Any(filter);
    }
}
