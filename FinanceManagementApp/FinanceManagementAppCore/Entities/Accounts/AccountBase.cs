using Domain.Entities.Interfaces;
using SQLite;

namespace Domain.Entities.Accounts
{
    public abstract class AccountBase : IBankEntity
    {
        public AccountBase()
        {

        }
        public AccountBase(decimal balance, string currencyName, string name, int userId)
        {
            Balance = balance;
            CurrencyName = currencyName;
            Name = name;
            UserId = userId;
        }

        public decimal Balance { get; set; }

        public string CurrencyName { get; set; }

        public string Name { get; set; }

        public int UserId { get; set; }

        public User? User { get; set; }

        [PrimaryKey, Indexed, AutoIncrement]
        public int Id { get; set; }

        public virtual IEnumerable<(string PropName, object propValue)> GetInfo()
        {
            List<(string PropName, object propValue)> info = new();
            info.Add(("Name", Name));
            info.Add(("Currency name", CurrencyName));
            info.Add(("Balance", Balance));
            return info;
        }
    }
}