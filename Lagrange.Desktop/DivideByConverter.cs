using System.Globalization;
using System.Windows.Data;

namespace Lagrange.Desktop;

public class DivideByConverter : IValueConverter
{
    public object Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        if (value is double width && parameter is string ratioString && double.TryParse(ratioString, out double ratio))
        {
            return width * ratio;
        }
        return 0;
    }

    public object ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}