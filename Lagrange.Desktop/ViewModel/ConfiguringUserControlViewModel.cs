using CommunityToolkit.Mvvm.ComponentModel;
using Lagrange.Desktop.Model;

namespace Lagrange.Desktop.ViewModel;

public partial class ConfiguringUserControlViewModel : ObservableObject
{
    [ObservableProperty]
    private LagrangeAppSettings _lagrangeAppSettings;
}