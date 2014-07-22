using System;
using System.Globalization;
using System.Windows.Data;

namespace AgilityWall.WinPhone.Infrastructure.Converters
{
    public class ToCapsConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var str = value as string;
            if (str != null)
            {
                return str.ToUpper();
            }
            return value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}