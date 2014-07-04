using System.Collections.Generic;

namespace AgilityWall.TrelloApi.Client.Requests
{
    // ReSharper disable UnusedTypeParameter
    public interface ITrelloRequest<TRequest, TResponse>
    {
        string Resource { get; }
        IDictionary<string, string> Parameters { get; }
    }
}