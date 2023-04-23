namespace Lab2.Helpers
{
    public static class PasswordConsoleEntering
    {
        public static string GetPassword(string message)
        {
            ConsoleKey key;
            string password = string.Empty;
            while (true)
            {
                Console.Write($"{message}: ");
                do
                {
                    var keyInfo = Console.ReadKey(intercept: true);
                    key = keyInfo.Key;

                    if (key == ConsoleKey.Backspace && password.Length > 0)
                    {
                        Console.Write("\b \b");
                        password = password[0..^1];
                    }
                    else if (!char.IsControl(keyInfo.KeyChar))
                    {
                        Console.Write("*");
                        password += keyInfo.KeyChar;
                    }
                } while (key != ConsoleKey.Enter);
                if (password.Length < 8)
                {
                    ColorPrinter.Print(ConsoleColor.Red, "Password must be at least 8 characters long!");
                    password = string.Empty;
                    continue;
                }
                Console.WriteLine();
                break;
            }
            return password;
        }
    }
}
