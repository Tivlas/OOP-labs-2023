namespace Lab2.Const
{
    public static class Constants
    {
        public static readonly string HelpInfo =
            $"Enter {3,2} to add simple account\r\n" + //done
            $"Enter {4,2} to add deposit\r\n" + //----------------------------
            $"Enter {5,2} to add loan\r\n" + //----------------------------
            $"Enter {6,2} to add card\r\n" + //done
            $"Enter {7,2} to add transaction category\r\n" + //done
            $"Enter {8,2} to add simple transaction\r\n" + //----------------------------
            $"Enter {9,2} to add transfer\r\n" +//----------------------------
            "Enter 10 to remove category\r\n" + //done
            "Enter 11 to remove transaction\r\n" + //done
            "Enter 12 to remove account or card\r\n" + //done
            "Enter 13 to list ur accounts and cards\r\n" + //done
            "Enter 14 to list ur transaction categories\r\n" +
            "Enter 15 to list ur transactions"; //done

        public static string Delimiter => "———————————————————————————————————————";

        public const string Cancel = "CANCEL";
    }
}
