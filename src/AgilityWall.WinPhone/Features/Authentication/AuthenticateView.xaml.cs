﻿using System;
using System.Threading.Tasks;
using System.Windows.Navigation;
using AgilityWall.TrelloApi.Authentication;
using Microsoft.Phone.Controls;

namespace AgilityWall.WinPhone.Features.Authentication
{
    public partial class AuthenticateView : PhoneApplicationPage, IAuthenticationFrame
    {
        public event EventHandler<BrowserEventArgs> BrowserRedirected;
        public event EventHandler LoginCanceled;

        protected virtual void OnLoginCanceled()
        {
            EventHandler handler = LoginCanceled;
            if (handler != null) handler(this, EventArgs.Empty);
        }

        protected virtual void OnResponseRedirected(BrowserEventArgs e)
        {
            EventHandler<BrowserEventArgs> handler = BrowserRedirected;
            if (handler != null) handler(this, e);
        }

        private bool _isWaitingForResult;

        public AuthenticateView()
        {
            InitializeComponent();
            Browser.Navigated += BrowserOnNavigated;
        }

        private void BrowserOnNavigated(object sender, NavigationEventArgs navigationEventArgs)
        {
            var source = Browser.SaveToString();
            if (_isWaitingForResult && source != null)
                OnResponseRedirected(new BrowserEventArgs(Browser.Source.ToString(), source));
        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            base.OnNavigatedFrom(e);
            OnLoginCanceled();
        }

        public Task DisplayUri(Uri authorizationUrl)
        {
            Browser.Navigate(authorizationUrl);
            _isWaitingForResult = true;
            return Task.FromResult(true);
        }

    }
}