using FinanceManagementAppCore.Enums;

namespace FinanceManagementAppCore.Accounts
{
    public class Deposit : AccountAtInterestBase
    {
        public Deposit(decimal balance, string currencyName, string name, int userId, Term term, int interestRate, DepositInterestAccrual accrual) : base(balance, currencyName, name, userId, term, interestRate)
        {
            InterestAccrual = accrual;
        }

        public DepositInterestAccrual InterestAccrual { get; set; }
    }
}