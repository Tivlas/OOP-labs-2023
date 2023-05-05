using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FinanceManagementMAUI.Models;

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
