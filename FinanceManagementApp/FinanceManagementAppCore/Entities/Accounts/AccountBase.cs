using Domain.Entities.Interfaces;

namespace Domain.Entities.Accounts
{
    public abstract class AccountBase : IBankEntity
    {
        private static int s_IdController = 0;

        public AccountBase()
        {
            
        }
        public AccountBase(decimal balance, string currencyName, string name, int userId)
        {
            Balance = balance;
            CurrencyName = currencyName;
            Name = name;
            UserId = userId;
            Id = s_IdController;
            ++s_IdController;
        }

        public decimal Balance { get; set; }

        public string CurrencyName { get; set; }

        public string Name { get; set; }

        public int UserId { get; init; }

        public int Id { get; init; }

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