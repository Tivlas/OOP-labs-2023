using System.Globalization;

namespace FinanceManagementMAUI.ValueConverters;
public class LoginPagePasswordToColorConverter : IValueConverter
{
    private readonly Color _valid = Colors.Black;
    private readonly Color _invalid = Colors.Red;

    public LoginPagePasswordToColorConverter()
    {

    }

    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if ((bool)value)
        {
            return _valid;
        }
        return _invalid;
    }
    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) => throw new NotImplementedException();
}
