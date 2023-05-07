using Domain.Entities.Accounts;
using Domain.Entities.Interfaces;
using Domain.Entities.Transactions;

namespace Domain.Entities
{
    public class User : IEntity
    {
        private static int s_IdController = 0;

        public User(string email, string password)
        {
            Email = email;
            Password = password;
            Id = s_IdController;
            ++s_IdController;
        }

        public int Id { get; init; }

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