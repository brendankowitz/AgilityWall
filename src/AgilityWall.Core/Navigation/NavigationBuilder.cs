using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Caliburn.Micro;

namespace AgilityWall.Core.Navigation
{
    public class NavigationBuilder<T> where T : class
    {
        private readonly INavService _navService;
        readonly Dictionary<string, string> _queryString = new Dictionary<string, string>();

        public NavigationBuilder(INavService navService)
        {
            _navService = navService;
        }

        public NavigationBuilder<T> WithParam<TValue>(Expression<Func<T, TValue>> property, TValue value)
        {
            if (value is ValueType || !ReferenceEquals(null, value))
                _queryString[property.GetMemberInfo().Name] = value.ToString();
            return this;
        }

        public void Navigate()
        {
            object obj = _queryString.Any() ? _queryString.ToExpando() : null;
            _navService.Navigate<T>(obj);
        }
    }
}