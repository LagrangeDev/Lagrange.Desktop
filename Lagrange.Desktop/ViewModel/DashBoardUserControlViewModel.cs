using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Lagrange.Desktop.Model;
using System.IO;
using System.Windows.Media.Imaging;

namespace Lagrange.Desktop.ViewModel;

public partial class DashBoardUserControlViewModel : ObservableObject
{
    public DashBoardUserControlViewModel(StringWriter consoleStringWriter)
    {
        ConsoleStringWriter = consoleStringWriter;
    }
    [ObservableProperty]
    private string _cpuModel = "AMD Ryzen 9 7945HX with Radeon Graphics";
    [ObservableProperty]
    private double _cpuLoad = 0.14;
    [ObservableProperty]
    public string _ramModel = "10/32 GB";
    [ObservableProperty]
    private double _ramLoad = 0.68;
    [ObservableProperty]
    private string _diskModel = "Disk Time";
    [ObservableProperty]
    private double _diskLoad = 0.92;
    [ObservableProperty]
    private double _processCpuLoad = 0.14;
    [ObservableProperty]
    private double _processRamLoad = 0.68;
    [ObservableProperty]
    private bool _isRunning = false;

    [ObservableProperty]
    private BitmapImage _botAvatar = new (new Uri("pack://application:,,,/Resources/Logo.png"));
    [ObservableProperty]
    private string _botName = "Lagrange";
    [ObservableProperty]
    private string _botUin = "NotLogin";

    public StringWriter ConsoleStringWriter { get; set; }

    private bool CanStart() => !IsRunning;
    private bool CanStop() => IsRunning;

    [RelayCommand(CanExecute = nameof(CanStart))]
    public async Task StartAsync()
    {
        IsRunning = true;
        StartCommand.NotifyCanExecuteChanged();
        StopCommand.NotifyCanExecuteChanged();
        await LagrangeAppStarter.Instance.StartAsync(ConsoleStringWriter);
    }

    [RelayCommand(CanExecute = nameof(CanStop))]
    public async Task StopAsync()
    {
        await LagrangeAppStarter.Instance.StopAsync();
        IsRunning = false;
        StartCommand.NotifyCanExecuteChanged();
        StopCommand.NotifyCanExecuteChanged();
    }
    public async Task DisposeAsync()
    {
        await StopAsync();
        ConsoleStringWriter.Dispose();
    }
}
