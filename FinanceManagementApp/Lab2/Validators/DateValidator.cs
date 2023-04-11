using System.Globalization;

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
