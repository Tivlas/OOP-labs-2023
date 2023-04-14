﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Abstractions.ConsoleSync;
using Domain.Cards;
using Domain.Entities.Accounts;
using Domain.Entities.Transactions;
using Persistence.Data;
using Persistence.Repository;

namespace Persistence.UnitOfWork;
public class ConsoleUnitOfWork : IConsoleUnitOfWork
{
    private readonly IDbEmulatorContext _context;
    private readonly Lazy<IConsoleEntityRepository<SimpleAccount>> _simpleAccountsRepository;
    private readonly Lazy<IConsoleEntityRepository<Card>> _cardsRepository;
    private readonly Lazy<IConsoleEntityRepository<SimpleTransaction>> _simpleTransactionsRepository;
    private readonly Lazy<IConsoleEntityRepository<Transfer>> _transfersRepository;
    private readonly Lazy<IConsoleEntityRepository<TransactionCategory>> _transactionCategoriesRepository;

    public IConsoleEntityRepository<SimpleAccount> SimpleAccountsRepository => _simpleAccountsRepository.Value;
    public IConsoleEntityRepository<Card> CardsRepository => _cardsRepository.Value;
    public IConsoleEntityRepository<SimpleTransaction> SimpleTransactionsRepository => _simpleTransactionsRepository.Value;
    public IConsoleEntityRepository<Transfer> TransfersRepository => _transfersRepository.Value;
    public IConsoleEntityRepository<TransactionCategory> TransactionCategoriesRepository => _transactionCategoriesRepository.Value;

    public ConsoleUnitOfWork(IDbEmulatorContext context)
    {
        _context = context;
        _simpleAccountsRepository = new Lazy<IConsoleEntityRepository<SimpleAccount>>(
            () => new ConsoleEntityRepository<SimpleAccount>(_context));

        _cardsRepository = new Lazy<IConsoleEntityRepository<Card>>(
            () => new ConsoleEntityRepository<Card>(_context));

        _simpleTransactionsRepository = new Lazy<IConsoleEntityRepository<SimpleTransaction>>(
            () => new ConsoleTransactionsRepository<SimpleTransaction>(_context));

        _transfersRepository = new Lazy<IConsoleEntityRepository<Transfer>>(
            () => new ConsoleTransactionsRepository<Transfer>(_context));

        _transactionCategoriesRepository = new Lazy<IConsoleEntityRepository<TransactionCategory>>(
            () => new ConsoleEntityRepository<TransactionCategory>(_context));
    }


}
