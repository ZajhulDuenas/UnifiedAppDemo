using Polly;
using Polly.Retry;
using System.Net;
using System.Net.Http.Headers;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class HttpClientExtensions
    {
        private static AsyncRetryPolicy<HttpResponseMessage> GetRetryPolicy()
        {
            return Policy<HttpResponseMessage>
                .Handle<HttpRequestException>()
                .OrResult(msg => msg.StatusCode == System.Net.HttpStatusCode.InternalServerError ||
                                 msg.StatusCode == System.Net.HttpStatusCode.RequestTimeout)
                .WaitAndRetryAsync(3, retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)));
        }

        public static void AddHttpClientWithRetryPolicy(this IServiceCollection services, string name, bool useCompression = true)
        {
            services.AddHttpClient(name, client =>
            {
                client.DefaultRequestHeaders.Clear();
                client.Timeout = TimeSpan.FromMinutes(5);
                client.DefaultRequestHeaders.ConnectionClose = true;
                if (useCompression)
                    client.DefaultRequestHeaders.AcceptEncoding.Add(new StringWithQualityHeaderValue("gzip"));
            }).ConfigurePrimaryHttpMessageHandler(() =>
            {
                var httpClientHandler = new HttpClientHandler
                {
                    MaxConnectionsPerServer = 2000,
                };

                if (useCompression)
                    httpClientHandler.AutomaticDecompression = DecompressionMethods.GZip
                        | DecompressionMethods.Deflate;

                return httpClientHandler;
            }).AddPolicyHandler(GetRetryPolicy());
        }
    }
}
