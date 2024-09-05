using System;
using System.Collections.Concurrent;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;
using Lagrange.Desktop.Model;
using Lagrange.Desktop.ViewModel;

namespace Lagrange.Desktop.View
{
    /// <summary>
    /// Interaction logic for ConsoleUserControl.xaml
    /// </summary>
    public partial class ConsoleUserControl : UserControl
    {
        private readonly DispatcherTimer _logUpdateTimer;
        private readonly ConcurrentQueue<string> _logQueue;

        public ConsoleUserControl()
        {
            InitializeComponent();
            _logQueue = new ConcurrentQueue<string>();
            _logUpdateTimer = new DispatcherTimer
            {
                Interval = TimeSpan.FromMilliseconds(200) // Update interval to 200ms
            };
            _logUpdateTimer.Tick += OnLogUpdateTimerTick;
            _logUpdateTimer.Start();
        }

        private void OnLogUpdateTimerTick(object sender, EventArgs e)
        {
            var stringWriter = ((ConsoleUserControlViewModel)DataContext)?.ConsoleStringWriter;
            if (stringWriter != null)
            {
                var log = stringWriter.ToString();
                if (!string.IsNullOrEmpty(log))
                {
                    var logs = log.Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);
                    foreach (var l in logs)
                    {
                        stringWriter.GetStringBuilder().Clear();
                        _logQueue.Enqueue(l);
                    }
                }
            }

            while (_logQueue.TryDequeue(out var log))
            {
                ProcessLog(log);
            }
        }

        public void AppendText(string header, Brush headerColor, string message, Brush messageColor)
        {
            ConsoleRichTextBox.Dispatcher.Invoke(() =>
            {
                var paragraph = new Paragraph() 
                {
                    LineHeight = 5,
                };
                var headerRun = new Run(header) { Foreground = headerColor };
                var messageRun = new Run(message) { Foreground = messageColor };

                paragraph.Inlines.Add(headerRun);
                paragraph.Inlines.Add(messageRun);

                ConsoleRichTextBox.Document.Blocks.Add(paragraph);
                ConsoleRichTextBox.ScrollToEnd();
                if (ConsoleRichTextBox.Document.Blocks.Count > 300)
                {
                    ConsoleRichTextBox.Document.Blocks.Remove(ConsoleRichTextBox.Document.Blocks.FirstBlock);
                }
            });
        }

        public void ProcessLog(string log)
        {
            if (log.StartsWith("Error: "))
            {
                AppendText("Error: ", Brushes.Red, log.Substring(7), Brushes.White);
            }
            else if (log.StartsWith("Warning: "))
            {
                AppendText("Warning: ", Brushes.Orange, log.Substring(13), Brushes.White);
            }
            else if (log.StartsWith("Information: "))
            {
                if (log.StartsWith("Information: Please scan the QR code above, Url: ")) 
                {
                    if (Application.Current.MainWindow is MainWindow mainWindow)
                    {
                        mainWindow.OnNeedScanQrCode(log.Substring(49));
                    }
                }
                AppendText("Information: ", Brushes.Green, log.Substring(13), Brushes.White);
            }
            else if (log.StartsWith("Trace: "))
            {
                AppendText("Trace: ", Brushes.Gray, log.Substring(7), Brushes.White);
            }
            else
            {
                AppendText(string.Empty, Brushes.White, log, Brushes.White);
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            ConsoleRichTextBox.Document.Blocks.Clear();
        }
    }
}
