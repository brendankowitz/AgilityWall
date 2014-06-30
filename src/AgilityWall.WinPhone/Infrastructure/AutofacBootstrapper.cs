using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using AgilityWall.Core.Features.Main;
using Autofac;
using Autofac.Features.OwnedInstances;
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

        public void EnableFastAppResumeSupport<T>() where T : INotifyPropertyChanged
        {
            var viewModelType = typeof(T);
            var type = ViewLocator.LocateTypeForModelType(viewModelType, null, null);
            if (type == null)
                throw new InvalidOperationException(string.Format("No view was found for {0}. See the log for searched views.", viewModelType.FullName));
            
            EnableFastAppResumeSupport(new Uri(ViewLocator.DeterminePackUriFromType(viewModelType, type), UriKind.Relative));
        }
    }
}