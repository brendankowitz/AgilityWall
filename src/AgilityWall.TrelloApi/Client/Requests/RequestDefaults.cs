using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace AgilityWall.TrelloApi.Client.Requests
{
    public static class RequestDefaults
    {
         public static readonly IDictionary<string,string> EmptyDictionary = new ReadOnlyDictionary<string, string>(new Dictionary<string, string>());
    }
}