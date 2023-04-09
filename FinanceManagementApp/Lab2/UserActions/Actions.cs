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
        public static Dictionary<int, Action<int, Storage>> UserActions = new Dictionary<int, Action<int, Storage>>();

        static Actions()
        {
            UserActions[3] = AddSimpleAccount;
            UserActions[13] = ListBankEntities;
        }


        private static (decimal Balance, string CurrencyName, string Name) AddAccount(int userId, Storage storage)
        {
            decimal balance;
            while (true)
            {
                Console.Write("Enter balance: ");
                string? temp = Console.ReadLine();
                if (!decimal.TryParse(temp, out balance))
                {
                    ColorPrinter.Print(ConsoleColor.Red, "Invalid input!");
                    continue;
                }
                break;
            }

            string? currencyName;
            while (true)
            {
                Console.Write("Enter currency name: ");
                currencyName = Console.ReadLine();
                if (currencyName is null || currencyName?.Length < 1)
                {
                    ColorPrinter.Print(ConsoleColor.Red, "Invalid input!");
                    continue;
                }
                break;
            }

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
                if (storage.AccountExists(name, userId))
                {
                    ColorPrinter.Print(ConsoleColor.Red, "Account with this name already exists!");
                    continue;
                }
                break;
            }

            return (balance, currencyName!, name);
        }

        public static void AddSimpleAccount(int userId, Storage storage)
        {
            var args = AddAccount(userId, storage);
            var acc = new SimpleAccount(args.Balance, args.CurrencyName, args.Name, userId);
            storage.AddAccount(acc);
        }

        public static void ListBankEntities(int userId, Storage storage)
        {
            var bankEntities = storage?.BankEntities?.Where(be => be.UserId == userId)?.ToList();

            if (bankEntities is not null)
            {
                foreach (var be in bankEntities.Select((value, index) => new { index, value.Name, value.Id }))
                {
                    ColorPrinter.Print(ConsoleColor.Yellow, $"{be.index + 1}. ", false);
                    Console.WriteLine($"Name: {be.Name}, id: {be.Id}");
                }
            }
            else
            {
                ColorPrinter.Print(ConsoleColor.Yellow, "No accounts!");
            }
        }
    }
}
