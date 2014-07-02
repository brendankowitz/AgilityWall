﻿using System;

namespace AgilityWall.TrelloApi.Authentication
{
    public class BrowserEventArgs : EventArgs
    {
        public BrowserEventArgs(string uri, string htmlContent)
        {
            Uri = uri;
            HtmlContent = htmlContent;
        }

        public string Uri { get; set; }
        public string HtmlContent { get; set; }
    }
}