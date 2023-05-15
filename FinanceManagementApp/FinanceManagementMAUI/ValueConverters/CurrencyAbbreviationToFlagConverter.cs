using System.Globalization;

namespace FinanceManagementMAUI.ValueConverters;
public class CurrencyAbbreviationToFlagConverter : IValueConverter
{

    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        string country = (value as string)![..2];
        var flag = $"{string.Concat(country.ToUpper().Select(x => char.ConvertFromUtf32(x + 0x1F1A5)))} ({(value as string)!})";
        return flag;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}
