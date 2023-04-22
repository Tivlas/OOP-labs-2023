using System.ComponentModel;
using Application.Abstractions.Console;
using Domain.Entities;
using Domain.Entities.Accounts;
using Domain.Entities.Interfaces;
using Domain.Entities.Transactions;
using Lab2.Const;
using Lab2.Helpers;
using Lab2.Validators;

namespace Lab2.UserActions
{
    public class Actions : IActions
    {
        public Dictionary<int, Action<int>> AvailableActions = new Dictionary<int, Action<int>>();

        private readonly IConsoleSimpleAccountService _simpleAccountService;
        private readonly IConsoleSimpleTransactionService _transactionService;
        private readonly IConsoleTransactionCategoryService _transactionCategoryService;
        private readonly IConsoleUserService _userService;

        public Actions(IConsoleSimpleAccountService simpleAccountService,
            IConsoleSimpleTransactionService transactionService,
            IConsoleTransactionCategoryService transactionCategoryService, IConsoleUserService consoleUserService)
        {
            _simpleAccountService = simpleAccountService;
            _transactionService = transactionService;
            _transactionCategoryService = transactionCategoryService;
            _userService = consoleUserService;

            AvailableActions[3] = AddSimpleAccount;
            AvailableActions[33] = RemoveSimpleAccount;
            AvailableActions[333] = PrintSimpleAccounts;
            AvailableActions[4] = AddTransactionCategory;
            AvailableActions[44] = RemoveTransactionCategory;
            AvailableActions[444] = PrintTransactionCategories;
            AvailableActions[5] = AddSimpleTransaction;
            AvailableActions[55] = RemoveSimpleTransaction;
            AvailableActions[555] = PrintSimpleTransactions;
            AvailableActions[7] = PrintTransactionsGroupedByDate;
            AvailableActions[8] = GetStatisticsByCategory;
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

        #region Receiving args 
        private string? GetEntityNameMustNotExist<T>(IBaseConsoleService<T> service, string message, int userId) where T : INamedEntity, IRelatedToUser
        {
            string? name;
            while (true)
            {
                Console.Write(message);
                name = Console.ReadLine();
                if (name == Constants.Cancel)
                {
                    return null;
                }
                if (string.IsNullOrWhiteSpace(name))
                {
                    ColorPrinter.Print(ConsoleColor.Red, "At least 1 non space character!");
                }
                else if (service.Exists(e => e.Name == name && e.UserId == userId))
                {
                    ColorPrinter.Print(ConsoleColor.Red, "This name is not available!");
                }
                else
                {
                    break;
                }
            }

            return name;
        }

        private string? GetEntityNameMustExist<T>(IBaseConsoleService<T> service, string message, int userId) where T : INamedEntity, IRelatedToUser
        {
            string? name;
            while (true)
            {
                Console.Write(message);
                name = Console.ReadLine();
                if (name == Constants.Cancel)
                {
                    return null;
                }
                if (string.IsNullOrWhiteSpace(name) && service is IConsoleTransactionCategoryService)
                {
                    name = "No category";
                    return name;
                }
                else if (string.IsNullOrWhiteSpace(name))
                {
                    ColorPrinter.Print(ConsoleColor.Red, "At least 1 non space character!");
                }
                else if (!service.Exists(e => e.Name == name && e.UserId == userId))
                {
                    ColorPrinter.Print(ConsoleColor.Red, "No such name!");
                }
                else
                {
                    break;
                }
            }

            return name;
        }

        public T? GetArg<T>(string message, IValidator? validator = null)
        {
            T? arg;
            while (true)
            {
                Console.Write(message);
                string? input = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(input))
                {
                    ColorPrinter.Print(ConsoleColor.Red, "Invalid input!");
                    continue;
                }
                if (input == Constants.Cancel)
                {
                    return default(T);
                }
                try
                {
                    var converter = TypeDescriptor.GetConverter(typeof(T));
                    if (converter != null)
                    {
                        arg = (T?)converter.ConvertFromString(input);
                        if (validator is not null && !validator.IsValid(input))
                        {
                            ColorPrinter.Print(ConsoleColor.Red, "Invalid input!");
                            continue;
                        }
                        break;
                    }
                }
                catch (Exception)
                {
                    ColorPrinter.Print(ConsoleColor.Red, "Invalid input!");
                }
            }
            return arg;
        }
        #endregion

        #region Simple account
        private void AddSimpleAccount(int userId)
        {
            decimal? balance = GetArg<decimal?>("Enter balance: ");
            if (balance is null)
            {
                return;
            }
            string? currencyName = GetArg<string?>("Enter currency name: ");
            if (currencyName is null)
            {
                return;
            }
            string? name = GetEntityNameMustNotExist(_simpleAccountService, "Enter storage name: ", userId);
            if (name is null)
            {
                return;
            }
            _simpleAccountService.Add(new SimpleAccount(balance.Value, currencyName, name, userId));
        }
        private void RemoveSimpleAccount(int userId)
        {
            string? name = GetEntityNameMustExist(_simpleAccountService, "Enter storage name: ", userId);
            if (name is null)
            {
                return;
            }
            _simpleAccountService.Delete(_simpleAccountService.FirstOrDefault(acc => acc.Name == name));
        }
        private void PrintSimpleAccounts(int userId)
        {
            PrintItems(_simpleAccountService, userId);
        }
        #endregion

        #region Transaction category
        private void AddTransactionCategory(int userId)
        {
            string? name = GetEntityNameMustNotExist(_transactionCategoryService, "Enter name: ", userId);
            if (name is null)
            {
                return;
            }
            _transactionCategoryService.Add(new TransactionCategory(name, userId));
        }

