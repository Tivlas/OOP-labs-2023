﻿namespace FinanceManagementAppCore.Accounts
{
    public class SimpleAccount : AccountBase
    {
        public SimpleAccount(decimal balance, string currencyName, string name, int userId) : base(balance, currencyName, name, userId)
        {
        }
    }
}