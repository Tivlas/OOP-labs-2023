using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Abstractions;
using Domain.Cards;
using Domain.Entities.Accounts;
using Domain.Entities.Transactions;

namespace Persistence.Data;
public class DbEmulatorContext
{
    List<SimpleAccount> SimpleAccounts { get; set; } = new();
    List<Card> Cards { get; set; } = new();
    List<SimpleTransaction> SimpleTransactions { get; set; } = new();
    List<Transfer> Transfers { get; set; } = new();
    List<TransactionCategory> TransactionCategories { get; set; } = new();

    public DbEmulatorContext()
    {

    }
}
