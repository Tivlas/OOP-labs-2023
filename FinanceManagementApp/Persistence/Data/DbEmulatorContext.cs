using System.Collections;
using System.Text.Json;
using Domain.Entities;
using Domain.Entities.Accounts;
using Domain.Entities.Transactions;
using Serialization;

namespace Persistence.Data;
public class DbEmulatorContext : IDbEmulatorContext
{
    public List<SimpleAccount> SimpleAccounts { get; set; }
    public List<SimpleTransaction> SimpleTransactions { get; set; }
    public List<TransactionCategory> TransactionCategories { get; set; }
    public List<User> Users { get; set; }


    private readonly ISerializer _serializer;
    private readonly string _simpleAccountsPath = Path.Combine(Directory.GetCurrentDirectory(), typeof(SimpleAccount).Name + ".json");
    private readonly string _simpleTransactionsPath = Path.Combine(Directory.GetCurrentDirectory(), typeof(SimpleTransaction).Name + ".json");
    private readonly string _transactionCategoriesPath = Path.Combine(Directory.GetCurrentDirectory(), typeof(TransactionCategory).Name + ".json");
    private readonly string _usersPath = Path.Combine(Directory.GetCurrentDirectory(), typeof(User).Name + ".json");


    public DbEmulatorContext(ISerializer serializer)
    {
        _serializer = serializer;

        SimpleAccounts = _serializer.DeserializeJson<SimpleAccount>(_simpleAccountsPath)?.ToList() ?? new();
        SimpleTransactions = _serializer.DeserializeJson<SimpleTransaction>(_simpleTransactionsPath)?.ToList() ?? new();
        TransactionCategories = _serializer.DeserializeJson<TransactionCategory>(_transactionCategoriesPath)?.ToList() ?? new();
        Users = _serializer.DeserializeJson<User>(_usersPath)?.ToList() ?? new();
    }

    public void Save()
    {
        var options = new JsonSerializerOptions
        {
            WriteIndented = true
        };
        _serializer.SerializeJson(SimpleAccounts, _simpleAccountsPath, options);
        _serializer.SerializeJson(SimpleTransactions, _simpleTransactionsPath, options);
        _serializer.SerializeJson(TransactionCategories, _transactionCategoriesPath, options);
        _serializer.SerializeJson(Users, _usersPath, options);
    }
}
