using Domain.Entities.Transactions;

namespace Domain.Entities.Interfaces
{
    public interface IDataManager
    {
        IGrouping<DateTime, TransactionBase> GetTransactionsGroupedByDate(int userId);

        IGrouping<DateTime, TransactionBase> GetTransactionsGroupedByCategory(int userId);

        IEnumerable<TransactionBase> GetTransactionsForTimePeriod(int userId);

        void AddUser(User user);

        bool EmailPasswordMatch(string email, string password);

        int GetUserId(Func<User, bool> match);

        bool BankEntityExists(Func<IBankEntity, bool> match);

        bool UserExists(Func<User, bool> match);

        void AddTransactionCategory(TransactionCategory category);

        void RemoveTransactionCategory(Func<TransactionCategory, bool> filter);

        bool TransactionCategoryExists(Func<TransactionCategory, bool> filter);

        void AddTransaction(TransactionBase transaction);

        void RemoveTransaction(Func<TransactionBase, bool> filter);

        void AddBankEntity(IBankEntity account);

        IBankEntity? FirstOrDefaultBankEntity(Func<IBankEntity, bool> filter);

        IEnumerable<IBankEntity>? GetBankEntities(Func<IBankEntity, bool> filter);

        IEnumerable<TransactionBase>? GetTransactions(Func<TransactionBase, bool> filter);

        IEnumerable<TransactionCategory>? GetCategories(Func<TransactionCategory, bool> filter);
    }
}