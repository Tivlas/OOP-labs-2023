﻿namespace FinanceManagementAppCore.Transactions
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
    }
}