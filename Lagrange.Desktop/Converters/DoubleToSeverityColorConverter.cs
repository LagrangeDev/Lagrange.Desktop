using System.Globalization;
using System.Windows.Data;

namespace Lagrange.Desktop;

public class DoubleToSeverityColorConverter : IValueConverter
{
    public object Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        if (value is double severity and not (< 0 or > 1) )
        {
            return severity switch
            {
                >= 0.9 => "#FF0033",
                >= 0.6 => "#FFFF00",
                >= 0.3 => "#99CCFF",
                _ => "#20A53A"
            };
        }
        throw new ArgumentException("Value must be a double more than 0 and less than 1!");
    }

    public object ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}