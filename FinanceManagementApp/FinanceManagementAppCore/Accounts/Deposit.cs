using FinanceManagementAppCore.Enums;

namespace FinanceManagementAppCore.Accounts
{
    public class Deposit : AccountAtInterestBase
    {
        public Deposit(decimal balance, string currencyName,  string name, int userId, int cardId, Term term, int interestRate, DepositInterestAccrual accrual) : base(balance, currencyName,  name, userId, cardId, term, interestRate)
        {
            InterestAccrual = accrual;
        }

        public DepositInterestAccrual InterestAccrual { get; set; }
    }
}