        private void RemoveTransactionCategory(int userId)
        {
            string? name = GetEntityNameMustExist(_transactionCategoryService, "Enter category name: ", userId);
            if (name is null)
            {
                return;
            }
            _transactionCategoryService.Delete(_transactionCategoryService.FirstOrDefault(tc => tc.Name == name));
        }

        private void PrintTransactionCategories(int userId)
        {
            PrintItems(_transactionCategoryService, userId);
        }
        #endregion

        #region Simple transaction
        private decimal? GetTransactionAmountOfMoney(string message)
        {
            decimal? arg;
            while (true)
            {
                Console.Write(message);
                string? input = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(input))
                {
                    ColorPrinter.Print(ConsoleColor.Red, "Invalid input!");
                    continue;
                }
                if (input == Constants.Cancel)
                {
                    return null;
                }
                try
                {
                    arg = Calculator.Calc(input);
                }
                catch (Exception)
                {
                    ColorPrinter.Print(ConsoleColor.Red, "Invalid expression!");
                    continue;
                }
                break;
            }
            return arg;
        }

        private void AddSimpleTransaction(int userId)
        {
            DateTime? date = GetArg<DateTime?>("Enter transaction date: ", new DateValidator());
            if (date is null)
            {
                return;
            }

            bool? isIncome = GetArg<bool?>("Is income (true/false): ");
            if (isIncome is null)
            {
                return;
            }

            decimal? money = GetTransactionAmountOfMoney("Enter amount of money: ");
            if (money is null)
            {
                return;
            }

            string? storName = GetEntityNameMustExist(_simpleAccountService, "Enter storage name: ", userId);
            if (storName is null)
            {
                return;
            }

            string? categoryName = GetEntityNameMustExist(_transactionCategoryService, "Enter category name: ", userId);
            if (categoryName is null)
            {
                return;
            }

            Console.Write("Enter comment: ");
            string? comment = Console.ReadLine();
            if (comment == Constants.Cancel)
            {
                return;
            }
            comment ??= string.Empty;

            var acc = _simpleAccountService.FirstOrDefault(c => c.Name == storName);

            if (isIncome == true)
            {
                acc.Balance += money.Value;
            }
            else
            {
                acc.Balance -= money.Value;
            }
            _simpleAccountService.Update(acc);
            _transactionService.Add(new SimpleTransaction(date.Value, isIncome.Value, money.Value, acc.Id,
                new TransactionCategory(categoryName, userId), comment, userId));
        }

        private void RemoveSimpleTransaction(int userId)
        {
            int? transactionId = GetArg<int?>("Enter transaction id: ");
            if (transactionId is null)
            {
                return;
            }
            var transaction = _transactionService.FirstOrDefault(tr => tr.Id == transactionId);
            if (transaction is null)
            {
                ColorPrinter.Print(ConsoleColor.Red, "Nu transaction with such Id!");
                return;
            }

            var account = _simpleAccountService.FirstOrDefault(acc => acc.Id == transaction.AccountId);

            if (transaction.IsIncome == true)
            {
                account.Balance -= transaction.AmountOfMoney;
            }
            else
            {
                account.Balance += transaction.AmountOfMoney;
            }
            _simpleAccountService.Update(account);
            _transactionService.Delete(transaction);
        }

        private void PrintSimpleTransactions(int userId)
        {
            PrintItems(_transactionService, userId);
        }

        #endregion

        #region Printing
        private void PrintItems<T>(IBaseConsoleService<T> service, int userId) where T : IEntity, IRelatedToUser
        {
            var items = service.List(e => e.UserId == userId);
            foreach (var item in items)
            {
                ColorPrinter.Print(ConsoleColor.Green, Constants.Delimiter);
                var infoList = item.GetInfo();
                foreach (var infoItem in infoList)
                {
                    Console.WriteLine($"{infoItem.PropName}: {infoItem.propValue}");
                }
            }
            ColorPrinter.Print(ConsoleColor.Green, Constants.Delimiter);
        }
        #endregion

        #region Statistics

        private void PrintGroup<T>(IEnumerable<T> items) where T : IEntity
        {
            foreach (var item in items)
            {
                ColorPrinter.Print(ConsoleColor.Green, Constants.Delimiter);
                var infoList = item.GetInfo();
                foreach (var infoItem in infoList)
                {
                    Console.WriteLine($"{infoItem.PropName}: {infoItem.propValue}");
                }
            }
        }

        private void PrintTransactionsGroupedByDate(int userId)
        {
            var transactions = _transactionService.List(trn => trn.UserId == userId);
            var query = transactions.GroupBy(trn => trn.TransactionDate.Date);
            foreach (var group in query)
            {
                Console.Write($"\nTransaction on ");
                ColorPrinter.Print(ConsoleColor.Green, group.Key.ToShortDateString());
                PrintGroup(group.ToList());
            }
        }

        private void GetStatisticsByCategory(int userId)
        {
            var transactions = _transactionService.List(trn => trn.UserId == userId);
            var query = transactions.GroupBy(trn => trn.Category.Name);
            var result = query.Select(g => new
            {
                Name = g.Key,
                Count = g.Count(),
                Percentage = (double)g.Count() / transactions.Count() * 100
            });

            foreach (var item in result)
            {
                Console.WriteLine($"{item.Name}: {item.Count} ({item.Percentage:F2}%)");
            }
        }
        #endregion
    }
}
