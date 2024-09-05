using Lagrange.Desktop.ViewModel;
using System.Diagnostics;
using System.Management;

namespace Lagrange.Desktop.Model;

public class DeviceMonitor
{
    private CancellationTokenSource _monitorCancellationTokenSource = new();
    public static DeviceMonitor Instance { get; } = new DeviceMonitor();
    public string CpuModel { get; private set; }
    public double RamTotalSize { get; private set; }
    private DeviceMonitor()
    {
        CpuModel = GetCpuModel();
        RamTotalSize = GetRamSize();
    }

    public async Task InitMonitor(DashBoardUserControlViewModel viewModel)
    {
        var monitor = DeviceMonitor.Instance;
        var token = _monitorCancellationTokenSource.Token;

        viewModel.CpuModel = monitor.CpuModel;

        while (!token.IsCancellationRequested)
        {
            var data = monitor.GetData();

            viewModel.CpuLoad = data[monitor.CpuCounter];
            viewModel.RamModel = $"{data[monitor.RamCounter]:F0}/{monitor.RamTotalSize:F0} GB";
            viewModel.RamLoad = data[monitor.RamCounter] / monitor.RamTotalSize;
            viewModel.DiskLoad = data[monitor.DiskCounter];
            viewModel.ProcessCpuLoad = data[monitor.ProcessCpuCounter];
            viewModel.ProcessRamLoad = data[monitor.ProcessRamCounter];
            await Task.Delay(2000, token);
        }

        monitor.Dispose();
    }

    //Percentage of CPU usage
    public PerformanceCounter CpuCounter = new (
        "Processor Information",
        "% Processor Utility",
        "_Total",
        true
    );

    //Available memory(GB)
    public PerformanceCounter RamCounter = new (
        "Memory",
        "Available MBytes",
        true
    );


    //Percentage of disk time
    public PerformanceCounter DiskCounter = new (
        "PhysicalDisk",
        "% Disk Time",
        "_Total",
        true
    );

    //Memory usage of the current process(MB)
    public PerformanceCounter ProcessRamCounter = new (
        "Process",
        "Working Set",
        Process.GetCurrentProcess().ProcessName
    );

    //CPU usage of the current process
    public PerformanceCounter ProcessCpuCounter = new (
        "Process",
        "% Processor Time",
        Process.GetCurrentProcess().ProcessName
    );

    public Dictionary<PerformanceCounter, double> GetData() 
    {
        return new Dictionary<PerformanceCounter, double>
        {
            { CpuCounter, CpuCounter.NextValue() / 100 },
            { RamCounter, RamCounter.NextValue() / 1024 },
            { DiskCounter, DiskCounter.NextValue() / 100 },
            { ProcessRamCounter, ProcessRamCounter.NextValue() / 1024 / 1024 },
            { ProcessCpuCounter, ProcessCpuCounter.NextValue() }
        };
    }

    public string GetCpuModel()
    {
        var mos =
            new ManagementObjectSearcher("root\\CIMV2", "SELECT * FROM Win32_Processor");
        foreach (ManagementObject mo in mos.Get())
        {
            var name = (mo["Name"]);
            return name.ToString();
        }
        return "";
    }

    public double GetRamSize() {
        var mos = new ManagementObjectSearcher("root\\CIMV2", "SELECT * FROM Win32_ComputerSystem");
        foreach (ManagementObject mo in mos.Get())
        {
            var totalRam = Convert.ToInt64(mo["TotalPhysicalMemory"]);
            return totalRam / 1024 / 1024 / 1024;
        }
        return 1;
    }

    public void Dispose()
    {
        CpuCounter.Dispose();
        RamCounter.Dispose();
        DiskCounter.Dispose();
        ProcessRamCounter.Dispose();
        ProcessCpuCounter.Dispose();
        _monitorCancellationTokenSource.Cancel();
    }
}
