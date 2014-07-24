using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using PortableTrello.Client;

namespace PortableTrello.Tests
{
    public class PartiallyMockedTrelloClient : TrelloClient
    {
        public PartiallyMockedTrelloClient(string key, string secret, ITokenStore tokenStore) : base(key, secret, tokenStore)
        {
        }

        protected override Task<string> ExecuteRequest(string resource, IDictionary<string, string> parameters, HttpMethod method, HttpContent content = null)
        {
            return ExecuteRequestStub(resource, parameters, method, content);
        }

        public virtual Task<string> ExecuteRequestStub(string resource, IDictionary<string, string> parameters, HttpMethod method, HttpContent content = null)
        {
            throw new NotImplementedException();
        }
    }
}