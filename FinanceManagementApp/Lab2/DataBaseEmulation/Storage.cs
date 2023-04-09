using FinanceManagementAppCore;
using FinanceManagementAppCore.Accounts;
using FinanceManagementAppCore.Interfaces;
using FinanceManagementAppCore.Transactions;
using Lab2.Services;

namespace Lab2.DataBaseEmulation
{
    public class Storage : IDataManager
    {
        #region Containers
        public List<User> Users { get; set; } = new List<User>();

        public List<IBankEntity> BankEntities { get; set; } = new List<IBankEntity>();

        public List<TransactionCategory> Categories { get; set; } = new List<TransactionCategory>();

        public List<TransactionBase> MadeTransactions { get; set; } = new List<TransactionBase>();
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

        public void RemoveBankEntity(int userId, string name)
        {
            throw new NotImplementedException();
        }

        public bool BankEntityExists(string name, int userId)
        {
            return BankEntities is not null && BankEntities.Any(e => e.Name == name && e.UserId == userId);
        }
        #endregion

        #region Transaction related methods
        public void AddTransaction(TransactionBase transaction)
        {
            throw new NotImplementedException();
        }


        public void RemoveTransaction(int transactionId)
        {
            throw new NotImplementedException();
        }
        #endregion

        #region TransactionCategory related methods
        public void AddTransactionCategory(TransactionCategory category)
        {
            throw new NotImplementedException();
        }

        public void RemoveTransactionCategory(string name, int userId)
        {
            throw new NotImplementedException();
        }

        public bool TransactionCategoryExists(string name, int userId)
        {
            throw new NotImplementedException();
        }
        #endregion

        #region User related methods
        public bool UserExists(string email)
        {
            email = StringHasher.GetHash(email);
            return Users is not null && Users.Any(u => u.Email == email);
        }
        public void AddUser(User user)
        {
            Users?.Add(user);
        }

        public int GetUserId(string email)
        {
            if (Users is null)
            {
                return -1;
            }
            email = StringHasher.GetHash(email);
            int i = Users.FindIndex(u => u.Email == email);
            return i == -1 ? i : Users[i].Id;
        }
        #endregion
    }
}
