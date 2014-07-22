namespace AgilityWall.Core.Navigation
{
    public static class NavigationServiceExtensions
    {
        public static NavigationBuilder<T> ForView<T>(this INavService navService) where T : class
        {
            return new NavigationBuilder<T>(navService);
        }
    }
}