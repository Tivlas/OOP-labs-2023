using FinanceManagementAppCore.Interfaces;

namespace FinanceManagementAppCore.Accounts
{
    public abstract class AccountBase : IBankEntity
    {
        private static int s_IdController = 0;

        public AccountBase(decimal balance, string currencyName, string name, int userId, int cardId)
        {
            Balance = balance;
            CurrencyName = currencyName;
            Name = name;
            UserId = userId;
            CardId = cardId;
            Id = s_IdController;
            ++s_IdController;
        }

        public decimal Balance { get; set; }

        public string CurrencyName { get; set; }

        public string Name { get; set; }

        public int UserId { get; init; }

        public int Id { get; init; }

        public int CardId { get; init; }
    }
}