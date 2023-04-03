namespace FinanceManagementAppCore.Accounts
{
    public abstract class AccountAtInterestBase : AccountBase
    {

        public AccountAtInterestBase(decimal balance, string currencyName,  string name, int userId, Term term, int interestRate) : base(balance, currencyName,  name, userId)
        {
            AccountTerm = term;
            InterestRate = interestRate;
        }

        public Term AccountTerm { get; set; }

        public int InterestRate { get; set; }
    }
}