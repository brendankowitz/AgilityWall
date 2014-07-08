using PortableTrello.Client.Requests;

namespace AgilityWall.Core.Messages
{
    public class ModelResponse<TRequest, TResponse> 
        where TRequest : ITrelloRequest<TRequest, TResponse>
    {
        public ModelResponse(string requestedResource, TResponse value)
        {
            RequestedResource = requestedResource;
            Value = value;
        }

        public string RequestedResource { get; private set; }
        public TResponse Value { get; private set; }
    }
}