using AgilityWall.WinStore.Infrastructure.PlatformServices;
using Autofac;

namespace AgilityWall.WinStore.Infrastructure.Modules
{
    public class NavigationModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<NavigationWrapper>()
                .AsSelf()
                .AsImplementedInterfaces()
                .SingleInstance();
        }
    }
}