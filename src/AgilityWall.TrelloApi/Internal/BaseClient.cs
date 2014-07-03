using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace AgilityWall.TrelloApi.Internal
{
    public abstract class BaseClient
    {
        protected abstract string BaseUrl { get; }

        protected virtual async Task<T> ExecuteRequest<T>(string resource, IDictionary<string, string> parameters)
        {
            return await ExecuteRequest<T>(resource, parameters, HttpMethod.Get);
        }

        protected virtual async Task<T> ExecuteRequest<T>(string resource, IDictionary<string, string> parameters, HttpMethod method, string content = null)
        {
            var response = await ExecuteRequest(resource, parameters, method, content);
            return JsonConvert.DeserializeObject<T>(response);
        }

        protected virtual async Task<T> ExecuteRequest<T>(string resource, IDictionary<string, string> parameters, HttpMethod method, IDictionary<string, string> formUrlEncodedContent)
        {
            var response = await ExecuteRequest(resource, parameters, method, formUrlEncodedContent);
            return JsonConvert.DeserializeObject<T>(response);
        }

        protected virtual async Task<string> ExecuteRequest(string resource, IDictionary<string, string> parameters)
        {
            return await ExecuteRequest(resource, parameters, HttpMethod.Get);
        }

        protected virtual async Task<string> ExecuteRequest(string resource, IDictionary<string, string> parameters, HttpMethod method, string content = null)
        {
            using (var client = CreateHttpClient())
            {
                var req = new HttpRequestMessage(method, BuildUri(resource, parameters));
                if(!string.IsNullOrEmpty(content))
                    req.Content = new StringContent(content);
                var response = await client.SendAsync(req);
                return await HandleResponseMessage(response);
            }
        }

        protected virtual async Task<string> ExecuteRequest(string resource, IDictionary<string, string> parameters, HttpMethod method, IDictionary<string, string> formUrlEncodedContent)
        {
            if (formUrlEncodedContent == null) throw new ArgumentNullException("formUrlEncodedContent");

            using (var client = CreateHttpClient())
            {
                var req = new HttpRequestMessage(method, BuildUri(resource, parameters));
                req.Content = new FormUrlEncodedContent(formUrlEncodedContent);
                var response = await client.SendAsync(req);
                return await HandleResponseMessage(response);
            }
        }

        protected virtual async Task Execute(string resource, IDictionary<string, string> parameters)
        {
            using (var client = CreateHttpClient())
            {
                await client.GetAsync(BuildUri(resource, parameters));
            }
        }

        protected virtual HttpClient CreateHttpClient()
        {
            return new HttpClient();
        }

        protected virtual Task<string> HandleResponseMessage(HttpResponseMessage message)
        {
            return message.Content.ReadAsStringAsync();
        }

        protected virtual string BuildUri(string resource, IDictionary<string, string> parameters)
        {
            var uriBuilder = new StringBuilder(string.Format("{0}{1}", BaseUrl, resource));

            if (parameters.Any())
            {
                uriBuilder.Append("?");
                uriBuilder.Append(string.Join("&", parameters.Select(x => string.Format("{0}={1}", x.Key, x.Value))));
            }

            return uriBuilder.ToString();
        }
    }
}