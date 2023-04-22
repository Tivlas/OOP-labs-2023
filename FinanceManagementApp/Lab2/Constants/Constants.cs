namespace Lab2.Const
{
    public static class Constants
    {
        public static readonly string HelpInfo =
            $"Enter {0,3} to quit program\r\n" +
            $"Enter {-1,3} to logout\r\n" +
            $"Enter {3,3} to add simple account\r\n" +
            $"Enter {33,3} to remove account\r\n" +
            $"Enter {333,3} to print ur accounts\r\n" +
            $"Enter {4,3} to add transaction category\r\n" +
            $"Enter {44,3} to remove category\r\n" +
            $"Enter {444,3} to print ur transaction categories\r\n" +
            $"Enter {5,3} to add simple transaction\r\n" +
            $"Enter {55,3} to remove transaction\r\n" +
            $"Enter {555,3} to print ur transactions\r\n" +
            $"Enter {7,3} to print ur transactions grouped by date\r\n" +
            $"Enter {8,3} to print ur transactions statistics by category";

        public static string Delimiter => "———————————————————————————————————————";

        public const string Cancel = "CANCEL";
    }
}
