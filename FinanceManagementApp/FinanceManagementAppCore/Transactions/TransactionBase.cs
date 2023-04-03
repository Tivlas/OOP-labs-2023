using FinanceManagementAppCore.Interfaces;

namespace FinanceManagementAppCore.Transactions
{
    public abstract class TransactionBase : IEntity
    {
        private static int s_IdController = 0;
        public TransactionBase(DateTime transactionDate, decimal amountOfMoney, int accountId)
        {
            TransactionDate = transactionDate;
            AmountOfMoney = amountOfMoney;
            AccountId = accountId;
            Id = s_IdController;
            ++s_IdController;
        }

        public DateTime TransactionDate { get; set; }

        public decimal AmountOfMoney { get; set; }

        public int AccountId { get; set; }

        public int Id { get; init; }
    }
}