using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Abstractions.NotForConsoleAsync;
using Domain.Entities;
using Domain.Entities.Accounts;
using Domain.Entities.Transactions;
using Persistence.Data;
using Persistence.Repository;

namespace Persistence.UnitOfWork;
public class EfUnitOfWork : IUnitOfWork
{
    private readonly AppDbContext _context;
    private readonly Lazy<IEntityRepository<SimpleAccount>> _simpleAccountsRepository;
    private readonly Lazy<IEntityRepository<SimpleTransaction>> _simpleTransactionsRepository;
    private readonly Lazy<IEntityRepository<TransactionCategory>> _transactionCategoriesRepository;
    private readonly Lazy<IEntityRepository<User>> _usersRepository;

    public EfUnitOfWork(AppDbContext appDbContext)
    {
        _context = appDbContext;
        _simpleAccountsRepository = new Lazy<IEntityRepository<SimpleAccount>>(() =>
        new EfRepository<SimpleAccount>(_context));

        _simpleTransactionsRepository = new Lazy<IEntityRepository<SimpleTransaction>>(() =>
        new EfRepository<SimpleTransaction>(_context));

        _transactionCategoriesRepository = new Lazy<IEntityRepository<TransactionCategory>>(() =>
        new EfRepository<TransactionCategory>(_context));

        _usersRepository = new Lazy<IEntityRepository<User>>(() =>
        new EfRepository<User>(_context));
    }

    public IEntityRepository<SimpleAccount> SimpleAccountsRepository => _simpleAccountsRepository.Value;
    public IEntityRepository<SimpleTransaction> SimpleTransactionsRepository => _simpleTransactionsRepository.Value;
    public IEntityRepository<TransactionCategory> TransactionCategoriesRepository => _transactionCategoriesRepository.Value;
    public IEntityRepository<User> UsersRepository => _usersRepository.Value;

    public async Task RemoveDatbaseAsync()
    {
        await _context.Database.EnsureDeletedAsync();
    }

    public async Task CreateDatabaseAsync()
    {
        await _context.Database.EnsureCreatedAsync();
    }

    public async Task SaveAllAsync()
    {
        await _context.SaveChangesAsync();
    }
}
