using System.Globalization;
using System.Windows.Data;

namespace Lagrange.Desktop;

public class RunStateToColorConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        return (bool)value ? "#FF00FF00" : "#FFFF0000";
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}