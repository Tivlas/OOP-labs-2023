using Lab2.Validators;

namespace Lab2.Helpers
{
    public static class EmailConsoleEntering
    {
        public static string GetEmail()
        {
            EmailValidator validator = new();
            string? email;
            while (true)
            {
                Console.Write("Enter email: ");
                email = Console.ReadLine();
                if (!validator.IsValid(email))
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
