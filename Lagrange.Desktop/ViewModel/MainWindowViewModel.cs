using CommunityToolkit.Mvvm.ComponentModel;
using System.Globalization;
using System.Windows.Data;

namespace Lagrange.Desktop.ViewModel;

public class MainWindowViewModel : ObservableObject
{
    public class DivideByFourConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is double width)
            {
                return width / 4;
            }
            return 0;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}