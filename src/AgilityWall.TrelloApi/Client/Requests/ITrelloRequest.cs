using System.Collections.Generic;

namespace PortableTrello.Client.Requests
{
    // ReSharper disable UnusedTypeParameter
    public interface ITrelloRequest<TRequest, TResponse>
    {
        string Resource { get; }
        IDictionary<string, string> Parameters { get; }
    }
}