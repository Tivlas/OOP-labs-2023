using Domain.Entities.Interfaces;
using SQLite;

namespace Domain.Entities.Transactions
{
    public abstract class TransactionBase : IEntity, IRelatedToUser
    {
        public TransactionBase()
        {

        }
        public TransactionBase(DateTime transactionDate, decimal amountOfMoney, int userId)
        {
            TransactionDate = transactionDate;
            AmountOfMoney = amountOfMoney;
            UserId = userId;
        }

        public DateTime TransactionDate { get; set; }

        public decimal AmountOfMoney { get; set; }

        [PrimaryKey, Indexed, AutoIncrement]
        public int Id { get; set; }

        public int UserId { get; set; }

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