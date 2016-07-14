using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Provausio.Rest.Client.Infrastructure;

namespace Provausio.Rest.Client
{
    internal class UriBuilder : IUriBuilder
    {
        private readonly List<KeyValuePair<string, string>> _segments = new List<KeyValuePair<string, string>>();
        private readonly QueryParameterCollection _queryParameters = new QueryParameterCollection();

        private Scheme _scheme;
        private string _host;
        private uint? _port;
        private string _path;

        /// <summary>
        /// Initializes a new instance of the <see cref="UriBuilder"/> class.
        /// </summary>
        public UriBuilder()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="UriBuilder"/> class.
        /// </summary>
        /// <param name="scheme">The scheme.</param>
        /// <param name="host">The host.</param>
        /// <exception cref="System.ArgumentException">
        /// Must specify scheme!
        /// or
        /// Must specify host name
        /// </exception>
        public UriBuilder(Scheme scheme, string host)
        {
            if (scheme == Scheme.Unspecified)
                throw new ArgumentException("Must specify scheme!", nameof(scheme));

            if(string.IsNullOrEmpty(host))
                throw new ArgumentException("Must specify host name", nameof(host));

            _scheme = scheme;
            _host = host.Trim(' ', '/');
        }

        /// <summary>
        /// Sets the scheme.
        /// </summary>
        /// <param name="scheme">The scheme.</param>
        /// <returns></returns>
        public IUriBuilder WithScheme(Scheme scheme)
        {
            _scheme = scheme;
            return this;
        }

        /// <summary>
        /// Sets the host authority.
        /// </summary>
        /// <param name="host">The host.</param>
        /// <returns></returns>
        public IUriBuilder WithHost(string host)
        {
            if (string.IsNullOrEmpty(host))
                return this;

            _host = host.Trim(' ', '/');
            return this;
        }

        /// <summary>
        /// Sets the port.
        /// </summary>
        /// <param name="port">The port.</param>
        /// <returns></returns>
        /// <exception cref="System.ArgumentException">Invalid port range</exception>
        public IUriBuilder WithPort(uint port)
        {
            if(port < 1 || port > 65535)
                throw new ArgumentException("Invalid port range", nameof(port));

            _port = port;
            return this;
        }


        /// <summary>
        /// Adds the resource/path to the host authority.
        /// </summary>
        /// <param name="path">The path.</param>
        /// <returns></returns>
        public IUriBuilder WithPath(string path)
        {
            _path = path.Trim(' ', '/');
            return this;
        }

        /// <summary>
        /// Adds object properties to the resource as a query string
        /// </summary>
        /// <param name="parameters">The parameters.</param>
        /// <returns></returns>
        /// <exception cref="System.ArgumentNullException">Object is null, nothing to add</exception>
        public IUriBuilder WithQueryParameters(object parameters)
        {
            if(parameters == null)
                throw new ArgumentNullException(nameof(parameters), "Object is null, nothing to add");

            _queryParameters.Add(parameters);
            return this;
        }

        /// <summary>
        /// Adds the key value pairs to the resource as a query string
        /// </summary>
        /// <param name="parameters">The parameters.</param>
        /// <returns></returns>
        /// <exception cref="System.ArgumentException"></exception>
        public IUriBuilder WithQueryParameters(IEnumerable<KeyValuePair<string, string>> parameters)
        {
            var asList = parameters.ToList();
            if(parameters == null || !asList.Any())
                throw new ArgumentException(nameof(parameters));

            foreach (var parameter in asList)
            {
                _queryParameters.Add(parameter.Key, parameter.Value);
            }

            return this;
        }

        /// <summary>
        /// Adds a key value pair to the resource as a key/value segement
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        /// <exception cref="System.ArgumentNullException">
        /// </exception>
        public IUriBuilder WithSegmentPair(string name, string value)
        {
            if (string.IsNullOrEmpty(name))
                throw new ArgumentNullException(nameof(name));

            if(string.IsNullOrEmpty(value))
                throw new ArgumentNullException(nameof(value));

            var kvp = new KeyValuePair<string, string>(name, value);
            _segments.Add(kvp);

            return this;
        }


        /// <summary>
        /// Builds the URI.
        /// </summary>
        /// <returns></returns>
        public Uri BuildUri()
        {
            var uri = GetUriString();
            return new Uri(uri);
        }

        public override string ToString()
        {
            return GetUriString();
        }

        private string GetUriString()
        {
            if(_scheme == Scheme.Unspecified)
                throw new InvalidOperationException("Scheme not set.");

            // TODO: validate host format
            if(string.IsNullOrEmpty(_host))
                throw new InvalidOperationException("Host not set.");

            var sb = new StringBuilder();
            sb.Append(_scheme.ToString().ToLower());
            sb.Append("://");
            sb.Append(_host);

            if (_port.HasValue)
                sb.Append($":{_port.Value}");

            if (!string.IsNullOrEmpty(_path))
            {
                AddTrailingSlash(sb);

                sb.Append(_path);
            }

            if (_segments.Any())
            {
                foreach (var segment in _segments)
                {
                    AddTrailingSlash(sb);
                    sb.Append($"{segment.Key}/{segment.Value}");
                }
            }

            if (_queryParameters.Any())
            {
                sb.Append("?");
                sb.Append(_queryParameters);
            }

            return sb.ToString();
        }

        private static void AddTrailingSlash(StringBuilder builder)
        {
            if (builder.ToString().EndsWith("/"))
                return;

            builder.Append("/");
        }
    }
}
