using System.Linq.Expressions;
using Application.Abstractions.NotConsole;
using Domain.Abstractions.NotForConsoleAsync;
using Domain.Entities.Transactions;

namespace Application.Services.NotConsole;
public class TransactionCategoryService : ITransactionCategoryService
{
    private readonly IUnitOfWork _unitOfWork;

    public TransactionCategoryService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public Task<bool> Exists(Expression<Func<TransactionCategory, bool>> filter, CancellationToken cancellationToken = default)
    {
        return _unitOfWork.TransactionCategoriesRepository.Exists(filter, cancellationToken);
    }

    public Task<IReadOnlyList<TransactionCategory>> ListAllAsync(CancellationToken cancellationToken = default)
    {
        return _unitOfWork.TransactionCategoriesRepository.ListAllAsync(cancellationToken);
    }

    public Task<IReadOnlyList<TransactionCategory>> ListAsync(Expression<Func<TransactionCategory, bool>> filter, CancellationToken cancellationToken = default)
    {
        return _unitOfWork.TransactionCategoriesRepository.ListAsync(filter, cancellationToken);
    }

    public Task AddAsync(TransactionCategory entity, CancellationToken cancellationToken = default)
    {
        return _unitOfWork.TransactionCategoriesRepository.AddAsync(entity, cancellationToken);
    }

    public Task UpdateAsync(TransactionCategory entity, CancellationToken cancellationToken = default)
    {
        return _unitOfWork.TransactionCategoriesRepository.UpdateAsync(entity, cancellationToken);
    }

    public Task DeleteAsync(TransactionCategory entity, CancellationToken cancellationToken = default)
    {
        return _unitOfWork.TransactionCategoriesRepository.DeleteAsync(entity, cancellationToken);
    }

    public Task<TransactionCategory?> FirstOrDefaultAsync(Expression<Func<TransactionCategory, bool>> filter, CancellationToken cancellationToken = default)
    {
        return _unitOfWork.TransactionCategoriesRepository.FirstOrDefaultAsync(filter, cancellationToken);
    }

    public Task SaveChangesAsync()
    {
        return _unitOfWork.SaveAllAsync();
    }
}
