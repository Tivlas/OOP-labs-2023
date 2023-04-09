using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab2.Helpers
{
    public static class ColorPrinter
    {
        public static void Print(ConsoleColor color, string message, bool endWithNewLine = true)
        {
            Console.ForegroundColor = color;
            if (endWithNewLine)
            {
                Console.WriteLine(message);
            }
            else
            {
                Console.Write(message);
            }
            Console.ResetColor();
        }
    }
}
