using System.Windows;
using System.Windows.Data;
using Lagrange.Desktop.ViewModel;
using Panuon.WPF.UI;

namespace Lagrange.Desktop.View
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : WindowX
    {
        public MainWindow()
        {
            InitializeComponent();
            DataContext = new MainWindowViewModel();
            SetWindowXCaptionBinding();
        }
        private void SetWindowXCaptionBinding()
        {
            var binding = new Binding
            {
                Path = new PropertyPath("ActualHeightProperty"),
                RelativeSource = new RelativeSource(RelativeSourceMode.Self),
                Converter = new DivideByConverter(),
                ConverterParameter = 0.3
            };

            SetBinding(WindowXCaption.HeightProperty, binding);
        }
    }
}