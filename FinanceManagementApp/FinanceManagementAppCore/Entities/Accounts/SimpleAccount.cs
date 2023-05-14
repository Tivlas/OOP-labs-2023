using Domain.Entities.Transactions;

namespace Domain.Entities.Accounts
{
    public class SimpleAccount : AccountBase
    {
        public SimpleAccount(decimal balance, string currencyName, string name, int userId) : base(balance, currencyName, name, userId)
        {
        }
        public List<SimpleTransaction> SimpleTransactions { get; set; } = new();
    }
}