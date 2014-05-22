using System;
using System.Diagnostics;
using System.Windows;

namespace AgilityWall.WinPhone
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            UnhandledException += OnUnhandledException;
        }

        private void OnUnhandledException(object sender, ApplicationUnhandledExceptionEventArgs applicationUnhandledExceptionEventArgs)
        {
            if (Debugger.IsAttached)
            {
                Debugger.Break();
            }
        }
    }
}