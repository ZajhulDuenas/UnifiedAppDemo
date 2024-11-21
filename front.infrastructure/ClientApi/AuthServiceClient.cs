using common;
using System.Net.Http.Json;
using Microsoft.Extensions.Configuration;
using Models.ClientApi.Base;
using Models.ClientApi;
using Api.Models;


namespace Front.Infrastructure.ClientApi
{
    public class AuthServiceClient(IConfiguration configuration) : BaseClientApi, IAuthServiceClient
    {
        private readonly IConfiguration configuration = configuration;

        const string AUTH_TOKEN = "/GetToken";

        public string baseEndPoint { get; set; } = string.Empty;
        public string userName { get; set; } = string.Empty;
        public string password { get; set; } = string.Empty;
        public override ClientToken? ClientToken { get; set; }

        public async Task<Response<ClientToken>> OnBasicGetToken(string user, string pass)
        {
            var response = new Response<ClientToken>();

            baseEndPoint = configuration["UrlsServices:UrlBase"];

            try
            {

                using (var client = GetClient())
                {
                    if (client == null)
                        return response.AddError("Error al crear el Http Client BlockChainApisClientProxy");

                    var request = new
                    {
                        username = user,
                        password = pass
                    };

                    using var content = request.SerializeJsonContent();

                    var endpoint = new Uri($"{baseEndPoint}{AUTH_TOKEN}");

                    var responseApi = await client.PostAsJsonAsync(endpoint, request).ConfigureAwait(false);
                    var responseContent = await responseApi.Content.ReadAsStringAsync().ConfigureAwait(false);

                    if (!responseApi.IsSuccessStatusCode)
                    {
                        var log = new
                        {

                            Response = responseApi.ToString(),
                            Content = responseContent,
                            EndPoint = baseEndPoint
                        }.SerializeJson();

                        // Logger.LogError("Error de API: {Data}", log);

                        return response.AddError($"Ocurrió un error al consumir: {endpoint}");
                    }

                    var result = responseContent.DeserializeJson<Response<ClientToken>>();

                    // response.Payload = result.Payload;

                    return result;

                }

            }
            catch (Exception ex) {

                Console.WriteLine($"Exception {ex.Message}");
                return null;
            }
        }

        public override Task<Response<ClientToken>> OnGetToken()
        {
            throw new NotImplementedException();
        }
    }
}
