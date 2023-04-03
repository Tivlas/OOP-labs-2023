using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab2.UserActions
{
    public static class Actions
    {
        public static Dictionary<int, Action> UserActions = new Dictionary<int, Action>();

        static Actions()
        {
            UserActions[3] = AddSimpleAccount;
        }

        private static (int Balance, string CurrencyName,int UserId, int CardId) AddAccount()
        {
            // TODO: дописать возвращаемый тип, решить как связывать карту и счет
            throw new NotImplementedException();
        }

        private static void AddSimpleAccount()
        {

        }
    }
}
