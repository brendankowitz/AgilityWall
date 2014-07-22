using System;
using Windows.UI.Xaml.Data;

namespace AgilityWall.WinStore.Infrastructure.Converters
{
    public class CardDateFormatter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            try
            {
                var date = System.Convert.ToDateTime(value);
                return date.ToLocalTime().ToString("M");
            }
            catch
            {
                return null;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}