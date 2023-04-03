using FinanceManagementAppCore.Accounts;
using FinanceManagementAppCore.Transactions;
using System.Globalization;

namespace FinanceManagementAppCore.Interfaces
{
    public interface IDataManager
    {
        IGrouping<DateTime, TransactionBase> GetTransactionsGroupedByDate(int userId);

        IGrouping<DateTime, TransactionBase> GetTransactionsGroupedByCategory(int userId);

        IEnumerable<TransactionBase> GetTransactionsForTimePeriod(int userId);

        void AddUser(User user);

        bool EmailPasswordMatch(string email, string password);

        int GetUserId(string email);

        bool UserExists(string email);

        void AddTransactionCategory(int userId, TransactionCategory category);

        void RemoveTransactionCategory(int userId, int categoryId);

        void AddTransaction(int userId, TransactionBase transaction);

        void RemoveTransaction(int userId, int transactionId);

        bool AddAccountIfNotExist(int userId, AccountBase account);

        void RemoveAccount(int userId, int accountId);
    }
}