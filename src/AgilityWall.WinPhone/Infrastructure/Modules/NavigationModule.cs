using AgilityWall.Core.PlatformServices;
using Autofac;

namespace AgilityWall.WinPhone.Infrastructure.Modules
{
    public class NavigationModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<NavigationWrapper>()
                .AsImplementedInterfaces()
                .SingleInstance();
        }
    }
}