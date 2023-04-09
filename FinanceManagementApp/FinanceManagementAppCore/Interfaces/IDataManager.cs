using FinanceManagementAppCore.Accounts;
using FinanceManagementAppCore.Transactions;

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

        bool BankEntityExists(string name, int userId);

        bool UserExists(string email);

        void AddTransactionCategory(TransactionCategory category);

        void RemoveTransactionCategory(string name, int userId);

        bool TransactionCategoryExists(string name, int userId);

        void AddTransaction(TransactionBase transaction);

        void RemoveTransaction(int transactionId);

        void AddBankEntity(IBankEntity account);

        void RemoveBankEntity(int userId, string name);
    }
}