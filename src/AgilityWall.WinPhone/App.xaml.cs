﻿using System.Diagnostics;
using System.Net.Http;
using System.Windows;
using AgilityWall.Core.Messages;
using Autofac.Features.OwnedInstances;
using Caliburn.Micro;
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
            if (args.ExceptionObject is HttpRequestException)
            {
                using (var broadcast = IoC.Get<Owned<IEventAggregator>>())
                {
                    broadcast.Value.Publish(new NetworkFailure(args.ExceptionObject), Execute.BeginOnUIThread);
                    args.Handled = true;
                    return;
                }
            }

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