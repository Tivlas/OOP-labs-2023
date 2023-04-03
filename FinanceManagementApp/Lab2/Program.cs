﻿using FinanceManagementAppCore;
using Lab2.DataBaseEmulation;
using Lab2.Helpers;
using Lab2.Services;
using System.Xml.Serialization;

Storage storage = new();

(string Email, int Id) curUser = (string.Empty, -1);

StartActions();


#region Login or user registration
void StartActions()
{
    while (true)
    {
        string password = string.Empty;
        string email = string.Empty;

        Console.Write("Do you want't to sign up [1] or sign in [2] (or [exit/e]): ");
        string? choice = Console.ReadLine();

        if (choice == "1")
        {
            email = EmailConsoleEntering.GetEmail();
            if (storage.UserExists(email))
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
            storage.AddUser(user);
            curUser.Email = email;
            curUser.Id = user.Id;
            break;
        }
        else if (choice == "2")
        {
            email = EmailConsoleEntering.GetEmail();
            password = PasswordConsoleEntering.GetPassword("Enter password");
            if (!storage.EmailPasswordMatch(email,password))
            {
                ColorPrinter.Print(ConsoleColor.Red, "Invalid email or password");
                continue;
            }
            curUser.Email = email;
            curUser.Id = storage.GetUserId(email);
        }
        else if (choice == "exit" || choice == "e")
        {
            Environment.Exit(0);
        }
        else { ColorPrinter.Print(ConsoleColor.Red, "Invalid choice! Enter [1] or [2]?"); }
    }
}
#endregion