using AgilityWall.WinStore.Infrastructure.PlatformServices;
using Autofac;

namespace AgilityWall.WinStore.Infrastructure.Modules
{
    public class TileModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<TileService>()
                .SingleInstance()
                .AutoActivate();
        }
    }
}