using FinanceManagementAppCore.Accounts;
using Lab2.DataBaseEmulation;
using Lab2.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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

        private static T ParseInput<T>(string message)
        {
            T? obj;
            while (true)
            {
                try
                {
                    Console.Write($"{message}: ");
                    string? input = Console.ReadLine();
                    var converter = TypeDescriptor.GetConverter(typeof(T));
                    obj = (T?)converter?.ConvertFromString(input);
                }
                catch (Exception)
                {
                    ColorPrinter.Print(ConsoleColor.Red, "Invalid input!");
                    continue;
                }
                break;
            }
            return obj!;
        }

        private static (decimal Balance, string CurrencyName) AddAccount()
        {
            decimal balance = ParseInput<decimal>("Enter balance");
            string currencyName = ParseInput<string>("Enter balance");
            return (balance, currencyName);
        }

        private static void AddSimpleAccount(int userId, Storage storage)
        {
            var args = AddAccount();
            string? name;
            while (true)
            {
                Console.Write("Enter account name: ");
                name = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(name))
                {
                    ColorPrinter.Print(ConsoleColor.Red, "At least 1 character!");
                    continue;
                }
                if (storage.AccountExists(name))
                {
                    ColorPrinter.Print(ConsoleColor.Red, "Account with this name already exists!");
                    continue;
                }
                break;
            }
            var acc = new SimpleAccount(args.Balance, args.CurrencyName, name, userId);
            storage.AddAccount
        }
    }
}
