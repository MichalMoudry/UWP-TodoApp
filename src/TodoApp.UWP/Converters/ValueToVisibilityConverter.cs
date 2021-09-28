using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Data;

namespace TodoApp.Converters
{
    /// <summary>
    /// Converter for converting a value to visibility.
    /// </summary>
    public class ValueToVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if (value == null)
            {
                return Visibility.Collapsed;
            }
            else if (value != null)
            {
                return Visibility.Visible;
            }
            else if (value is string val)
            {
                return string.IsNullOrEmpty(val) ? Visibility.Collapsed : (object)Visibility.Visible;
            }
            else
            {
                return Visibility.Collapsed;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
