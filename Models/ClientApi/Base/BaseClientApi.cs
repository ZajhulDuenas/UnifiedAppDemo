using Api.Models;
using System.Net.Http.Headers;

namespace Models.ClientApi.Base
{
    public abstract class BaseClientApi
    {
        public abstract ClientToken? ClientToken { get; set; }

        private HttpClient client = null;
        protected HttpClient GetClient()
        {
            //var handler = new HttpClientHandler();
            //handler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => true;

            client = new HttpClient();

            client.DefaultRequestHeaders.Clear();
            client.Timeout = TimeSpan.FromMinutes(5);
            client.DefaultRequestHeaders.ConnectionClose = true;
            client.DefaultRequestHeaders.AcceptEncoding.Add(new StringWithQualityHeaderValue("gzip"));
            client.DefaultRequestHeaders.AcceptEncoding.Add(new System.Net.Http.Headers.StringWithQualityHeaderValue("deflate"));

            return client;
        }


        public abstract Task<Response<ClientToken>> OnGetToken();

        public async Task<Response<ClientToken>> GetToken()
        {
            var response = new Response<ClientToken>();

            if (ClientToken != null
                && ClientToken.TokenExpiresIn > DateTime.Now
                && !string.IsNullOrEmpty(ClientToken.Token)
                && ClientToken.ExpiresIn >= 0)
                return response.AddPayload(ClientToken);
            //Cong
            response = await OnGetToken().ConfigureAwait(false);

            if (response.Success && response.Payload != null
                && response.Payload.TokenExpiresIn > DateTime.Now
                && !string.IsNullOrEmpty(response.Payload.Token)
                && response.Payload.ExpiresIn >= 0)
                ClientToken = response.Payload;
            else
                response.Payload = null;

            return response;
        }
    }
}
