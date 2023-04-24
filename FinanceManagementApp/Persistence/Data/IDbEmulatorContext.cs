using Domain.Entities.Accounts;
using Domain.Entities.Transactions;
using Domain.Entities;

namespace Persistence.Data;

public interface IDbEmulatorContext
{
    void Save();
    List<SimpleAccount> SimpleAccounts { get; set; }
    List<SimpleTransaction> SimpleTransactions { get; set; }
    List<TransactionCategory> TransactionCategories { get; set; }
    List<User> Users { get; set; }
}