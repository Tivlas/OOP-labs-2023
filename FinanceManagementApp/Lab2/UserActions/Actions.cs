﻿using Application.Abstractions.Console;
using Domain.Entities;

namespace Lab2.UserActions
{
    public class Actions : IActions
    {
        public Dictionary<int, Action<int>> AvailableActions = new Dictionary<int, Action<int>>();

        private readonly IConsoleCardService _cardService;
        private readonly IConsoleSimpleAccountService _simpleAccountService;
        private readonly IConsoleSimpleTransactionService _transactionService;
        private readonly IConsoleTransferService _transferService;
        private readonly IConsoleTransactionCategoryService _transactionCategoryService;
        private readonly IConsoleUserService _userService;

        public Actions(IConsoleCardService cardService, IConsoleSimpleAccountService simpleAccountService,
            IConsoleSimpleTransactionService transactionService, IConsoleTransferService transferService,
            IConsoleTransactionCategoryService transactionCategoryService, IConsoleUserService consoleUserService)
        {
            _cardService = cardService;
            _simpleAccountService = simpleAccountService;
            _transactionService = transactionService;
            _transferService = transferService;
            _transactionCategoryService = transactionCategoryService;
            _userService = consoleUserService;
            //AvailableActions[3] = AddSimpleAccount;
            //AvailableActions[6] = AddCard;
            //AvailableActions[7] = AddTransactionCategory;
            //AvailableActions[8] = AddSimpleTransaction;
            //AvailableActions[10] = RemoveTransactionCategory;
            //AvailableActions[12] = RemoveBankEntity;
            //AvailableActions[13] = ListBankEntities;
            //AvailableActions[14] = ListCategories;
            //AvailableActions[15] = ListTransactions;
        }

        #region User
        public bool UserExists(Func<User, bool> match)
        {
            return _userService.Exists(match);
        }

        public void AddUser(User u)
        {
            _userService.Add(u);
        }

        public int GetUserId(Func<User, bool> match)
        {
            var user = _userService.FirstOrDefault(match);
            return user == null ? -1 : user.Id;
        }
        #endregion

        //#region Bank entity actions
        //// TODO: add loan, deposit
        //private static string GetBankEntityName(int userId, Storage storage)
        //{
        //    string? name;
        //    while (true)
        //    {
        //        Console.Write("Enter name: ");
        //        name = Console.ReadLine();
        //        if (name == Constants.Cancel)
        //        {
        //            return name;
        //        }
        //        if (string.IsNullOrWhiteSpace(name))
        //        {
        //            ColorPrinter.Print(ConsoleColor.Red, "At least 1 character!");
        //            continue;
        //        }
        //        if (storage.BankEntityExists(be => be.Name == name && be.UserId == userId))
        //        {
        //            ColorPrinter.Print(ConsoleColor.Red, "Account with this name already exists!");
        //            continue;
        //        }
        //        break;
        //    }
        //    return name;
        //}

        //private static (bool Canceled, decimal Balance, string CurrencyName, string Name) GetGeneralArgs(int userId, Storage storage)
        //{
        //    var canceled = (true, 0, "", "");
        //    decimal balance;
        //    while (true)
        //    {
        //        Console.Write("Enter balance: ");
        //        string? temp = Console.ReadLine();
        //        if (temp == Constants.Cancel)
        //        {
        //            return canceled;
        //        }
        //        if (!decimal.TryParse(temp, out balance))
        //        {
        //            ColorPrinter.Print(ConsoleColor.Red, "Invalid input!");
        //            continue;
        //        }
        //        break;
        //    }

        //    string? currencyName;
        //    while (true)
        //    {
        //        Console.Write("Enter currency name: ");
        //        currencyName = Console.ReadLine();
        //        if (currencyName == Constants.Cancel)
        //        {
        //            return canceled;
        //        }
        //        if (currencyName is null || currencyName?.Length < 1)
        //        {
        //            ColorPrinter.Print(ConsoleColor.Red, "Invalid input!");
        //            continue;
        //        }
        //        break;
        //    }

        //    string name = GetBankEntityName(userId, storage);
        //    if (name == Constants.Cancel)
        //    {
        //        return canceled;
        //    }

