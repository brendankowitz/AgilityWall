using System.Linq;
using System.Windows;
using AgilityWall.Core.Features.Main;
using AgilityWall.Core.Features.Shared;
using Autofac;
using Caliburn.Micro;
using Caliburn.Micro.Autofac;
using Microsoft.Phone.Controls;
using Telerik.Windows.Controls;

namespace AgilityWall.WinPhone.Infrastructure
{
    public class AutofacBootstrapper : AutofacBootstrapperBase
    {
        protected override System.Collections.Generic.IEnumerable<System.Reflection.Assembly> SelectAssemblies()
        {
            return base.SelectAssemblies().Concat(new[] { typeof(MainPageViewModel).Assembly });
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

            EnableFastAppResumeSupport<MainPageViewModel>();
        }

        protected override void ConfigureContainer(ContainerBuilder builder)
        {
            builder.RegisterType<NetworkErrorViewModel>()
                .AsSelf()
                .Named<NetworkErrorViewModel>("NetworkErrorViewModel")
                .SingleInstance();
        }

        protected override PhoneApplicationFrame CreatePhoneApplicationFrame()
        {
            return new RadPhoneApplicationFrame();
        }
    }
}