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

        bool AccountExists(string name, int userId);

        bool UserExists(string email);

        void AddTransactionCategory(TransactionCategory category);

        void RemoveTransactionCategory(string name, int userId);

        bool TransactionCategoryExists(string name, int userId);

        void AddTransaction(TransactionBase transaction);

        void RemoveTransaction(int transactionId);

        void AddAccount(AccountBase account);

        void RemoveAccount(int userId, string name);
    }
}