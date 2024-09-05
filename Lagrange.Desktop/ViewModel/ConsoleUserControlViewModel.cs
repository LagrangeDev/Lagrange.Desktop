using CommunityToolkit.Mvvm.ComponentModel;
using Lagrange.Desktop.Model;
using System.IO;

namespace Lagrange.Desktop.ViewModel;

public class ConsoleUserControlViewModel : ObservableObject
{
    public ConsoleUserControlViewModel(StringWriter consoleStringWriter)
    {
        ConsoleStringWriter = consoleStringWriter;
    }
    public StringWriter ConsoleStringWriter { get; set; }
}