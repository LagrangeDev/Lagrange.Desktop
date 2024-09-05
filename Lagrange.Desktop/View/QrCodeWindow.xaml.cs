using System;
using System.Collections.Generic;
using System.IO;
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
using System.Windows.Shapes;
using Panuon.WPF.UI;

namespace Lagrange.Desktop.View
{
    /// <summary>
    /// Interaction logic for QrCodeWindow.xaml
    /// </summary>
    public partial class QrCodeWindow : WindowX
    {
        public string LoginUrl { get; set; }
        public string QrCodePath { get; set; }

        public QrCodeWindow(string loginUrl, string qrCodePath)
        {
            LoginUrl = loginUrl;
            QrCodePath = qrCodePath;
            InitializeComponent();

            LoadQrCodeImage(QrCodePath);
            LoginUrlTextBlock.Text = LoginUrl;
        }

        private void LoadQrCodeImage(string qrCodePath)
        {
            try
            {
                var currentDirectory = Directory.GetCurrentDirectory();
                var imagePath = System.IO.Path.Combine(currentDirectory, qrCodePath);

                var imageData = File.ReadAllBytes(imagePath);

                using (var memoryStream = new MemoryStream(imageData))
                {
                    var bitmap = new BitmapImage();
                    bitmap.BeginInit();
                    bitmap.CacheOption = BitmapCacheOption.OnLoad;
                    bitmap.StreamSource = memoryStream;
                    bitmap.EndInit();
                    bitmap.Freeze();
                    QrCodeImage.Source = bitmap;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Failed to load image: {ex.Message}");
            }
        }
    }
}
