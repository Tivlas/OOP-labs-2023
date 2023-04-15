using Application.Abstractions.Console;
using Application.Services.Console;
using Domain.Abstractions.ConsoleSync;
using Domain.Entities;
using Lab2.Const;
using Lab2.Helpers;
using Lab2.Services;
using Lab2.UserActions;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Persistence.Data;
using Persistence.UnitOfWork;

using IHost host = CreateHostBuilder(args).Build();
using var scope = host.Services.CreateScope();
var services = scope.ServiceProvider;
(string Email, int Id) curUser = ("NoEmail", -1);
Actions actions;
try
{
    actions = services.GetRequiredService<Actions>();
}
catch (Exception ex)
{
    Console.WriteLine(ex.Message);
    return;
}

static IHostBuilder CreateHostBuilder(string[] args)
{
    return Host.CreateDefaultBuilder(args)
        .ConfigureServices((_, services) =>
        {
            services.AddSingleton<Actions>();
            services.AddSingleton<IConsoleUnitOfWork, ConsoleUnitOfWork>();
            services.AddSingleton<IConsoleCardService, ConsoleCardService>();
            services.AddSingleton<IConsoleSimpleAccountService, ConsoleSimpleAccountService>();
            services.AddSingleton<IConsoleTransactionCategoryService, ConsoleTransactionCategoryService>();
            services.AddSingleton<IConsoleTransferService, ConsoleTransferService>();
            services.AddSingleton<IConsoleSimpleTransactionService, ConsoleSimpleTransactionService>();
            services.AddSingleton<IConsoleUserService, ConsoleUserService>();
            services.AddSingleton<IDbEmulatorContext, DbEmulatorContext>();
        });
}


StartActions();
ExecuteCommand();

#region Log in or sign up
void StartActions()
{
    while (true)
    {
        string password = string.Empty;
        string email = string.Empty;

        Console.Write("Do you want't to sign up [1] or log in [2] (or [exit/e]): ");
        string? choice = Console.ReadLine();

        if (choice == "1")
        {
            email = EmailConsoleEntering.GetEmail();
            if (actions.UserExists(u => u.Email == StringHasher.GetHash(email)))
            {
                ColorPrinter.Print(ConsoleColor.Red, "Such user already exists!");
                continue;
            }

            password = PasswordConsoleEntering.GetPassword("Enter password");
            while (true)
            {
                string passwordCopy = PasswordConsoleEntering.GetPassword("Confirm password");
                if (password != passwordCopy)
                {
                    ColorPrinter.Print(ConsoleColor.Red, "Password mismatch!");
                    continue;
                }
                break;
            }
            User user = new(StringHasher.GetHash(email), StringHasher.GetHash(password));
            actions.AddUser(user);
            curUser.Email = email;
            curUser.Id = user.Id;
            break;
        }
        else if (choice == "2")
        {
            email = EmailConsoleEntering.GetEmail();
            password = PasswordConsoleEntering.GetPassword("Enter password");
            if (!actions.UserExists(u => u.Email == StringHasher.GetHash(email)
                                      && u.Password == StringHasher.GetHash(password)))
            {
                ColorPrinter.Print(ConsoleColor.Red, "Invalid email or password");
                continue;
            }
            curUser.Email = email;
            curUser.Id = actions.GetUserId(u => u.Email == StringHasher.GetHash(email));
            break;
        }
        else if (choice == "exit" || choice == "e")
        {
            Environment.Exit(0);
        }
        else { ColorPrinter.Print(ConsoleColor.Red, "Invalid choice! Enter [1] or [2]?"); }
    }
}
#endregion

#region User actions

void ExecuteCommand()
{
    ColorPrinter.Print(ConsoleColor.Green, Constants.Delimiter);
    Console.WriteLine(Constants.HelpInfo);
    ColorPrinter.Print(ConsoleColor.Green, Constants.Delimiter);
    while (true)
    {
        if (curUser.Id < 0)
        {
            StartActions();
        }
        ColorPrinter.Print(ConsoleColor.Yellow, "\nEnter command code: ", false);
        if (!int.TryParse(Console.ReadLine(), out int code))
        {
            ColorPrinter.Print(ConsoleColor.Red, "Invalid input!");
        }
        else
        {
            if (code < 0)
            {
                LogOut();
            }
            else if (code == 0)
            {
                Environment.Exit(0);
            }
            else if (!actions.AvailableActions.ContainsKey(code))
            {
                ColorPrinter.Print(ConsoleColor.Red, "No such command!");
            }
            else
            {
                actions.AvailableActions[code](curUser.Id);
            }
        }
    }
}

void LogOut()
{
    curUser.Email = string.Empty;
    curUser.Id = -100;
}

#endregion