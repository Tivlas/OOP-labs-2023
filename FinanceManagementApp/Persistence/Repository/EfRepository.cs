using System.Linq.Expressions;
using Domain.Abstractions.NotForConsoleAsync;
using Domain.Entities.Interfaces;
using Microsoft.EntityFrameworkCore;
using Persistence.Data;

namespace Persistence.Repository;
public class EfRepository<T> : IEntityRepository<T> where T : class, IEntity
{
    protected readonly AppDbContext _context;
    protected readonly DbSet<T> _entities;

    public EfRepository(AppDbContext context)
    {
        _context = context;
        _entities = context.Set<T>();
    }

    public async Task<bool> Exists(Expression<Func<T, bool>> filter, CancellationToken cancellationToken = default)
    {
        return await _entities.AnyAsync(filter, cancellationToken);
    }

    public async Task<IReadOnlyList<T>> ListAllAsync(CancellationToken cancellationToken = default)
    {
        return await _entities.ToListAsync(cancellationToken);
    }

    public async Task<IReadOnlyList<T>> ListAsync(Expression<Func<T, bool>> filter, CancellationToken cancellationToken = default)
    {
        return await _entities.Where(filter).ToListAsync(cancellationToken);
    }

    public async Task AddAsync(T entity, CancellationToken cancellationToken = default)
    {
        await _entities.AddAsync(entity, cancellationToken);
    }

    public Task UpdateAsync(T entity, CancellationToken cancellationToken = default)
    {
        return Task.FromResult(_entities.Update(entity));
    }

    public Task DeleteAsync(T entity, CancellationToken cancellationToken = default)
    {
        return Task.FromResult(_entities.Remove(entity));
    }

    public async Task<T?> FirstOrDefaultAsync(Expression<Func<T, bool>> filter, CancellationToken cancellationToken = default)
    {
        return await _entities.FirstOrDefaultAsync(filter, cancellationToken);
    }
}
