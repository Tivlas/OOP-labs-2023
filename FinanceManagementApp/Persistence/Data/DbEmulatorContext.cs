using System.Collections;
using Domain.Entities;
using Domain.Entities.Accounts;
using Domain.Entities.Transactions;

namespace Persistence.Data;
public class DbEmulatorContext : IDbEmulatorContext
{
    private List<SimpleAccount> _simpleAccounts { get; set; } = new();
    private List<SimpleTransaction> _simpleTransactions { get; set; } = new();
    private List<TransactionCategory> _transactionCategories { get; set; } = new();
    private List<User> _users { get; set; } = new();

    private Dictionary<Type, object> _entitiesLists;

    public DbEmulatorContext()
    {
        _entitiesLists = new Dictionary<Type, object>(){
            { typeof(SimpleAccount),  _simpleAccounts},
            { typeof(SimpleTransaction), _simpleTransactions },
            { typeof(TransactionCategory), _users },
            { typeof(User), _transactionCategories }
        };
    }

    public IEnumerable<T>? GetList<T>()
    {
        if (_entitiesLists.TryGetValue(typeof(T), out object? list))
        {
            var ret = list as IEnumerable;
            return ret!.OfType<T>();
        }
        return null;
    }
}
