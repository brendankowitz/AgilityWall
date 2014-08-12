using System;
using System.Globalization;
using System.Windows.Data;

namespace AgilityWall.WinPhone.Infrastructure.Converters
{
    public class CardLabelConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var key = string.Format("CardLabel{0}", TitleCase(value as string));

            if (App.Current.Resources.Contains(key))
                return App.Current.Resources[key];

            return value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        string TitleCase(string input)
        {
            if (string.IsNullOrEmpty(input)) return input;
            return input[0].ToString().ToUpper() + input.Substring(1);
        }
    }
}