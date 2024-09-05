using System.IO;
using System.Reflection;
using System.Runtime;
using System.Text;
using System.Windows;
using System.Windows.Documents;
using System.Windows.Navigation;
using System.Windows.Threading;
using CommunityToolkit.Mvvm.ComponentModel;
using Lagrange.Core.Common.Interface.Api;
using Lagrange.Core.Event.EventArg;
using Lagrange.Desktop.View;
using Lagrange.OneBot;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Lagrange.Desktop.Model;

public class LagrangeAppStarter
{
    public static LagrangeAppStarter Instance { get; set; } = new();
    public static LagrangeApp? LagrangeApp { get; set; }

    private static bool _isLagrangeRunning;
    public static bool IsLagrangeRunning
    {
        get { return _isLagrangeRunning; }
        set
        {
            _isLagrangeRunning = value;
            if (Application.Current.MainWindow is MainWindow mainWindow)
            {
                mainWindow.OnLagrangeRunning();
            }
        }
    }

    public async Task StartAsync(StringWriter stringWriter)
    {
        GCSettings.LatencyMode = GCLatencyMode.Batch;

        if (!File.Exists("appsettings.json")) { }

        var hostBuilder = new LagrangeAppBuilder(new string[] { })
            .ConfigureConfiguration("appsettings.json", false, true)
            .ConfigureBots()
            .ConfigureOneBot()
            .ConfigureLogging(logging =>
            {
                logging.ClearProviders();
                logging.AddProvider(new StringWriterLoggerProvider(stringWriter));
            });

        LagrangeApp = hostBuilder.Build();
        IsLagrangeRunning = true;
        LagrangeApp.StartAsync();
        LagrangeApp.Instance.Invoker.OnBotOnlineEvent += (_, args) => OnBotOnline(args);
    }

    public async Task StopAsync()
    {
        IsLagrangeRunning = false;
        if (LagrangeApp != null)
        {
            await LagrangeApp.StopAsync();
            LagrangeApp.Dispose();
        }
    }

    private async void OnBotOnline(BotOnlineEvent arg)
    {
        if (arg.Reason == BotOnlineEvent.OnlineReason.Reconnect || LagrangeApp == null)
            return;

        var uin = LagrangeApp.Instance.BotUin;
        var botInfo = await LagrangeApp.Instance.FetchUserInfo(uin);

        if (botInfo != null)
        {
            Application.Current.Dispatcher.Invoke(() =>
            {
                if (Application.Current.MainWindow is MainWindow mainWindow)
                {
                    mainWindow.OnBotOnline(
                        (botInfo.Avatar, botInfo.Nickname, botInfo.Uin.ToString())
                    );
                }
            });
        }
    }
}
