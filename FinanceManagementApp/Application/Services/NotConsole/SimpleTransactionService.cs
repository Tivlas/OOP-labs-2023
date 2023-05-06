using System.Linq.Expressions;
using Application.Abstractions.NotConsole;
using Domain.Abstractions.NotForConsoleAsync;
using Domain.Entities.Transactions;

namespace Application.Services.NotConsole;
public class SimpleTransactionService : ISimpleTransactionService
{
    private readonly IUnitOfWork _unitOfWork;

    public SimpleTransactionService(IUnitOfWork unit)
    {
        _unitOfWork = unit;
    }

    public Task<bool> Exists(Expression<Func<SimpleTransaction, bool>> filter, CancellationToken cancellationToken = default)
    {
        return _unitOfWork.SimpleTransactionsRepository.Exists(filter, cancellationToken);
    }

    public Task<IReadOnlyList<SimpleTransaction>> ListAllAsync(CancellationToken cancellationToken = default)
    {
        return _unitOfWork.SimpleTransactionsRepository.ListAllAsync(cancellationToken);
    }

    public Task<IReadOnlyList<SimpleTransaction>> ListAsync(Expression<Func<SimpleTransaction, bool>> filter, CancellationToken cancellationToken = default)
    {
        return _unitOfWork.SimpleTransactionsRepository.ListAsync(filter, cancellationToken);
    }

    public Task AddAsync(SimpleTransaction entity, CancellationToken cancellationToken = default)
    {
        return _unitOfWork.SimpleTransactionsRepository.AddAsync(entity, cancellationToken);
    }

    public Task UpdateAsync(SimpleTransaction entity, CancellationToken cancellationToken = default)
    {
        return _unitOfWork.SimpleTransactionsRepository.UpdateAsync(entity, cancellationToken);
    }

    public Task DeleteAsync(SimpleTransaction entity, CancellationToken cancellationToken = default)
    {
        return _unitOfWork.SimpleTransactionsRepository.DeleteAsync(entity, cancellationToken);
    }

    public Task<SimpleTransaction?> FirstOrDefaultAsync(Expression<Func<SimpleTransaction, bool>> filter, CancellationToken cancellationToken = default)
    {
        return _unitOfWork.SimpleTransactionsRepository.FirstOrDefaultAsync(filter, cancellationToken);
    }

    public Task SaveChangesAsync()
    {
        return _unitOfWork.SaveAllAsync();
    }
}
