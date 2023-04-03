using Lab2.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Lab2.Helpers
{
    public static class EmailConsoleEntering
    {
        public static string GetEmail()
        {
            string? email;
            while (true)
            {
                Console.Write("Enter email: ");
                email = Console.ReadLine();
                if (!EmailValidator.IsValidEmail(email))
                {
                    ColorPrinter.Print(ConsoleColor.Red, "Invalid email format!");
                    continue;
                }
                break;
            }
            return email!.Trim();
        }
    }
}
