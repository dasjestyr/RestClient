using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace RestClient
{
    public class RestClient : IUriBuilder
    {
        private readonly IUriBuilder _builder;

        /// <summary>
        /// Gets or sets the handler.
        /// </summary>
        /// <value>
        /// The handler.
        /// </value>
        public HttpMessageHandler Handler { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="RestClient"/> class.
        /// </summary>
        public RestClient()
            : this(new UriBuilder())
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="RestClient"/> class.
        /// </summary>
        /// <param name="builder">The builder.</param>
        public RestClient(IUriBuilder builder)
        {
            _builder = builder;
        }

        public async Task<HttpResponseMessage> Get()
        {
            using (var client = GetClient())
            {
                var request = new HttpRequestMessage(HttpMethod.Get, _builder.BuildUri());
                return await client.SendAsync(request);
            }
        }

        public async Task<HttpResponseMessage> Delete()
        {
            using (var client = GetClient())
            {
                var request = new HttpRequestMessage(HttpMethod.Delete, _builder.BuildUri());
                return await client.SendAsync(request);
            }
        }

        public async Task<HttpResponseMessage> Post()
        {
            using (var client = GetClient())
            {
                var request = new HttpRequestMessage(HttpMethod.Post, _builder.BuildUri());
                return await client.SendAsync(request);
            }
        }
        
        public async Task<HttpResponseMessage> Post(HttpContent content)
        {
            using (var client = GetClient())
            {
                var request = new HttpRequestMessage(HttpMethod.Post, _builder.BuildUri()) {Content = content};
                return await client.SendAsync(request);
            }
        }

        public async Task<HttpResponseMessage> Put()
        {
            using (var client = GetClient())
            {
                var request = new HttpRequestMessage(HttpMethod.Put, _builder.BuildUri());
                return await client.SendAsync(request);
            }
        }

        public async Task<HttpResponseMessage> Put(HttpContent content)
        {
            using (var client = GetClient())
            {
                var request = new HttpRequestMessage(HttpMethod.Put, _builder.BuildUri()) {Content = content};
                return await client.SendAsync(request);
            }
        }

        private HttpClient GetClient()
        {
            return Handler == null
                ? new HttpClient()
                : new HttpClient(Handler);
        }

        #region -- IUriBuilder Implementation --

        public IUriBuilder WithScheme(Scheme scheme)
        {
            return _builder.WithScheme(scheme);
        }

        public IUriBuilder WithHost(string host)
        {
            return _builder.WithHost(host);
        }

        public IUriBuilder WithPort(uint port)
        {
            return _builder.WithPort(port);
        }

        public IUriBuilder WithPath(string path)
        {
            return _builder.WithPath(path);
        }

        public IUriBuilder WithQueryParameters(object parameters)
        {
            return _builder.WithQueryParameters(parameters);
        }

        public IUriBuilder WithQueryParameters(IEnumerable<KeyValuePair<string, string>> parameters)
        {
            return _builder.WithQueryParameters(parameters);
        }

        public IUriBuilder WithSegmentPair(string name, string value)
        {
            return _builder.WithSegmentPair(name, value);
        }

        public Uri BuildUri()
        {
            return _builder.BuildUri();
        }

        #endregion
    }
}
