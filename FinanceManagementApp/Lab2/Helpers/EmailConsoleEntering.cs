using Lab2.Validators;

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
