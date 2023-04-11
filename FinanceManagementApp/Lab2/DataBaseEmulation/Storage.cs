using FinanceManagementAppCore;
using FinanceManagementAppCore.Interfaces;
using FinanceManagementAppCore.Transactions;
using Lab2.Services;

namespace Lab2.DataBaseEmulation
{
    public class Storage : IDataManager
    {
        #region Containers
        private List<User> Users { get; set; } = new List<User>();

        private List<IBankEntity> BankEntities { get; set; } = new List<IBankEntity>();

        private List<TransactionCategory> Categories { get; set; } = new List<TransactionCategory>();

        private List<TransactionBase> Transactions { get; set; } = new List<TransactionBase>();
        #endregion

        #region Email and password related methods
        public bool EmailPasswordMatch(string email, string password)
        {
            email = StringHasher.GetHash(email);
            password = StringHasher.GetHash(password);
            return Users is not null && Users.Any(u => u.Email == email && u.Password == password);
        }
        #endregion

        #region Statistics related methods
        public IEnumerable<TransactionBase> GetTransactionsForTimePeriod(int userId)
        {
            throw new NotImplementedException();
        }

        public IGrouping<DateTime, TransactionBase> GetTransactionsGroupedByCategory(int userId)
        {
            throw new NotImplementedException();
        }

        public IGrouping<DateTime, TransactionBase> GetTransactionsGroupedByDate(int userId)
        {
            throw new NotImplementedException();
        }
        #endregion

        #region Account related methods
        public void AddBankEntity(IBankEntity bankEntity)
        {
            BankEntities.Add(bankEntity);
        }

        public void RemoveBankEntity(Predicate<IBankEntity> filter)
        {
            BankEntities?.RemoveAll(filter);
        }

        public IBankEntity? FirstOrDefaultBankEntity(Func<IBankEntity, bool> filter)
        {
            return BankEntities?.FirstOrDefault(filter);
        }

        public IEnumerable<IBankEntity>? GetBankEntities(Func<IBankEntity, bool> filter)
        {
            return BankEntities?.Where(filter);
        }

        public bool BankEntityExists(Func<IBankEntity, bool> match)
        {
            return BankEntities.Any(match);
        }
        #endregion

        #region Transaction related methods
        public void AddTransaction(TransactionBase transaction)
        {
            Transactions?.Add(transaction);
        }

        public IEnumerable<TransactionBase>? GetTransactions(Func<TransactionBase, bool> filter)
        {
            return Transactions?.Where(filter);
        }

        public void RemoveTransaction(Func<TransactionBase, bool> filter)
        {
            Transactions?.RemoveAll(new Predicate<TransactionBase>(filter));
        }
        #endregion

        #region TransactionCategory related methods
        public void AddTransactionCategory(TransactionCategory category)
        {
            Categories.Add(category);
        }

        public IEnumerable<TransactionCategory>? GetCategories(Func<TransactionCategory, bool> filter)
        {
            return Categories?.AsQueryable().Where(filter);
        }

        public void RemoveTransactionCategory(Func<TransactionCategory, bool> filter)
        {
            Categories.RemoveAll(new Predicate<TransactionCategory>(filter));
        }

        public bool TransactionCategoryExists(Func<TransactionCategory, bool> filter)
        {
            return Categories.FirstOrDefault(filter) is not null;
        }
        #endregion

        #region User related methods
        public bool UserExists(Func<User, bool> match)
        {
            return Users.Any(match);
        }
        public void AddUser(User user)
        {
            Users?.Add(user);
        }

        public int GetUserId(Func<User, bool> match)
        {
            var user = Users.FirstOrDefault(match);
            return user is not null ? user.Id : -1;
        }
        #endregion
    }
}
