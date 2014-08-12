using System;
using System.Globalization;
using Windows.UI.Xaml.Data;

namespace AgilityWall.WinStore.Infrastructure.Converters
{
    public class CardLabelConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            var key = string.Format("CardLabel{0}", TitleCase(value as string));

            if (App.Current.Resources.ContainsKey(key))
                return App.Current.Resources[key];

            return value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
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