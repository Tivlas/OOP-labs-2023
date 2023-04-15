using System.ComponentModel;
using System.Reflection.Metadata;
using Application.Abstractions.Console;
using Domain.Entities;
using Domain.Entities.Accounts;
using Domain.Entities.Interfaces;
using Lab2.Const;
using Lab2.Helpers;

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

        private string? GetEntityName<T>(IBaseConsoleService<T> service, string message, int userId) where T : INamedEntity, IRelatedToUser
        {
            string? name;
            while (true)
            {
                Console.Write(message);
                name = Console.ReadLine();
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
            if (name == Constants.Cancel)
            {
                return null;
            }
            return name;
        }

        private T? GetArg<T>(string message)
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
                    return default(T?);
                }
                try
                {
                    var converter = TypeDescriptor.GetConverter(typeof(T));
                    if (converter != null)
                    {
                        arg = (T?)converter.ConvertFromString(input);
                        break;
                    }
                }
                catch (NotSupportedException)
                {
                    ColorPrinter.Print(ConsoleColor.Red, "Invalid input!");
                }
            }
            return arg;
        }

        #region Simple account
        public void AddSimpleAccount(int userId)
        {
            decimal? balance = GetArg<decimal>("Enter balance: ");
            if (balance is null)
            {
                return;
            }
            string? currencyName = GetArg<string>("Enter currency name: ");
            if (currencyName is null)
            {
                return;
            }
            string? name = GetEntityName<SimpleAccount>(_simpleAccountService, "Enter account name: ", userId);
            if(name is null)
            {
                return;
            }
            _simpleAccountService.Add(new SimpleAccount(balance.Value, currencyName, name, userId));
        }
        #endregion


        #region Statistics
        // TODO: add methods
        #endregion
    }
}
