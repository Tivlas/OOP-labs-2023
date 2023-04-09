namespace FinanceManagementAppCore.Transactions
{
    public class SimpleTransaction : TransactionBase
    {
        public SimpleTransaction(DateTime transactionDate, bool isIncome, decimal amountOfMoney, int accountId, TransactionCategory category, string comment, int userId) : base(transactionDate, amountOfMoney, accountId, userId)
        {
            Category = category;
            Comment = comment;
            IsIncome = isIncome;
        }

        public TransactionCategory Category { get; set; }

        public string Comment { get; set; }

        public bool IsIncome { get; init; }

        public override IEnumerable<(string PropName, object propValue)> GetInfo()
        {
            var info = base.GetInfo() as List<(string PropName, object propValue)>;
            info!.Add(("Category", Category.Name));
            info!.Add(("Comment", Comment));
            info!.Add(("IsIncome", IsIncome));
            return info;
        }
    }
}