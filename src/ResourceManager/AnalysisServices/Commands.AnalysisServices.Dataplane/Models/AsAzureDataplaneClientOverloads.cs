// ----------------------------------------------------------------------------------
//
// Copyright Microsoft Corporation
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
// http://www.apache.org/licenses/LICENSE-2.0
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.
// ----------------------------------------------------------------------------------

using System;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Rest;

namespace Microsoft.Azure.Commands.AnalysisServices.Dataplane.Models
{
    /// <summary>
    /// Contains all permutations of constructors, CallGetAsync, and CallPostAsync for <see cref="AsAzureDataplaneClient"/>.
    /// The reside here, in order to make the other partial class more readable and easier to debug.
    /// </summary>
    /// <remarks>Most modifications will likely be in the other part of the class.</remarks>
    public partial class AsAzureDataplaneClient : ServiceClient<AsAzureDataplaneClient>, IAsAzureHttpClient
    {
        public AsAzureDataplaneClient(params DelegatingHandler[] handlers) : base(handlers)
        {
            this.resetHttpClient();
        }

        public AsAzureDataplaneClient(HttpClientHandler rootHandler, params DelegatingHandler[] handlers) : base(rootHandler, handlers)
        {
            this.resetHttpClient();
        }

        public AsAzureDataplaneClient(Uri baseUri, params DelegatingHandler[] handlers) : this(handlers)
        {
            this.BaseUri = baseUri ?? throw new ArgumentNullException(nameof(baseUri));
        }

        public AsAzureDataplaneClient(Uri baseUri, HttpClientHandler rootHandler, params DelegatingHandler[] handlers) : this(rootHandler, handlers)
        {
            this.BaseUri = baseUri ?? throw new ArgumentNullException(nameof(baseUri));
        }

        public AsAzureDataplaneClient(ServiceClientCredentials credentials, params DelegatingHandler[] handlers) : this(handlers)
        {
            this.Credentials = credentials ?? throw new ArgumentNullException(nameof(credentials));
            this.Credentials.InitializeServiceClient(this);
        }

        public AsAzureDataplaneClient(ServiceClientCredentials credentials, HttpClientHandler rootHandler, params DelegatingHandler[] handlers) : this(rootHandler, handlers)
        {
            this.Credentials = credentials ?? throw new ArgumentNullException(nameof(credentials));
            this.Credentials.InitializeServiceClient(this);
        }

        public AsAzureDataplaneClient(Func<HttpClient> httpClientProvider, params DelegatingHandler[] handlers) : this(handlers)
        {
            this.HttpClientProvider = httpClientProvider ?? throw new ArgumentNullException(nameof(httpClientProvider));
        }

        public AsAzureDataplaneClient(Func<HttpClient> httpClientProvider, HttpClientHandler rootHandler, params DelegatingHandler[] handlers) : this(rootHandler, handlers)
        {
            this.HttpClientProvider = httpClientProvider ?? throw new ArgumentNullException(nameof(httpClientProvider));
        }

        public AsAzureDataplaneClient(Uri baseUri, ServiceClientCredentials credentials, params DelegatingHandler[] handlers) : this(credentials, handlers)
        {
            this.BaseUri = baseUri ?? throw new ArgumentNullException(nameof(baseUri));
        }

        public AsAzureDataplaneClient(Uri baseUri, ServiceClientCredentials credentials, HttpClientHandler rootHandler, params DelegatingHandler[] handlers) : this(credentials, rootHandler, handlers)
        {
            this.BaseUri = baseUri ?? throw new ArgumentNullException(nameof(baseUri));
        }

        public AsAzureDataplaneClient(Uri baseUri, Func<HttpClient> httpClientProvider, params DelegatingHandler[] handlers) : this(httpClientProvider, handlers)
        {
            this.BaseUri = baseUri ?? throw new ArgumentNullException(nameof(baseUri));
        }

        public AsAzureDataplaneClient(Uri baseUri, Func<HttpClient> httpClientProvider, HttpClientHandler rootHandler, params DelegatingHandler[] handlers) : this(httpClientProvider, rootHandler, handlers)
        {
            this.BaseUri = baseUri ?? throw new ArgumentNullException(nameof(baseUri));
        }

        public AsAzureDataplaneClient(ServiceClientCredentials credentials, Func<HttpClient> httpClientProvider, params DelegatingHandler[] handlers) : this(credentials, handlers)
        {
            this.HttpClientProvider = httpClientProvider ?? throw new ArgumentNullException(nameof(httpClientProvider));
        }

        public AsAzureDataplaneClient(ServiceClientCredentials credentials, Func<HttpClient> httpClientProvider, HttpClientHandler rootHandler, params DelegatingHandler[] handlers) : this(credentials, rootHandler, handlers)
        {
            this.HttpClientProvider = httpClientProvider ?? throw new ArgumentNullException(nameof(httpClientProvider));
        }

        public AsAzureDataplaneClient(Uri baseUri, ServiceClientCredentials credentials, Func<HttpClient> httpClientProvider, params DelegatingHandler[] handlers) : this(baseUri, credentials, handlers)
        {
            this.HttpClientProvider = httpClientProvider ?? throw new ArgumentNullException(nameof(httpClientProvider));
        }

