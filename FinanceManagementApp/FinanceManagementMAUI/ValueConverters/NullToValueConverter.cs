﻿using System.Globalization;
using Domain.Entities.Accounts;

namespace FinanceManagementMAUI.ValueConverters;
public class NullToValueConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is null)
        {
            return parameter;
        }
        else
        {
            return (value as SimpleAccount).Name;
        }
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotSupportedException();
    }
}
