using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab2.Validators;
public static class DateValidator
{
    public static bool IsValid(string? dateStr, out DateTime date)
    {
        if (DateTime.TryParseExact(dateStr, "dd.MM.yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out date))
        {
            if (date.Year == DateTime.Now.Year)
            {
                return true;
            }
        }
        return false;
    }
}
