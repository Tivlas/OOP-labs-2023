using FinanceManagementAppCore.Interfaces;

namespace FinanceManagementAppCore.Transactions
{
    public class TransactionCategory : IEntity
    {
        private static int s_IdController = 0;

        public TransactionCategory(string name, int userId)
        {
            Name = name;
            UserId = userId;
            Id = s_IdController;
            ++s_IdController;
        }

        public string Name { get; set; }

        public int Id { get; init; }

        public int UserId { get; set; }
    }
}