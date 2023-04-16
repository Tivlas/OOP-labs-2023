using System.ComponentModel;
using Application.Abstractions.Console;
using Domain.Cards;
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

            AvailableActions[3] = AddSimpleAccount;
            AvailableActions[10] = RemoveSimpleAccount;
            AvailableActions[4] = AddCard;
            AvailableActions[5] = AddTransactioncategory;
            AvailableActions[11] = RemoveCard;
            AvailableActions[6] = AddSimpleTransaction;
            AvailableActions[8] = RemoveTransactionCategory;
            AvailableActions[12] = PrintSimpleAccounts;
            AvailableActions[15] = PrintCards;
            AvailableActions[13] = PrintTransactionCategories;
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
                if (string.IsNullOrWhiteSpace(name))
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
            string? name = GetEntityNameMustNotExist(_simpleAccountService, "Enter account name: ", userId);
            if (name is null)
            {
                return;
            }
            _simpleAccountService.Add(new SimpleAccount(balance.Value, currencyName, name, userId));
        }

        private void RemoveSimpleAccount(int userId)
        {
            string? name = GetEntityNameMustExist(_simpleAccountService, "Enter account name: ", userId);
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

        #region Card
        private void AddCard(int userId)
        {
            string? name = GetEntityNameMustNotExist(_cardService, "Enter card name: ", userId);
            if (name is null)
            {
                return;
            }
            string? accName = GetEntityNameMustExist(_simpleAccountService, "Enter related account name: ", userId);
            if (accName is null)
            {
                return;
            }
            var acc = _simpleAccountService.FirstOrDefault(acc => acc.Name == accName);
            decimal balance = acc.Balance;
            int accId = acc.Id;
            string currencyName = acc.CurrencyName;
            _cardService.Add(new Card(accId, name, balance, currencyName));
        }

        private void RemoveCard(int userId)
        {
            string? name = GetEntityNameMustExist(_cardService, "Enter card name: ", userId);
            if (name is null)
            {
                return;
            }
            _cardService.Delete(_cardService.FirstOrDefault(c => c.Name == name));
        }

        private void PrintCards(int userId)
        {
            PrintItems(_cardService, userId);
        }
        #endregion

        #region Transaction category
        private void AddTransactioncategory(int userId)
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
            decimal? money = GetArg<decimal?>("Enter amount of money: ");
            if (money is null)
            {
                return;
            }
            string? cardName = GetEntityNameMustExist(_cardService, "Enter card name: ", userId);
            if (cardName is null)
            {
                return;
            }

            var card = _cardService.FirstOrDefault(c => c.Name == cardName);
            var accId = card.AccountId;
            var acc = _simpleAccountService.FirstOrDefault(a => a.Id == accId);
            if (isIncome == true)
            {
                acc.Balance += money.Value;
                card.Balance += money.Value;
            }
            else
            {
                acc.Balance -= money.Value;
                card.Balance -= money.Value;
            }
            _simpleAccountService.Update(acc);
            _cardService.Update(card);

            string? categoryName = GetEntityNameMustExist(_transactionCategoryService,"Enter category name: ", userId);
            if(categoryName is null)
            {
                return;
            }

            Console.WriteLine("Enter comment: ");
            string? comment = Console.ReadLine();
            if(comment == Constants.Cancel)
            {
                return;
            }
            comment ??= string.Empty;

            _transactionService.Add(new SimpleTransaction(date.Value, isIncome.Value, money.Value, accId,
                new TransactionCategory(categoryName, userId), comment, userId));
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
                    Console.WriteLine($"{infoItem.PropName,3}: {infoItem.propValue}");
                }
            }
            ColorPrinter.Print(ConsoleColor.Green, Constants.Delimiter);
        }
        #endregion

        #region Statistics
        // TODO: add methods
        #endregion
    }
}
