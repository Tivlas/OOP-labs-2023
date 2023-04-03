using FinanceManagementAppCore.Interfaces;

namespace FinanceManagementAppCore.Cards
{
    public class Card : IBankEntity
    {
        private static int s_IdController = 0;

        public Card(int accountId, string name, decimal balance, string currencyName)
        {
            Name = name;
            AccountId = accountId;
            Balance = balance;
            CurrencyName = currencyName;
            Id = s_IdController;
            ++s_IdController;
        }
        public int AccountId { get; init; }

        public int Id { get; init; }

        public string Name { get; set; }

        public decimal Balance { get; set; }

        public string CurrencyName { get; set; }
        public int UserId { get; init; }
    }
}