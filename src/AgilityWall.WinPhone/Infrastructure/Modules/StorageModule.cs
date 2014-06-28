using System.Linq;
using AgilityWall.Core.Infrastructure;
using AgilityWall.Core.PlatformServices;
using Autofac;
using Caliburn.Micro;

namespace AgilityWall.WinPhone.Infrastructure.Modules
{
    public class StorageModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterGeneric(typeof(ObjectStorageService<>))
                .AsImplementedInterfaces()
                .SingleInstance();
        }
    }
}