        public AsAzureDataplaneClient(Uri baseUri, ServiceClientCredentials credentials, Func<HttpClient> httpClientProvider, HttpClientHandler rootHandler, params DelegatingHandler[] handlers) : this(baseUri, credentials, rootHandler, handlers)
        {
            this.HttpClientProvider = httpClientProvider ?? throw new ArgumentNullException(nameof(httpClientProvider));
        }

        /// <summary>
        /// Calls SendRequestAsync() for a GET using a blank correlationId.
        /// </summary>
        /// <param name="baseUri">The base Uri to call.</param>
        /// <param name="requestUrl">The request Url.</param>
        /// <param name="accessToken">The access token (will be discarded).</param>
        /// <returns>The http response message.</returns>
        public async Task<HttpResponseMessage> CallGetAsync(Uri baseUri, string requestUrl, string accessToken)
        {
            return await CallGetAsync(baseUri: baseUri, requestUrl: requestUrl, accessToken: accessToken, correlationId: new Guid());
        }

        /// <summary>
        /// Calls SendRequestAsync() for a GET.
        /// </summary>
        /// <param name="baseUri">The base Uri to call.</param>
        /// <param name="requestUrl">The request Url.</param>
        /// <param name="accessToken">The access token (will be discarded).</param>
        /// <param name="correlationId">The CorrelationId</param>
        /// <returns>The http response message.</returns>
        public async Task<HttpResponseMessage> CallGetAsync(Uri baseUri, string requestUrl, string accessToken = null, Guid correlationId = new Guid())
        {
            return await SendRequestAsync(HttpMethod.Get, baseUri: baseUri, requestUrl: requestUrl, correlationId: correlationId);
        }

        /// <summary>
        /// Calls SendRequestAsync() for a GET using the default BaseUri and a blank correlationId.
        /// </summary>
        /// <param name="requestUrl">The Request Url.</param>
        /// <returns>The http response message.</returns>
        public async Task<HttpResponseMessage> CallGetAsync(string requestUrl)
        {
            return await CallGetAsync(requestUrl, new Guid());
        }

        /// <summary>
        /// Calls SendRequestAsync() for a GET using the default BaseUri.
        /// </summary>
        /// <param name="requestUrl">The Request Url.</param>
        /// <param name="correlationId">The CorrelationId</param>
        /// <returns>The http response message.</returns>
        public async Task<HttpResponseMessage> CallGetAsync(string requestUrl, Guid correlationId)
        {
            return await SendRequestAsync(HttpMethod.Get, BaseUri, requestUrl, correlationId);
        }

        /// <summary>
        /// Calls SendRequestAsync() for a POST using a blank correlationId.
        /// </summary>
        /// <param name="baseUri">The base Uri to call.</param>
        /// <param name="requestUrl">The request Url.</param>
        /// <param name="accessToken">The access token (will be discarded).</param>
        /// <param name="content">The content to post (optional).</param>
        /// <returns>The http response message.</returns>
        public async Task<HttpResponseMessage> CallPostAsync(Uri baseUri, string requestUrl, string accessToken, HttpContent content = null)
        {
            return await CallPostAsync(baseUri: baseUri, requestUrl: requestUrl, accessToken: accessToken, correlationId: new Guid(), content: content);
        }

        /// <summary>
        /// Calls SendRequestAsync() for a POST.
        /// </summary>
        /// <param name="baseUri">The base Uri to call.</param>
        /// <param name="requestUrl">The request Url.</param>
        /// <param name="accessToken">The access token (will be discarded).</param>
        /// <param name="correlationId">The CorrelationId</param>
        /// <param name="content">The content to post (optional).</param>
        /// <returns>The http response message.</returns>
        public async Task<HttpResponseMessage> CallPostAsync(Uri baseUri, string requestUrl, string accessToken = null, Guid correlationId = new Guid(), HttpContent content = null)
        {
            return await SendRequestAsync(HttpMethod.Post, baseUri: baseUri, requestUrl: requestUrl, correlationId: correlationId, content: content);
        }

        /// <summary>
        /// Calls SendRequestAsync() for a POST using the default BaseUri and a blank correlationId.
        /// </summary>
        /// <param name="requestUrl">The Request Url.</param>
        /// <param name="content">The content to post (optional).</param>
        /// <returns>The http response message.</returns>
        public async Task<HttpResponseMessage> CallPostAsync(string requestUrl, HttpContent content = null)
        {
            return await CallPostAsync(requestUrl, new Guid(), content);
        }

        /// <summary>
        /// Calls SendRequestAsync() for a POST using the default BaseUri.
        /// </summary>
        /// <param name="requestUrl">The Request Url.</param>
        /// <param name="correlationId">The CorrelationId</param>
        /// <param name="content">The content to post (optional).</param>
        /// <returns>The http response message.</returns>
        public async Task<HttpResponseMessage> CallPostAsync(string requestUrl, Guid correlationId, HttpContent content = null)
        {
            return await SendRequestAsync(HttpMethod.Post, BaseUri, requestUrl, correlationId, content);
        }
    }
}
