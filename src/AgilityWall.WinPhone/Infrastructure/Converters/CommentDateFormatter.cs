using System;
using System.Globalization;
using System.Windows.Data;

namespace AgilityWall.WinPhone.Infrastructure.Converters
{
    public class CommentDateFormatter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            try
            {
                var date = System.Convert.ToDateTime(value);
                var format = date.ToLocalTime().ToString("h:mmtt MMM d");
                if(date.Year != DateTime.Today.Year)
                    format += date.ToLocalTime().ToString(" yyyy");
                return format;
            }
            catch
            {
                return null;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}