using FinanceManagementAppCore;
using FinanceManagementAppCore.Accounts;
using FinanceManagementAppCore.Interfaces;
using FinanceManagementAppCore.Transactions;
using Lab2.Services;

namespace Lab2.DataBaseEmulation
{
    public class Storage : IDataManager
    {
        public List<User>? Users { get; set; } = new List<User>();

        public List<IBankEntity>? BankEntities { get; set; } = new List<IBankEntity>();

        public List<TransactionCategory>? Categories { get; set; } = new List<TransactionCategory>();

        public List<TransactionBase>? MadeTransactions { get; set; } = new List<TransactionBase>();

        public bool AddAccountIfNotExist(int userId, AccountBase account)
        {
            throw new NotImplementedException();
        }

        public void AddTransaction(int userId, TransactionBase transaction)
        {
            throw new NotImplementedException();
        }

        public void AddTransactionCategory(int userId, TransactionCategory category)
        {
            throw new NotImplementedException();
        }

        public void AddUser(User user)
        {
            Users?.Add(user);
        }

        public bool EmailPasswordMatch(string email, string password)
        {
            email = StringHasher.GetHash(email);
            password = StringHasher.GetHash(password);
            return Users is not null && Users.Any(u => u.Email == email && u.Password == password);
        }

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

        public int GetUserId(string email)
        {
            if(Users is null)
            {
                return -1;
            }
            email = StringHasher.GetHash(email);
            int i = Users.FindIndex(u => u.Email == email);
            return i == -1 ? i : Users[i].Id;
        }

        public void RemoveAccount(int userId, int accountId)
        {
            throw new NotImplementedException();
        }

        public void RemoveTransaction(int userId, int transactionId)
        {
            throw new NotImplementedException();
        }

        public void RemoveTransactionCategory(int userId, int categoryId)
        {
            throw new NotImplementedException();
        }

        public bool UserExists(string email)
        {
            email = StringHasher.GetHash(email);
            return Users is not null && Users.Any(u => u.Email == email);
        }
    }
}
