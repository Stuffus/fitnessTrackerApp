//value converter that returns Visibility.Visible when a bound boolean is false, and Visibility.Collapsed when it's true
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace fitnessTrackerApp.Utilities
{
    public class InverseBoolConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is bool b)
                return b ? Visibility.Collapsed : Visibility.Visible;

            return Visibility.Visible;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
            => throw new NotImplementedException();
    }
}
