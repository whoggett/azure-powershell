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
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Rest;

namespace Microsoft.Azure.Commands.AnalysisServices.Dataplane.Models
{
    /// <summary>
    /// Contains properties and logic for <see cref="AsAzureDataplaneClient"/>.
    /// All permutations of constructors, CallGetAsync, and CallPostAsync are in the partial class in order to make this partial class more readable.
    /// </summary>
    /// <remarks>Most modifications will likely be in this part of the class.</remarks>
    public partial class AsAzureDataplaneClient : ServiceClient<AsAzureDataplaneClient>, IAsAzureHttpClient
    {
        /// <summary>
        /// The base Uri of the service.
        /// </summary>
        public Uri BaseUri { get; set; }

        /// <summary>
        /// Credentials needed for the client to connect to Azure.
        /// </summary>
        public ServiceClientCredentials Credentials { get; private set; }

        private Func<HttpClient> HttpClientProvider { get; set; }

        // TODO: This might not work, but is only solution for multiple inhertiance
        public new HttpClient HttpClient { get; set; }

        public void resetHttpClient()
        {
            this.HttpClient = this.HttpClientProvider();
        }

        private async Task<HttpResponseMessage> SendRequestAsync(
            HttpMethod method,
            Uri baseUri,
            string requestUrl,
            Guid correlationId,
            HttpContent content = null,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            // Construct URL
            ////List<string> queryParameters = new List<string>();

            // Create HTTP transport objects
            HttpRequestMessage httpRequest = new HttpRequestMessage()
            {
                Method = method,
                RequestUri = new Uri(baseUri, requestUrl)
            };

            httpRequest.Content = content;

            // Set Headers
            AddHeader(httpRequest.Headers, "x-ms-client-request-id", correlationId.ToString());

            // TODO: Where is this flag located?
            ////if (this.Client.AcceptLanguage != null)
            ////{
            ////    AddHeader(httpRequest.Headers, "accept-language", this.Client.AcceptLanguage);
            ////}

            // TODO: Where are the custom headers coming from?
            ////if (customHeaders != null)
            ////{
            ////    foreach (var header in customHeaders)
            ////    {
            ////        AddHeader(httpRequest.Headers, header.Key, header.Value);
            ////    }
            ////}

            // Set Credentials
            if (Credentials != null)
            {
                cancellationToken.ThrowIfCancellationRequested();
                await Credentials.ProcessHttpRequestAsync(httpRequest, cancellationToken).ConfigureAwait(false);
            }

            HttpResponseMessage httpResponse = await this.HttpClient.SendAsync(httpRequest, cancellationToken).ConfigureAwait(false);
            return httpResponse;
        }

        /// <summary>
        /// Adds a header to the <see cref="HttpRequestHeaders"/> list object.
        /// </summary>
        /// <param name="headers">The request headers list object.</param>
        /// <param name="name">The name of the header to add.</param>
        /// <param name="value">The value of the header.</param>
        private static void AddHeader(HttpRequestHeaders headers, string name, string value)
        {
            if (headers.Contains(name))
            {
                headers.Remove(name);
            }
            headers.TryAddWithoutValidation(name, value);
        }
    }
}
