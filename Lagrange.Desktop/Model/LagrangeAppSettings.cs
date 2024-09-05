using System.Text.Json.Serialization;
using CommunityToolkit.Mvvm.ComponentModel;

namespace Lagrange.Desktop.Model;
public partial class LagrangeAppSettings : ObservableObject
{
    [ObservableProperty]
    [JsonPropertyName("Logging")]
    private Logging logging;

    [ObservableProperty]
    [JsonPropertyName("SignServerUrl")]
    private string signServerUrl = "";

    [ObservableProperty]
    [JsonPropertyName("SignProxyUrl")]
    private string signProxyUrl = "";

    [ObservableProperty]
    [JsonPropertyName("MusicSignServerUrl")]
    private string musicSignServerUrl = "";

    [ObservableProperty]
    [JsonPropertyName("Account")]
    private Account account;

    [ObservableProperty]
    [JsonPropertyName("Message")]
    private Message message;

    [ObservableProperty]
    [JsonPropertyName("QrCode")]
    private QrCode qrCode;

    [ObservableProperty]
    [JsonPropertyName("Implementations")]
    private List<Implementation> implementations;
}

public partial class Logging : ObservableObject
{
    [ObservableProperty]
    [JsonPropertyName("LogLevel")]
    private LogLevel logLevel;
}

public partial class LogLevel : ObservableObject
{
    [ObservableProperty]
    [JsonPropertyName("Default")]
    private string @default = "";

    [ObservableProperty]
    [JsonPropertyName("Microsoft")]
    private string microsoft = "";

    [ObservableProperty]
    [JsonPropertyName("Microsoft.Hosting.Lifetime")]
    private string microsoftHostingLifetime = "Information";
}

public partial class Account : ObservableObject
{
    [ObservableProperty]
    [JsonPropertyName("Uin")]
    private int uin;

    [ObservableProperty]
    [JsonPropertyName("Password")]
    private string password = "";

    [ObservableProperty]
    [JsonPropertyName("Protocol")]
    private string protocol = "";

    [ObservableProperty]
    [JsonPropertyName("AutoReconnect")]
    private bool autoReconnect;

    [ObservableProperty]
    [JsonPropertyName("GetOptimumServer")]
    private bool getOptimumServer;
}

public partial class Message : ObservableObject
{
    [ObservableProperty]
    [JsonPropertyName("IgnoreSelf")]
    private bool ignoreSelf;

    [ObservableProperty]
    [JsonPropertyName("StringPost")]
    private bool stringPost;
}

public partial class QrCode : ObservableObject
{
    [ObservableProperty]
    [JsonPropertyName("ConsoleCompatibilityMode")]
    private bool consoleCompatibilityMode;
}

public partial class Implementation : ObservableObject
{
    [ObservableProperty]
    [JsonPropertyName("Type")]
    private string type = "";

    [ObservableProperty]
    [JsonPropertyName("Host")]
    private string host = "";

    [ObservableProperty]
    [JsonPropertyName("Port")]
    private int port;

    [ObservableProperty]
    [JsonPropertyName("Suffix")]
    private string suffix = "";

    [ObservableProperty]
    [JsonPropertyName("ReconnectInterval")]
    private int reconnectInterval;

    [ObservableProperty]
    [JsonPropertyName("HeartBeatInterval")]
    private int heartBeatInterval;

    [ObservableProperty]
    [JsonPropertyName("AccessToken")]
    private string accessToken = "";
}
