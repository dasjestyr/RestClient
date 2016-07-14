using System;
using System.Collections.Generic;

namespace Provausio.Rest.Client
{
    public interface IUriBuilder
    {
        /// <summary>
        /// Sets the scheme.
        /// </summary>
        /// <param name="scheme">The scheme.</param>
        /// <returns></returns>
        IUriBuilder WithScheme(Scheme scheme);

        /// <summary>
        /// Sets the host authority.
        /// </summary>
        /// <param name="host">The host.</param>
        /// <returns></returns>
        IUriBuilder WithHost(string host);

        /// <summary>
        /// Sets the port.
        /// </summary>
        /// <param name="port">The port.</param>
        /// <returns></returns>
        /// <exception cref="System.ArgumentException">Invalid port range</exception>
        IUriBuilder WithPort(uint port);

        /// <summary>
        /// Adds the resource/path to the host authority.
        /// </summary>
        /// <param name="path">The path.</param>
        /// <returns></returns>
        IUriBuilder WithPath(string path);

        /// <summary>
        /// Adds object properties to the resource as a query string
        /// </summary>
        /// <param name="parameters">The parameters.</param>
        /// <returns></returns>
        /// <exception cref="System.ArgumentNullException">Object is null, nothing to add</exception>
        IUriBuilder WithQueryParameters(object parameters);

        /// <summary>
        /// Adds the key value pairs to the resource as a query string
        /// </summary>
        /// <param name="parameters">The parameters.</param>
        /// <returns></returns>
        /// <exception cref="System.ArgumentException"></exception>
        IUriBuilder WithQueryParameters(IEnumerable<KeyValuePair<string, string>> parameters);

        /// <summary>
        /// Adds a key value pair to the resource as a key/value segement
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        /// <exception cref="System.ArgumentNullException">
        /// </exception>
        IUriBuilder WithSegmentPair(string name, string value);

        /// <summary>
        /// Builds the URI.
        /// </summary>
        /// <returns></returns>
        Uri BuildUri();

        string ToString();
    }
}