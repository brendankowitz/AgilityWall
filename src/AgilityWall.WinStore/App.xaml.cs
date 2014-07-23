using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using Windows.ApplicationModel.Activation;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using AgilityWall.Core.Features.Main;
using AgilityWall.WinStore.Features.Main;
using Autofac;
using Caliburn.Micro;

namespace AgilityWall.WinStore
{
    /// <summary>
    /// Provides application-specific behavior to supplement the default Application class.
    /// </summary>
    public sealed partial class App
    {
        public App()
        {
            InitializeComponent();
        }

        protected async override void OnUnhandledException(object sender, UnhandledExceptionEventArgs args)
        {
            if (Debugger.IsAttached)
                Debugger.Break();
            args.Handled = true;
            await new MessageDialog(args.Exception.ToString(), "Error").ShowAsync();
        }

        protected override IEnumerable<Assembly> SelectAssemblies()
        {
            return base.SelectAssemblies().Concat(new []{ typeof(MainPageViewModel).GetTypeInfo().Assembly });
        }

        protected override void Configure()
        {
            try
            {
                base.Configure();
            }
            catch
            {
                if(Debugger.IsAttached) Debugger.Break();
            }
        }

        public override void HandleConfigure(ContainerBuilder builder)
        {
            var config = new TypeMappingConfiguration
            {
                DefaultSubNamespaceForViewModels = "AgilityWall.Core.Features",
                DefaultSubNamespaceForViews = "AgilityWall.WinStore.Features"
            };

            ViewLocator.ConfigureTypeMappings(config);
            ViewModelLocator.ConfigureTypeMappings(config);
        }

        protected override void OnLaunched(LaunchActivatedEventArgs args)
        {
            DisplayRootView<MainPage>();
        }
    }
}
