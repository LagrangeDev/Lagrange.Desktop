using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Threading;
using Lagrange.Desktop.Model;
using System.IO;

namespace Lagrange.Desktop.ViewModel;

public partial class MainWindowViewModel : ObservableObject
{
    [ObservableProperty]
    private StringWriter _consoleStringWriter;
    [ObservableProperty]
    private DashBoardUserControlViewModel _dashBoardUserControlViewModel;
    [ObservableProperty]
    private ConsoleUserControlViewModel _consoleUserControlViewModel;
    public DateTime StartTime { get; }
    public MainWindowViewModel()
    {
        _consoleStringWriter = new StringWriter();
        _dashBoardUserControlViewModel = new DashBoardUserControlViewModel(ConsoleStringWriter);
        _consoleUserControlViewModel = new ConsoleUserControlViewModel(ConsoleStringWriter);
        DeviceMonitor.Instance.InitMonitor(DashBoardUserControlViewModel);
        StartTime = DateTime.Now;
        _timer = new DispatcherTimer
        {
            Interval = TimeSpan.FromSeconds(2)
        };
        _timer.Tick += Timer_Tick;
        _timer.Start();
    }

    [ObservableProperty]
    private string _currentTime = "";

    [ObservableProperty]
    private string _runningTime = "0";

    private DispatcherTimer _timer;
    private void Timer_Tick(object sender, EventArgs e)
    {
        CurrentTime = DateTime.Now.ToString("HH:mm");
        RunningTime = (DateTime.Now - StartTime).Minutes.ToString(CultureInfo.InvariantCulture);
    }

    [ObservableProperty]
    private bool _isLagrangeRunning;
}