using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Abstractions.ConsoleSync;
using Domain.Entities.Transactions;
using Persistence.Data;

namespace Persistence.Repository;
public class ConsoleTransactionsRepository<T> : ConsoleEntityRepository<T>, IConsoleTransactionRepository<T> where T : TransactionBase
{
    public ConsoleTransactionsRepository(DbEmulatorContext context) : base(context)
    {

    }

    public IGrouping<DateTime, T> GetTransactionsGroupedByDate(Func<T, bool> filter) => throw new NotImplementedException();
    public IGrouping<DateTime, T> GetTransactionsGroupedByCategory(Func<T, bool> filter) => throw new NotImplementedException();
    public IEnumerable<T> GetTransactionsForTimePeriod(Func<T, bool> filter) => throw new NotImplementedException();
}
