using System.Windows;
using System.Windows.Data;
using Lagrange.Core.Common.Entity;
using Lagrange.Desktop.Model;
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

        public void OnLagrangeRunning()
        {
            ((MainWindowViewModel)DataContext).IsLagrangeRunning =
                LagrangeAppStarter.IsLagrangeRunning;
        }

        public void OnBotOnline((string Avatar, string Nickname, string Uin) botSelfUserInfo)
        {
            if (DashBoard.DataContext is DashBoardUserControlViewModel viewModel)
            {
                viewModel.BotAvatar = new System.Windows.Media.Imaging.BitmapImage(
                    new Uri(botSelfUserInfo.Avatar)
                );
                viewModel.BotName = botSelfUserInfo.Nickname;
                viewModel.BotUin = botSelfUserInfo.Uin;
            }
        }

        public void OnNeedScanQrCode(string url)
        {
            var qrCodeWindow = new QrCodeWindow(
                url,
                $"qr-{LagrangeAppStarter.LagrangeApp?.Instance.BotUin}.png"
            );
            qrCodeWindow.Show();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e) { }

        private void Window_Closed(object sender, EventArgs e)
        {
            Configuring.SaveAppSettings();
            DeviceMonitor.Instance.Dispose();
            DashBoard.Dispose();
        }
    }
}
