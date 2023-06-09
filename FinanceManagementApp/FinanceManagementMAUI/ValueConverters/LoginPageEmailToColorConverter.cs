﻿using System.Globalization;

namespace FinanceManagementMAUI.ValueConverters;
public class LoginPageEmailToColorConverter : IValueConverter
{
    private readonly Color _valid = App.Current.PlatformAppTheme == AppTheme.Dark ? Colors.White : Colors.Black;
    private readonly Color _invalid = Colors.Red;

    public LoginPageEmailToColorConverter()
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
