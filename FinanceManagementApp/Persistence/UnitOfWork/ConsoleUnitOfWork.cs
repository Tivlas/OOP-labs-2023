using Domain.Abstractions.ConsoleSync;
using Domain.Entities;
using Domain.Entities.Accounts;
using Domain.Entities.Transactions;
using Persistence.Data;
using Persistence.Repository;

namespace Persistence.UnitOfWork;
public class ConsoleUnitOfWork : IConsoleUnitOfWork
{
    private readonly IDbEmulatorContext _context;
    private readonly Lazy<IConsoleEntityRepository<SimpleAccount>> _simpleAccountsRepository;
    private readonly Lazy<IConsoleEntityRepository<SimpleTransaction>> _simpleTransactionsRepository;
    private readonly Lazy<IConsoleEntityRepository<TransactionCategory>> _transactionCategoriesRepository;
    private readonly Lazy<IConsoleEntityRepository<User>> _usersRepository;

    public IConsoleEntityRepository<SimpleAccount> SimpleAccountsRepository => _simpleAccountsRepository.Value;
    public IConsoleEntityRepository<SimpleTransaction> SimpleTransactionsRepository => _simpleTransactionsRepository.Value;
    public IConsoleEntityRepository<TransactionCategory> TransactionCategoriesRepository => _transactionCategoriesRepository.Value;
    public IConsoleEntityRepository<User> UsersRepository => _usersRepository.Value;

    public ConsoleUnitOfWork(IDbEmulatorContext context)
    {
        _context = context;
        _simpleAccountsRepository = new Lazy<IConsoleEntityRepository<SimpleAccount>>(
            () => new ConsoleEntityRepository<SimpleAccount>(_context));

        _simpleTransactionsRepository = new Lazy<IConsoleEntityRepository<SimpleTransaction>>(
            () => new ConsoleEntityRepository<SimpleTransaction>(_context));

        _transactionCategoriesRepository = new Lazy<IConsoleEntityRepository<TransactionCategory>>(
            () => new ConsoleEntityRepository<TransactionCategory>(_context));

        _usersRepository = new Lazy<IConsoleEntityRepository<User>>(
            () => new ConsoleEntityRepository<User>(_context));
    }

}
