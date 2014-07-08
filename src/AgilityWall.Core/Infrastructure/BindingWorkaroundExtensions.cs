using System.Reflection;

namespace AgilityWall.Core.Infrastructure
{
    public static class BindingWorkaroundExtensions
    {
        public static T EnsureModel<T>(object newItem) where T : class
        {
            if (newItem != null && Equals(newItem.GetType().Name, "PanoramaItem"))
            {
                var datacontextProperty = newItem.GetType().GetRuntimeProperty("DataContext");
                var datacontext = datacontextProperty.GetValue(newItem);
                return datacontext as T ?? newItem as T;
            }
            return newItem as T;
        }
    }
}