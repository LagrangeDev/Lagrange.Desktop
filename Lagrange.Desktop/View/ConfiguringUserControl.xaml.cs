using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
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
using Lagrange.Desktop.Model;
using Lagrange.Desktop.ViewModel;

namespace Lagrange.Desktop.View
{
    /// <summary>
    /// Interaction logic for ConfiguringUserControl.xaml
    /// </summary>
    public partial class ConfiguringUserControl : UserControl
    {
        public ConfiguringUserControl()
        {
            InitializeComponent();
            DataContext = new ConfiguringUserControlViewModel();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            InitializeAppSettings();
        }

        private void UserControl_Unloaded(object sender, RoutedEventArgs e)
        {
            SaveAppSettings();
        }

        private void InitializeAppSettings()
        {
            if (!File.Exists("appsettings.json"))
            {
                CreateAppSettings();
            }
            var dataContext = (ConfiguringUserControlViewModel)DataContext;
            var appSettings = JsonSerializer.Deserialize<LagrangeAppSettings>(
                File.ReadAllText("appsettings.json")
            );
            if (appSettings == null)
            {
                CreateAppSettings();
                appSettings = JsonSerializer.Deserialize<LagrangeAppSettings>(
                    File.ReadAllText("appsettings.json"),
                    new JsonSerializerOptions
                    {
                        DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingDefault,
                        PropertyNameCaseInsensitive = true }
                );
            }
            dataContext.LagrangeAppSettings = appSettings;
        }

        private void CreateAppSettings()
        {
            var assembly = Assembly.GetAssembly(typeof(Lagrange.OneBot.LagrangeApp));
            using var manifestResourceStream = assembly.GetManifestResourceStream(
                "Lagrange.OneBot.Resources.appsettings.json"
            )!;
            using var fileStream = File.Create("appsettings.json");
            manifestResourceStream.CopyTo(fileStream);
            manifestResourceStream.Close();
            fileStream.Close();
        }

        public void SaveAppSettings()
        {
            var dataContext = (ConfiguringUserControlViewModel)DataContext;
            var appSettings = dataContext.LagrangeAppSettings;

            var jsonString = JsonSerializer.Serialize(
                appSettings,
                new JsonSerializerOptions
                {
                    DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingDefault,
                    WriteIndented = true,
                    PropertyNameCaseInsensitive = true
                }
            );
            File.WriteAllText("appsettings.json", jsonString);
        }
    }
}
