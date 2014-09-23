using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Reflection;
using Windows.ApplicationModel.Activation;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using AgilityWall.Core.Features.Main;
using AgilityWall.Core.Features.Shared;
using AgilityWall.Core.Messages;
using AgilityWall.WinStore.Features.CardDetails;
using AgilityWall.WinStore.Features.Main;
using Autofac;
using Autofac.Features.OwnedInstances;
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
            if (args.Exception is HttpRequestException || string.Equals(args.Message, "An error occurred while sending the request."))
            {
                using (var broadcast = IoC.Get<Owned<IEventAggregator>>())
                {
                    broadcast.Value.Publish(new NetworkFailure(args.Exception), Execute.BeginOnUIThread);
                    args.Handled = true;
                    return;
                }
            }

            if (Debugger.IsAttached)
                Debugger.Break();

            args.Handled = true;
            await new MessageDialog(args.Exception.ToString(), "Error").ShowAsync();
        }

        protected override IEnumerable<Assembly> SelectAssemblies()
        {
            return base.SelectAssemblies().Concat(new []{ typeof(MainPageViewModel).GetTypeInfo().Assembly });
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

            builder.RegisterType<CardDetailsViewModel>()
                .AsSelf()
                .As<AgilityWall.Core.Features.CardDetails.CardDetailsViewModel>();

            builder.RegisterType<NetworkErrorViewModel>()
                .AsSelf()
                .Named<NetworkErrorViewModel>("NetworkErrorViewModel")
                .SingleInstance();
        }

        protected override void OnLaunched(LaunchActivatedEventArgs args)
        {
            DisplayRootView<MainPage>();
        }
    }
}
