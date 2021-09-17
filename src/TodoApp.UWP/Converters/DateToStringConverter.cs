using System;
using Windows.UI.Xaml.Data;

namespace TodoApp.Converters
{
    public class DateToStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            DateTime date = (DateTime)value;
            string res = date.Equals(DateTime.MinValue) ? "None" : date.ToShortDateString();
            return res;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}