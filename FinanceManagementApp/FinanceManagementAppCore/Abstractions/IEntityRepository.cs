using System.Linq.Expressions;
using Domain.Entities.Interfaces;

namespace Domain.Abstractions;
public interface IEntityRepository<T> where T : IEntity
{
    Task<T> GetByNameAsync(string name, CancellationToken cancellationToken = default,
        params Expression<Func<T, object>>[]? includesProperties);

    Task<bool> Exists(Expression<Func<T, bool>> filter,
    CancellationToken cancellationToken = default);

    Task<IReadOnlyList<T>> ListAllAsync(CancellationToken cancellationToken = default);

    Task<IReadOnlyList<T>> ListAsync(Expression<Func<T, bool>> filter,
    CancellationToken cancellationToken = default,
    params Expression<Func<T, object>>[]? includesProperties);

    Task AddAsync(T entity, CancellationToken cancellationToken = default);

    Task UpdateAsync(T entity, CancellationToken cancellationToken = default);

    Task DeleteAsync(T entity, CancellationToken cancellationToken = default);

    Task<T> FirstOrDefaultAsync(Expression<Func<T, bool>> filter, CancellationToken
   cancellationToken = default);
}
