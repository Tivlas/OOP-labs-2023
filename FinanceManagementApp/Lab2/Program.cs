using Application.Abstractions.Console;
using Application.Services.Console;
using Domain.Abstractions.ConsoleSync;
using Lab2.UserActions;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Persistence.Data;
using Persistence.UnitOfWork;

using IHost host = CreateHostBuilder(args).Build();
using var scope = host.Services.CreateScope();
var services = scope.ServiceProvider;

var actions = services.GetRequiredService<Actions>();

static IHostBuilder CreateHostBuilder(string[] args)
{
    return Host.CreateDefaultBuilder(args)
        .ConfigureServices((_, services) =>
        {
            services.AddSingleton<Actions>();
            services.AddSingleton<IConsoleUnitOfWork, ConsoleUnitOfWork>();
            services.AddSingleton<IDbEmulatorContext, DbEmulatorContext>();
            services.AddSingleton<IConsoleCardService, ConsoleCardService>();
            services.AddSingleton<IConsoleSimpleAccountService, ConsoleSimpleAccountService>();
            services.AddSingleton<IConsoleTransactionCategoryService, ConsoleTransactionCategoryService>();
            services.AddSingleton<IConsoleTransferService, ConsoleTransferService>();
            services.AddSingleton<IConsoleSimpleTransactionService, ConsoleSimpleTransactionService>();

        });
}


//StartActions();
//ExecuteCommand();

//#region Login or user registration
//void StartActions()
//{
//    while (true)
//    {
//        string password = string.Empty;
//        string email = string.Empty;

//        Console.Write("Do you want't to sign up [1] or sign in [2] (or [exit/e]): ");
//        string? choice = Console.ReadLine();

//        if (choice == "1")
//        {
//            email = EmailConsoleEntering.GetEmail();
//            if (storage.UserExists(u => u.Email == StringHasher.GetHash(email)))
//            {
//                ColorPrinter.Print(ConsoleColor.Red, "Such user already exists!");
//                continue;
//            }

//            password = PasswordConsoleEntering.GetPassword("Enter password");
//            while (true)
//            {
//                string passwordCopy = PasswordConsoleEntering.GetPassword("Confirm password");
//                if (password != passwordCopy)
//                {
//                    ColorPrinter.Print(ConsoleColor.Red, "Password mismatch!");
//                    continue;
//                }
//                break;
//            }
//            User user = new(StringHasher.GetHash(email), StringHasher.GetHash(password));
//            storage.AddUser(user);
//            curUser.Email = email;
//            curUser.Id = user.Id;
//            break;
//        }
//        else if (choice == "2")
//        {
//            email = EmailConsoleEntering.GetEmail();
//            password = PasswordConsoleEntering.GetPassword("Enter password");
//            if (!storage.EmailPasswordMatch(email, password))
//            {
//                ColorPrinter.Print(ConsoleColor.Red, "Invalid email or password");
//                continue;
//            }
//            curUser.Email = email;
//            curUser.Id = storage.GetUserId(u => u.Email == StringHasher.GetHash(email));
//        }
//        else if (choice == "exit" || choice == "e")
//        {
//            Environment.Exit(0);
//        }
//        else { ColorPrinter.Print(ConsoleColor.Red, "Invalid choice! Enter [1] or [2]?"); }
//    }
//}
//#endregion

//#region User actions

//void ExecuteCommand()
//{
//    ColorPrinter.Print(ConsoleColor.Green, Constants.Delimiter);
//    Console.WriteLine(Constants.HelpInfo);
//    ColorPrinter.Print(ConsoleColor.Green, Constants.Delimiter);
//    while (true)
//    {
//        ColorPrinter.Print(ConsoleColor.Yellow, "\nEnter command code: ", false);
//        if (!int.TryParse(Console.ReadLine(), out int code))
//        {
//            ColorPrinter.Print(ConsoleColor.Red, "Invalid input!");
//        }
//        else
//        {
//            if (!Actions.AvailableActions.ContainsKey(code))
//            {
//                ColorPrinter.Print(ConsoleColor.Red, "No such command!");
//            }
//            else
//            {
//                Actions.AvailableActions[code](curUser.Id, storage);
//            }
//        }
//    }
//}

//#endregion