﻿using Squirrel;
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

namespace SquirrelTest
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            AddVersionNumber();
            CheckForUpdates();
        }

        private async Task CheckForUpdates()
        {
            try
            {
                UpdateManager updateManager;
                using (var mgr = await UpdateManager.GitHubUpdateManager("https://github.com/PaulMirve/SquirrelHelloWorld"))
                {
                    updateManager = mgr;
                    var release = await mgr.UpdateApp();
                }
            }
            catch (Exception ex)
            {
                string message = ex.Message + Environment.NewLine;
                if (ex.InnerException != null)
                    message += ex.InnerException.Message;
                MessageBox.Show(message);
            }
        }

        private void AddVersionNumber()
        {
            System.Reflection.Assembly assembly = System.Reflection.Assembly.GetExecutingAssembly();
            FileVersionInfo versionInfo = FileVersionInfo.GetVersionInfo(assembly.Location);
            GreetingMessage.Text = "This application is currently in its " + versionInfo.FileVersion + " version.";
            this.Title = $"v.{versionInfo.FileVersion}";
        }
    }
}
