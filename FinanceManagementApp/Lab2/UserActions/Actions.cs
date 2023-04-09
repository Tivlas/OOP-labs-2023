using FinanceManagementAppCore.Accounts;
using FinanceManagementAppCore.Cards;
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
            UserActions[6] = AddCard;
        }

        private static string GetName(int userId, Storage storage)
        {
            string? name;
            while (true)
            {
                Console.Write("Enter name: ");
                name = Console.ReadLine();
                if (name == "CANCEL")
                {
                    return name;
                }
                if (string.IsNullOrWhiteSpace(name))
                {
                    ColorPrinter.Print(ConsoleColor.Red, "At least 1 character!");
                    continue;
                }
                if (storage.BankEntityExists(name, userId))
                {
                    ColorPrinter.Print(ConsoleColor.Red, "Account with this relatedAccName already exists!");
                    continue;
                }
                break;
            }
            return name;
        }

        private static (bool Canceled, decimal Balance, string CurrencyName, string Name) GetGeneralArgs(int userId, Storage storage)
        {
            var canceled = (true, 0, "", "");
            decimal balance;
            while (true)
            {
                Console.Write("Enter balance: ");
                string? temp = Console.ReadLine();
                if (temp == "CANCEL")
                {
                    return canceled;
                }
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
                if (currencyName == "CANCEL")
                {
                    return canceled;
                }
                if (currencyName is null || currencyName?.Length < 1)
                {
                    ColorPrinter.Print(ConsoleColor.Red, "Invalid input!");
                    continue;
                }
                break;
            }

            string name = GetName(userId, storage);
            if(name == "CANCEL")
            {
                return canceled;
            }

            return (false, balance, currencyName!, name);
        }

        public static void AddSimpleAccount(int userId, Storage storage)
        {
            var args = GetGeneralArgs(userId, storage);
            if (args.Canceled == true)
            {
                return;
            }
            var acc = new SimpleAccount(args.Balance, args.CurrencyName, args.Name, userId);
            storage.AddBankEntity(acc);
        }

        public static void AddCard(int userId, Storage storage)
        {
            string name = GetName(userId, storage);
            decimal balance;
            string currencyName;
            int accountId;
            while (true)
            {
                Console.Write("Enter related account name: ");
                string? relatedAccName = Console.ReadLine();
                if (relatedAccName == "CANCEL")
                {
                    return;
                }
                var acc = storage?.BankEntities?.FirstOrDefault(be => be.Name == relatedAccName);
                if(acc is null)
                {
                    ColorPrinter.Print(ConsoleColor.Red, "No such account!");
                    continue;
                }
                accountId = acc.Id;
                balance = acc.Balance;
                currencyName = acc.CurrencyName;
                break;
            }

            var card = new Card(accountId, name, balance, currencyName);
            storage?.AddBankEntity(card);
        }

        public static void ListBankEntities(int userId, Storage storage)
        {
            var bankEntities = storage?.BankEntities?.Where(be => be.UserId == userId)?.ToList();

            if (bankEntities is not null)
            {
                foreach (var be in bankEntities.Select((value, index) => new { index, value}))
                {
                    ColorPrinter.Print(ConsoleColor.Yellow, $"{be.index + 1}. ", false);
                    var infoList = be.value.GetInfo();
                    List<string> temp = new();
                    foreach(var infoItem in infoList)
                    {
                        temp.Add($"{infoItem.PropName}: {infoItem.propValue}");
                    }
                    Console.WriteLine(string.Join(", ", temp));
                }
            }
            else
            {
                ColorPrinter.Print(ConsoleColor.Yellow, "No accounts!");
            }
        }
    }
}
