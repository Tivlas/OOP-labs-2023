using FinanceManagementAppCore.Accounts;
using FinanceManagementAppCore.Cards;
using Lab2.DataBaseEmulation;
using Lab2.Helpers;
using Lab2.Const;
using FinanceManagementAppCore.Transactions;

namespace Lab2.UserActions
{
    public static class Actions
    {
        public static Dictionary<int, Action<int, Storage>> UserActions = new Dictionary<int, Action<int, Storage>>();

        static Actions()
        {
            UserActions[3] = AddSimpleAccount;
            UserActions[6] = AddCard;
            UserActions[7] = AddTransactionCategory;
            UserActions[10] = RemoveTransactionCategory;
            UserActions[12] = RemoveBankEntity;
            UserActions[13] = ListBankEntities;
            UserActions[14] = Listcategories;
        }


        #region Bank entity actions
        // TODO: add loan, deposit
        private static string GetBankEntityName(int userId, Storage storage)
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

            string name = GetBankEntityName(userId, storage);
            if (name == "CANCEL")
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
            string name = GetBankEntityName(userId, storage);
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
                if (acc is null)
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
                foreach (var be in bankEntities)
                {
                    ColorPrinter.Print(ConsoleColor.Green, Constants.Delimiter);
                    var infoList = be.GetInfo();
                    foreach (var infoItem in infoList)
                    {
                        Console.WriteLine($"{infoItem.PropName,3}: {infoItem.propValue}");
                    }
                }
                ColorPrinter.Print(ConsoleColor.Green, Constants.Delimiter);
            }
            else
            {
                ColorPrinter.Print(ConsoleColor.Yellow, "No accounts or cards!");
            }
        }

        public static void RemoveBankEntity(int userId, Storage storage)
        {
            Console.Write("Enter name: ");
            string? name = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(name))
            {
                storage?.BankEntities?.RemoveAll(be => be.Name == name);
            }
        }

        #endregion

        #region Transaction & transaction category actions

        public static void AddTransactionCategory(int userId, Storage storage)
        {
            string? name;
            while (true)
            {
                Console.Write("Enter name: ");
                name = Console.ReadLine();
                if (name == "CANCEL")
                {
                    return;
                }
                if (string.IsNullOrWhiteSpace(name))
                {
                    ColorPrinter.Print(ConsoleColor.Red, "At least 1 character!");
                }
                break;
            }
            if (!storage!.TransactionCategoryExists(name!, userId))
            {
                storage?.AddTransactionCategory(new TransactionCategory(name!, userId));
            }
        }

        public static void RemoveTransactionCategory(int userId, Storage storage)
        {
            string? name = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(name))
            {
                storage?.RemoveTransactionCategory(name!, userId);
            }
        }

        public static void Listcategories(int userId,Storage storage)
        {
            var categories = storage?.Categories?.Where(ctg => ctg.UserId == userId)?.ToList();

            if (categories is not null)
            {
                foreach (var ctg in categories)
                {
                    ColorPrinter.Print(ConsoleColor.Green, Constants.Delimiter);
                    var infoList = ctg.GetInfo();
                    foreach (var infoItem in infoList)
                    {
                        Console.WriteLine($"{infoItem.PropName,3}: {infoItem.propValue}");
                    }
                }
                ColorPrinter.Print(ConsoleColor.Green, Constants.Delimiter);
            }
            else
            {
                ColorPrinter.Print(ConsoleColor.Yellow, "No categories!");
            }
        }

        // TODO: list categories
        #endregion

        #region Statistics
        // TODO: add methods
        #endregion
    }
}
