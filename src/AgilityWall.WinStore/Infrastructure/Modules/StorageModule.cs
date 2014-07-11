using Windows.Storage;
using AgilityWall.WinStore.Infrastructure.PlatformServices;
using Autofac;

namespace AgilityWall.WinStore.Infrastructure.Modules
{
    public class StorageModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterGeneric(typeof(ObjectStorageService<>))
                .WithParameter(new TypedParameter(typeof(StorageFolder), ApplicationData.Current.LocalFolder))
                .AsImplementedInterfaces()
                .SingleInstance();
        }
    }
}