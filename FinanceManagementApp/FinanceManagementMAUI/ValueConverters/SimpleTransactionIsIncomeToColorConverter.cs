using System.Globalization;

namespace FinanceManagementMAUI.ValueConverters
{
    class SimpleTransactionIsIncomeToColorConverter : IValueConverter
    {
        private readonly Color _income = Colors.Green;
        private readonly Color _expense = Colors.Red;
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if ((bool)value)
            {
                return _income;
            }
            return _expense;
        }
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) => throw new NotImplementedException();
    }
}
