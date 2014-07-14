using System;
using Windows.UI.Xaml.Data;
using AgilityWall.WinStore.Controls;

namespace AgilityWall.WinStore.Infrastructure.Converters
{
    public class IsLoadingToPageStateConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if (value == null) return PageStates.Normal;
            try
            {
                return System.Convert.ToBoolean(value) ? PageStates.Busy : PageStates.Normal;
            }
            catch
            {
                return PageStates.Normal;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}