using Domain.Abstractions.ConsoleSync;
using Domain.Entities.Interfaces;
using Persistence.Data;

namespace Persistence.Repository;
public class ConsoleEntityRepository<T> : IConsoleEntityRepository<T> where T : IEntity
{
    protected readonly List<T> _entities;
    protected readonly DbEmulatorContext _context;

    public ConsoleEntityRepository(DbEmulatorContext context)
    {
        _context = context;
        _entities = _context.GetList<T>()!;
    }

    public IReadOnlyList<T> ListAll()
    {
        return _entities;
    }
    public IReadOnlyList<T> List(Func<T, bool> filter)
    {
        return (IReadOnlyList<T>)_entities.Where(filter);
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
