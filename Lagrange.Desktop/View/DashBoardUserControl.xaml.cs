using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Lagrange.Desktop.Model;
using Lagrange.Desktop.ViewModel;

namespace Lagrange.Desktop.View
{
    /// <summary>
    /// Interaction logic for DashBoardUserControl.xaml
    /// </summary>
    public partial class DashBoardUserControl : UserControl
    {
        
        public DashBoardUserControl()
        {
            InitializeComponent();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
        }

        private void UserControl_Unloaded(object sender, RoutedEventArgs e)
        {
        }
        public async void Dispose()
        {
            await ((DashBoardUserControlViewModel)DataContext).DisposeAsync();
        }
    }
}
