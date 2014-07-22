using System;
using System.Globalization;
using System.Windows.Data;

namespace AgilityWall.WinPhone.Infrastructure.Converters
{
    public class GravatarUriConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return string.Format("http://www.gravatar.com/avatar/{0}?size={1}", value, parameter ?? 56);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}