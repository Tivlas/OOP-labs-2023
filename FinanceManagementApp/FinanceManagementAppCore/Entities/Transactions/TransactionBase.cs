using System.ComponentModel.DataAnnotations.Schema;
using Domain.Entities.Accounts;
using Domain.Entities.Interfaces;

namespace Domain.Entities.Transactions
{
    public abstract class TransactionBase : IEntity, IRelatedToUser
    {
        private static int s_IdController = 0;

        public TransactionBase()
        {

        }
        public TransactionBase(DateTime transactionDate, decimal amountOfMoney, int userId)
        {
            TransactionDate = transactionDate;
            AmountOfMoney = amountOfMoney;
            UserId = userId;
            Id = s_IdController;
            ++s_IdController;
        }

        public DateTime TransactionDate { get; set; }

        public decimal AmountOfMoney { get; set; }

        public int Id { get; init; }

        public int UserId { get; init; }

        public User? User { get; set; }

        public virtual IEnumerable<(string PropName, object propValue)> GetInfo()
        {
            List<(string PropName, object propValue)> info = new();
            info.Add(("TransactionDate", TransactionDate.Date.ToShortDateString()));
            info.Add(("AmountOfMoney", AmountOfMoney));
            return info;
        }
    }
}