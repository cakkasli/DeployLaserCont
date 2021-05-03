using System;
using System.Collections.Generic;
using System.Diagnostics;
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
using Squirrel;

namespace WpfSquirrelTest
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            this.Loaded += new RoutedEventHandler(MainWindow_Loaded);
        }

        private void ButtonTest_Click(object sender, RoutedEventArgs e)
        {

        }

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            AddVersionNumber();
            CheckForUpdates();
        }

        private async void CheckForUpdates()
        {
            try
            {
                using(var mgr = await UpdateManager.GitHubUpdateManager("github"))
                {
                    var release = await mgr.UpdateApp();
                }
            }

            catch(Exception e)
            {
                Debug.WriteLine("Failed to check updates: " + e.ToString());
            }
        }

        private void AddVersionNumber()
        {
            System.Reflection.Assembly assembly = System.Reflection.Assembly.GetExecutingAssembly();
            FileVersionInfo versionInfo = FileVersionInfo.GetVersionInfo(assembly.Location);

            Dispatcher.Invoke(() => {
                this.Title += $" v{ versionInfo.FileVersion }";
            });
        }

    }
}
