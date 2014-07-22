using AgilityWall.WinPhone.Infrastructure.PlatformServices;
using Autofac;

namespace AgilityWall.WinPhone.Infrastructure.Modules
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