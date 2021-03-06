﻿using System;
using System.Collections.Generic;
using System.Linq;
using AgilityWall.Core.Navigation;
using Caliburn.Micro;

namespace AgilityWall.WinPhone.Infrastructure.PlatformServices
{
    public class NavigationWrapper : INavService
    {
        private readonly INavigationService _navigationService;

        public NavigationWrapper(INavigationService navigationService)
        {
            _navigationService = navigationService;
        }

        public bool CanGoBack { get { return _navigationService.CanGoBack; } }

        public bool Navigate<T>()
        {
            return Navigate<T>(null);
        }

        public bool Navigate<T>(object parameter)
        {
            var queryString = new Dictionary<string, string>();
            if (parameter != null)
            {
                foreach (var p in parameter.GetType().GetProperties())
                {
                    var value = p.GetValue(parameter);
                    if (value != null)
                        queryString.Add(p.Name, value.ToString());
                }
            }
            return Navigate<T>(queryString);
        }

        public bool Navigate<T>(IDictionary<string, string> parameters)
        {
            var queryString = new Dictionary<string, string>();
            if (parameters != null)
                queryString = new Dictionary<string, string>(parameters);
            return _navigationService.Navigate(BuildUri(typeof(T), queryString));
        }

        public void GoForward()
        {
            _navigationService.GoForward();
        }

        public void GoBack()
        {
            _navigationService.GoBack();
        }

        public Uri BuildUri(Type viewModelType, IDictionary<string, string> qry)
        {
            var type = ViewLocator.LocateTypeForModelType(viewModelType, null, null);
            if (type == null)
                throw new InvalidOperationException(string.Format("No view was found for {0}. See the log for searched views.", viewModelType.FullName));
            return new Uri(ViewLocator.DeterminePackUriFromType(viewModelType, type) + BuildQueryString(qry), UriKind.Relative);
        }

        private string BuildQueryString(IDictionary<string, string> qry)
        {
            if (qry.Count < 1)
                return string.Empty;
            string str = qry.Aggregate("?", (current, pair) => current + pair.Key + "=" + Uri.EscapeDataString(pair.Value) + "&");
            return str.Remove(str.Length - 1);
        }
    }
}