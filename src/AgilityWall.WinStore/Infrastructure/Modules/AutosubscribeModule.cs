using Autofac;
using Autofac.Core;
using Caliburn.Micro;

namespace AgilityWall.WinStore.Infrastructure.Modules
{
    public class AutosubscribeModule : Autofac.Module
    {
        protected override void AttachToComponentRegistration(IComponentRegistry registry, IComponentRegistration registration)
        {
            registration.Activated += OnComponentActivated;
        }

        static void OnComponentActivated(object sender, ActivatedEventArgs<object> e)
        {
            if (e == null)
                return;

            var handler = e.Instance as IHandle;
            if (handler == null)
                return;
            var bus = e.Context.Resolve<IEventAggregator>();
            bus.Subscribe(handler);
        }
    }
}