        //    return (false, balance, currencyName!, name);
        //}

        //public static void AddSimpleAccount(int userId, Storage storage)
        //{
        //    var args = GetGeneralArgs(userId, storage);
        //    if (args.Canceled == true)
        //    {
        //        return;
        //    }
        //    var acc = new SimpleAccount(args.Balance, args.CurrencyName, args.Name, userId);
        //    storage.AddBankEntity(acc);
        //}

        //public static void AddCard(int userId, Storage storage)
        //{
        //    string name = GetBankEntityName(userId, storage);
        //    decimal balance;
        //    string currencyName;
        //    int accountId;
        //    while (true)
        //    {
        //        Console.Write("Enter related account name: ");
        //        string? relatedAccName = Console.ReadLine();
        //        if (relatedAccName == Constants.Cancel)
        //        {
        //            return;
        //        }
        //        var acc = storage?.FirstOrDefaultBankEntity(be => be.Name == relatedAccName);
        //        if (acc is null)
        //        {
        //            ColorPrinter.Print(ConsoleColor.Red, "No such account!");
        //            continue;
        //        }
        //        accountId = acc.Id;
        //        balance = acc.Balance;
        //        currencyName = acc.CurrencyName;
        //        break;
        //    }

        //    var card = new Card(accountId, name, balance, currencyName);
        //    storage?.AddBankEntity(card);
        //}

        //public static void ListBankEntities(int userId, Storage storage)
        //{
        //    var bankEntities = storage?.GetBankEntities(be => be.UserId == userId)?.ToList();
        //    ListItems(bankEntities, "No accounts or cards");
        //}

        //public static void RemoveBankEntity(int userId, Storage storage)
        //{
        //    Console.Write("Enter name: ");
        //    string? name = Console.ReadLine();
        //    if (!string.IsNullOrWhiteSpace(name))
        //    {
        //        storage?.RemoveBankEntity(be => be.UserId == userId && be.Name == name);
        //    }
        //}

        //#endregion

        //#region Transaction category actions

        //public static void AddTransactionCategory(int userId, Storage storage)
        //{
        //    string? name;
        //    while (true)
        //    {
        //        Console.Write("Enter name: ");
        //        name = Console.ReadLine();
        //        if (name == Constants.Cancel)
        //        {
        //            return;
        //        }
        //        if (string.IsNullOrWhiteSpace(name))
        //        {
        //            ColorPrinter.Print(ConsoleColor.Red, "At least 1 character!");
        //        }
        //        break;
        //    }
        //    if (!storage!.TransactionCategoryExists(tc => tc.Name == name && tc.UserId == userId))
        //    {
        //        storage?.AddTransactionCategory(new TransactionCategory(name!, userId));
        //    }
        //}

        //public static void RemoveTransactionCategory(int userId, Storage storage)
        //{
        //    string? name = Console.ReadLine();
        //    if (!string.IsNullOrWhiteSpace(name))
        //    {
        //        storage?.RemoveTransactionCategory(tc => tc.Name == name && tc.UserId == userId);
        //    }
        //}

        //public static void ListCategories(int userId, Storage storage)
        //{
        //    var categories = storage?.GetCategories(ctg => ctg.UserId == userId)?.ToList();
        //    ListItems(categories, "No categories!");
        //}

        //#endregion

        //#region Transaction actions

        //private static (bool Canceled, DateTime Date, decimal AmoutOfMoney, int AccountId) GetBasicTransactionArgs(int userId, Storage storage)
        //{
        //    DateTime date;
        //    var canceled = (true, DateTime.Now, 0, -1);
        //    while (true)
        //    {
        //        Console.Write("Enter date: ");
        //        string? temp = Console.ReadLine();
        //        if (temp == Constants.Cancel)
        //        {
        //            return canceled;
        //        }
        //        if (!DateValidator.IsValid(temp, out date))
        //        {
        //            ColorPrinter.Print(ConsoleColor.Red, "Invalid date format!");
        //            continue;
        //        }
        //        break;
        //    }

