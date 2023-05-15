using Domain.Entities.Accounts;
using Domain.Entities.Interfaces;
using Domain.Entities.Transactions;

namespace Domain.Entities
{
    public class User : IEntity
    {
        public User(string email, string password)
        {
            Email = email;
            Password = password;
        }

        public int Id { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        public List<TransactionCategory> TransactionCategories { get; set; } = new();
        public List<SimpleAccount> SimpleAccounts { get; set; } = new();
        public List<SimpleTransaction> SimpleTransactions { get; set; } = new();

        public IEnumerable<(string PropName, object propValue)> GetInfo()
        {
            List<(string PropName, object propValue)> info = new();
            info.Add(("Email", Email));
            return info;
        }
    }
}