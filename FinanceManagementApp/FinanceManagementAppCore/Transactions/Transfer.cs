namespace FinanceManagementAppCore.Transactions
{
    public class Transfer : TransactionBase
    {
        public Transfer(DateTime transactionDate, decimal amountOfMoney, int accountId, int secondAccountId, string comment) : base(transactionDate, amountOfMoney, accountId)
        {
            SecondAccountId = secondAccountId;
            Comment = comment;
        }

        public int SecondAccountId { get; set; }

        public string Comment { get; set; }
    }
}