        //    decimal amountOfMoney;
        //    while (true)
        //    {
        //        Console.Write("Enter amount of money: ");
        //        string? temp = Console.ReadLine();
        //        if (temp == Constants.Cancel)
        //        {
        //            return canceled;
        //        }
        //        if (!decimal.TryParse(temp, out amountOfMoney))
        //        {
        //            ColorPrinter.Print(ConsoleColor.Red, "Invalid input!");
        //            continue;
        //        }
        //        break;
        //    }

        //    int accountId;
        //    while (true)
        //    {
        //        Console.Write("Enter account or card name: ");
        //        string? name = Console.ReadLine();
        //        if (name == Constants.Cancel)
        //        {
        //            return canceled;
        //        }
        //        if (!storage!.BankEntityExists(be => be.Name == name && be.UserId == userId))
        //        {
        //            ColorPrinter.Print(ConsoleColor.Red, "Such account or card does not exist!");
        //            continue;
        //        }
        //        else
        //        {
        //            accountId = storage!.FirstOrDefaultBankEntity(be => be.Name == name && be.UserId == userId)!.Id;
        //        }
        //        break;
        //    }
        //    return (false, date, amountOfMoney, accountId);
        //}
        //public static void AddSimpleTransaction(int userId, Storage storage)
        //{
        //    var args = GetBasicTransactionArgs(userId, storage);
        //    if (args.Canceled == true)
        //    {
        //        return;
        //    }

        //    string? comment;
        //    string? categoryName;
        //    bool isIncome;

        //    Console.Write("Enter comment: ");
        //    comment = Console.ReadLine();
        //    if (comment == Constants.Cancel)
        //    {
        //        return;
        //    }
        //    if (string.IsNullOrWhiteSpace(comment))
        //    {
        //        comment = string.Empty;
        //    }
        //    while (true)
        //    {
        //        Console.Write("Enter category name: ");
        //        categoryName = Console.ReadLine();
        //        if (categoryName == Constants.Cancel)
        //        {
        //            return;
        //        }
        //        if (!storage.TransactionCategoryExists(tc => tc.Name == categoryName && tc.UserId == userId))
        //        {
        //            ColorPrinter.Print(ConsoleColor.Red, "Such category does not exist!");
        //            continue;
        //        }
        //        break;
        //    }
        //    while (true)
        //    {
        //        Console.Write("Is income[y/n]: ");
        //        string? choice = Console.ReadLine();
        //        switch (choice)
        //        {
        //            case "y":
        //                isIncome = true;
        //                break;
        //            case "n":
        //                isIncome = false;
        //                break;
        //            case Constants.Cancel:
        //                return;
        //            default:
        //                ColorPrinter.Print(ConsoleColor.Red, "Invalid input!");
        //                continue;
        //        }
        //        break;
        //    }
        //    TransactionCategory category = new(categoryName!, userId);
        //    SimpleTransaction transaction = new(args.Date, isIncome, args.AmoutOfMoney, args.AccountId, category, comment, userId);
        //    storage?.AddTransaction(transaction);
        //}

        //public static void ListTransactions(int userId, Storage storage)
        //{
        //    var transactions = storage?.GetTransactions(trn => trn.UserId == userId)?.ToList();
        //    ListItems(transactions, "No transactions!");
        //}
        //#endregion


        //private static void ListItems<T>(IEnumerable<T>? collection, string noItemsMessage) where T : IEntity
        //{
        //    if (collection is not null && collection.Count() > 0)
        //    {
        //        foreach (var item in collection)
        //        {
        //            ColorPrinter.Print(ConsoleColor.Green, Constants.Delimiter);
        //            var infoList = item.GetInfo();
        //            foreach (var infoItem in infoList)
        //            {
        //                Console.WriteLine($"{infoItem.PropName,3}: {infoItem.propValue}");
        //            }
        //        }
        //        ColorPrinter.Print(ConsoleColor.Green, Constants.Delimiter);
        //    }
        //    else if (collection is null || collection.Count() < 1)
        //    {
        //        ColorPrinter.Print(ConsoleColor.Red, noItemsMessage);
        //    }
        //}


        #region Statistics
        // TODO: add methods
        #endregion
    }
}
