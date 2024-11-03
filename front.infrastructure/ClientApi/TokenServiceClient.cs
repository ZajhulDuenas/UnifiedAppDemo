using Models;
using Models.ClientApi;
using Models.ClientApi.Base;
using common;
using System.Net.Http.Json;
using System.Net;


namespace Front.Infrastructure.ClientApi
{
    public class TokenServiceClient : BaseClientApi, ITokenServiceClient
    {
       
        const string AUTH_TOKEN = "/GetToken";

        public string baseEndPoint { get; set; } = string.Empty;
        public string userName { get; set; } = string.Empty;
        public string password { get; set; } = string.Empty;
        public override ClientToken? ClientToken { get; set; }

        public override async Task<Response<ClientToken>> OnGetToken()
        {
            var response = new Response<ClientToken>();

            if (string.IsNullOrEmpty(baseEndPoint))
                throw new ArgumentException("Parametros de configuración TokenService Api vacios o nulos");


            try
            {

                using (var client = GetClient())
                {
                    if (client == null)
                        return response.AddError("Error al crear el Http Client BlockChainApisClientProxy");

                    var request = new
                    {
                        username = userName,
                        password = password
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
    }
}
