using System;
using System.Collections.Generic;
using System.Deployment.Application;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
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

namespace KoC_Launcher
{
    /// <summary>
    /// Interaktionslogik für MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        public string serverAddress = "";
        public string username = "";
        public string gamepath = "";
        public string password = "";

        public MainWindow()
        {
            InitializeComponent();
        }

        private void ExitButton_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            ProcessStartInfo processStartInfo = new ProcessStartInfo();
            processStartInfo.WorkingDirectory = GamePathField.Text;
            processStartInfo.FileName = @"KnockoutCity.exe";
            processStartInfo.Arguments = "-backend=" + ServerAddressField.Text + " -username=" + UsernameField.Text + " -secret=" + PasswordField.Password;
            Process.Start(processStartInfo);

            // Save Settings
            Properties.Settings.Default.ServerAddress = ServerAddressField.Text;
            Properties.Settings.Default.Username = UsernameField.Text;
            Properties.Settings.Default.GamePath = GamePathField.Text;
            Properties.Settings.Default.Password = PasswordField.Password;
            Properties.Settings.Default.Save();

            Application.Current.Shutdown();
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if(e.LeftButton == MouseButtonState.Pressed)
            {
                DragMove();
            }
        }

        private void Window_Initialized(object sender, EventArgs e)
        {
            serverAddress = Properties.Settings.Default.ServerAddress;
            username = Properties.Settings.Default.Username;
            gamepath = Properties.Settings.Default.GamePath;
            password = Properties.Settings.Default.Password;

            ServerAddressField.Text = serverAddress;
            UsernameField.Text = username;
            GamePathField.Text = gamepath;
            PasswordField.Password = password;
            try
            {
                //// get deployment version
                VersionText.Text = "Version: "+ApplicationDeployment.CurrentDeployment.CurrentVersion.ToString();
            }
            catch (InvalidDeploymentException)
            {
                //// you cannot read publish version when app isn't installed 
                //// (e.g. during debug)
                VersionText.Text = "Version: 1.0.0.0";
            }
        }
    }
}
