using AgilityWall.Core.Infrastructure;
using AgilityWall.TrelloApi.Client;
using Autofac;

namespace AgilityWall.WinPhone.Infrastructure.Modules
{
    public class TrelloApiModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<TrelloClient>()
                .WithParameter("key", "96d8e8fa4f535fbff7f524fb210a54fc")
                .WithParameter("secret", "b298727fa2c794204f8eff59f47c4c488afca00a2012fcd1007cf100bb07ee7a")
                .AsSelf()
                .SingleInstance();

            builder.RegisterType<TrelloToken>();

            builder.RegisterType<ObjectTokenStore>()
                .AsImplementedInterfaces()
                .SingleInstance();
        }
    }
}