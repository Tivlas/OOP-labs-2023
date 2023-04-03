using FinanceManagementAppCore.Enums;

namespace FinanceManagementAppCore.Accounts
{
    public class Loan : AccountAtInterestBase
    {
        public Loan(decimal balance, string currencyName, string name, int userId,  Term term, int interestRate, LoanPayments payments) : base(balance, currencyName, name, userId,  term, interestRate)
        {
            Payments = payments;
        }

        public LoanPayments Payments { get; set; }
    }
}