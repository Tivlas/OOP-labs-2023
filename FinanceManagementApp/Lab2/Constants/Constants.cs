using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab2.Constants
{
    public static class Constants
    {
        public static string HelpInfo => 
            $"Enter {3,2} to add simple account\r\n" +
            $"Enter {4,2} to add deposit\r\n" +
            $"Enter {5,2} to add loan\r\n" +
            $"Enter {6,2} to add card\r\n" +
            $"Enter {7,2} to add transaction category\r\n" +
            $"Enter {8,2} to add simple transaction\r\n" +
            $"Enter {9,2} to add transfer\r\n" +
            "Enter 10 to remove category\r\n" +
            "Enter 11 to remove transaction\r\n" +
            "Enter 12 to remove account or card\r\n" +
            "Enter 13 to list ur accounts and cards";

        public static string Delimiter => "———————————————————————————————————————";
    }
}
