namespace FinanceManagementAppCore.Transactions
{
    public class Transfer : TransactionBase
    {
        public Transfer(DateTime transactionDate, decimal amountOfMoney, int accountId, int secondAccountId, string comment, int userId) : base(transactionDate, amountOfMoney, accountId, userId)
        {
            SecondAccountId = secondAccountId;
            Comment = comment;
        }

        public int SecondAccountId { get; set; }

        public string Comment { get; set; }
    }
}