using System;
using System.Diagnostics;
using System.Windows;
using Microsoft.Phone.Controls;

namespace AgilityWall.WinPhone
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
            UnhandledException += OnUnhandledException;
            ThemeManager.OverrideOptions = ThemeManagerOverrideOptions.None;
            ThemeManager.ToLightTheme();
        }

        private void OnUnhandledException(object sender, ApplicationUnhandledExceptionEventArgs args)
        {
            if (Debugger.IsAttached)
            {
                Debugger.Break();
            }
            else
            {
                MessageBox.Show(args.ExceptionObject.ToString());
            }
        }
    }
}