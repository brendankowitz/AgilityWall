using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace PortableTrello.Internal
{
    public abstract class BaseClient
    {
        public static bool AllowAutomaticDecompression = true;

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

        protected virtual Task<string> ExecuteRequest(string resource, IDictionary<string, string> parameters, HttpMethod method, string content = null)
        {
            HttpContent httpContent = null;
            if (!string.IsNullOrEmpty(content))
                httpContent = new StringContent(content);
            return ExecuteRequest(resource, parameters, method, httpContent);
        }

        protected virtual Task<string> ExecuteRequest(string resource, IDictionary<string, string> parameters, HttpMethod method, IDictionary<string, string> formUrlEncodedContent)
        {
            if (formUrlEncodedContent == null) throw new ArgumentNullException("formUrlEncodedContent");
            return ExecuteRequest(resource, parameters, method, new FormUrlEncodedContent(formUrlEncodedContent));
        }

        protected virtual async Task<string> ExecuteRequest(string resource, IDictionary<string, string> parameters, HttpMethod method, HttpContent content = null)
        {
            using (var client = CreateHttpClient())
            {
                var req = new HttpRequestMessage(method, BuildUri(resource, parameters));
                if (content != null)
                    req.Content = content;
                var response = await client.SendAsync(req);
                return await HandleResponseMessage(response);
            }
        }

        protected virtual HttpClient CreateHttpClient()
        {
            var handler = new HttpClientHandler();
            if (handler.SupportsAutomaticDecompression && AllowAutomaticDecompression)
            {
                try
                {
                    handler.AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate;
                }
                catch
                {
                    AllowAutomaticDecompression = false;
                }
            }
            return new HttpClient(handler);
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