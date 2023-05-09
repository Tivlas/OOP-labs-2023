using System.Linq.Expressions;
using Application.Abstractions.NotConsole;
using Domain.Abstractions.NotForConsoleAsync;
using Domain.Entities.Accounts;

namespace Application.Services.NotConsole;
public class SimpleAccountService : ISimpleAccountService
{
    private readonly IUnitOfWork _unitOfWork;
    public SimpleAccountService(IUnitOfWork unit)
    {
        _unitOfWork = unit;
    }

    public Task<bool> Exists(Expression<Func<SimpleAccount, bool>> filter, CancellationToken cancellationToken = default)
    {
        return _unitOfWork.SimpleAccountsRepository.Exists(filter, cancellationToken);
    }

    public Task<IReadOnlyList<SimpleAccount>> ListAllAsync(CancellationToken cancellationToken = default)
    {
        return _unitOfWork.SimpleAccountsRepository.ListAllAsync(cancellationToken);
    }

    public Task<IReadOnlyList<SimpleAccount>> ListAsync(Expression<Func<SimpleAccount, bool>> filter, CancellationToken cancellationToken = default, params Expression<Func<SimpleAccount, object>>[] includesProperties)
    {
        return _unitOfWork.SimpleAccountsRepository.ListAsync(filter, cancellationToken, includesProperties);
    }

    public Task AddAsync(SimpleAccount entity, CancellationToken cancellationToken = default)
    {
        return _unitOfWork.SimpleAccountsRepository.AddAsync(entity, cancellationToken);
    }

    public Task UpdateAsync(SimpleAccount entity, CancellationToken cancellationToken = default)
    {
        return _unitOfWork.SimpleAccountsRepository.UpdateAsync(entity, cancellationToken);
    }

    public Task DeleteAsync(SimpleAccount entity, CancellationToken cancellationToken = default)
    {
        return _unitOfWork.SimpleAccountsRepository.DeleteAsync(entity, cancellationToken);
    }

    public Task<SimpleAccount?> FirstOrDefaultAsync(Expression<Func<SimpleAccount, bool>> filter, CancellationToken cancellationToken = default)
    {
        return _unitOfWork.SimpleAccountsRepository.FirstOrDefaultAsync(filter, cancellationToken);
    }

    public Task SaveChangesAsync()
    {
        return _unitOfWork.SaveAllAsync();
    }
}
