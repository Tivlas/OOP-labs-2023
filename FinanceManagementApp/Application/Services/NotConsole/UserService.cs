using System.Linq.Expressions;
using Application.Abstractions.NotConsole;
using Domain.Abstractions.NotForConsoleAsync;
using Domain.Entities;

namespace Application.Services.NotConsole;
public class UserService : IUserService
{
    private readonly IUnitOfWork _unitOfWork;

    public UserService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public Task<bool> Exists(Expression<Func<User, bool>> filter, CancellationToken cancellationToken = default)
    {
        return _unitOfWork.UsersRepository.Exists(filter, cancellationToken);
    }

    public Task<IReadOnlyList<User>> ListAllAsync(CancellationToken cancellationToken = default)
    {
        return _unitOfWork.UsersRepository.ListAllAsync(cancellationToken);
    }

    public Task<IReadOnlyList<User>> ListAsync(Expression<Func<User, bool>> filter, CancellationToken cancellationToken = default, params Expression<Func<User, object>>[] includesProperties)
    {
        return _unitOfWork.UsersRepository.ListAsync(filter, cancellationToken, includesProperties);
    }

    public Task AddAsync(User entity, CancellationToken cancellationToken = default)
    {
        return _unitOfWork.UsersRepository.AddAsync(entity, cancellationToken);
    }

    public Task UpdateAsync(User entity, CancellationToken cancellationToken = default)
    {
        return _unitOfWork.UsersRepository.UpdateAsync(entity, cancellationToken);
    }

    public Task DeleteAsync(User entity, CancellationToken cancellationToken = default)
    {
        return _unitOfWork.UsersRepository.DeleteAsync(entity, cancellationToken);
    }

    public Task<User?> FirstOrDefaultAsync(Expression<Func<User, bool>> filter, CancellationToken cancellationToken = default)
    {
        return _unitOfWork.UsersRepository.FirstOrDefaultAsync(filter, cancellationToken);
    }

    public Task SaveChangesAsync()
    {
        return _unitOfWork.SaveAllAsync();
    }
}
