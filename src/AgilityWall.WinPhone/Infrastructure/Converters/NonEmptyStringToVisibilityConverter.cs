using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace AgilityWall.WinPhone.Infrastructure.Converters
{
    public class NonEmptyStringToVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var str = value as string;

            if (string.IsNullOrWhiteSpace(str) || string.IsNullOrEmpty(str))
                return Visibility.Collapsed;
            return Visibility.Visible;

        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}