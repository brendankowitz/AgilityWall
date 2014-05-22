using System;
using System.Linq;
using System.Windows;
using AgilityWall.Core.Features.Main;
using Autofac;
using Caliburn.Micro;
using Caliburn.Micro.Autofac;

namespace AgilityWall.WinPhone.Infrastructure
{
    public class AutofacBootstrapper : AutofacBootstrapperBase
    {
        protected override System.Collections.Generic.IEnumerable<System.Reflection.Assembly> SelectAssemblies()
        {
            return base.SelectAssemblies().Concat(new[] { typeof(MainPageViewModel).Assembly });
        }

        protected override void ConfigureBootstrapper()
        {
            base.ConfigureBootstrapper();
            EnforceNamespaceConvention = false;
            EnableFastAppResumeSupport(new Uri("/Features/Main/MainPage.xaml", UriKind.Relative));
        }

        protected override void OnStartup(object sender, StartupEventArgs e)
        {
            base.OnStartup(sender, e);

            var config = new TypeMappingConfiguration
            {
                DefaultSubNamespaceForViewModels = "AgilityWall.Core.Features",
                DefaultSubNamespaceForViews = "AgilityWall.WinPhone.Features"
            };

            ViewLocator.ConfigureTypeMappings(config);
            ViewModelLocator.ConfigureTypeMappings(config);
        }
    }
}