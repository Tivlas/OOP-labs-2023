using Domain.Entities.Accounts;

namespace Domain.Entities.Transactions
{
    public class SimpleTransaction : TransactionBase
    {
        public SimpleTransaction()
        {

        }
        public SimpleTransaction(int simpleAccountId, DateTime transactionDate, bool isIncome, decimal amountOfMoney, TransactionCategory category, string comment, int userId) : base(transactionDate, amountOfMoney,  userId)
        {
            Category = category;
            Comment = comment;
            IsIncome = isIncome;
            SimpleAccountId = simpleAccountId;
        }

        public int SimpleAccountId { get; set; }

        public SimpleAccount? SimpleAccount { get; set; }

        public TransactionCategory Category { get; set; }

        public string Comment { get; set; }

        public bool IsIncome { get; init; }

        public override IEnumerable<(string PropName, object propValue)> GetInfo()
        {
            var info = base.GetInfo() as List<(string PropName, object propValue)>;
            info!.Add(("Category", Category.Name));
            info!.Add(("Comment", Comment));
            info!.Add(("IsIncome", IsIncome));
            info!.Add(("Id", Id));
            return info;
        }
    }
}