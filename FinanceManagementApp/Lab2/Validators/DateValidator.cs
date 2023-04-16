using System.Globalization;

namespace Lab2.Validators;
public class DateValidator : IValidator
{
    public bool IsValid(string? dateStr)
    {
        DateTime date;
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
