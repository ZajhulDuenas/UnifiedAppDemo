using common;
using Models;
using Models.ClientApi;
using Models.ClientApi.Base;
using Models.DTOs;
using System.Net.Http;
using System.Net.Http.Json;

namespace Front.Infrastructure.ClientApi
{
    public class EmployeersClient : BaseClientApi, IEmployeersClient
    {
        const string EMPLOYEERS = "/GetEmployees";
        const string DELETE = "/DeleteEmployee";
        const string MODIFY = "/ModifyEmployee";

        const string IMPORT = "/ImportEmployeeList";
        const string EXPORT = "/ExportEmployeeList";

        public string baseEndPoint { get; set; } = string.Empty;

        public override ClientToken? ClientToken { get; set; }

        public override Task<Response<ClientToken>> OnGetToken()
        {
            throw new NotImplementedException();
        }

        public async Task<Response<List<EmployeeRequestDto>>> GetEmployeersByIndex(int index = 1, int pageSize = 10)
        {

            var response = new Response<List<EmployeeRequestDto>>();

            if (string.IsNullOrEmpty(baseEndPoint))
                throw new ArgumentException("Parametros de configuración EmployeersClient Api vacios o nulos");

            try
            {

                using (var client = GetClient())
                {
                    if (client == null)
                        return response.AddError("Error al crear el Http Client EmployeersClient");

                    var request = new
                    {
                        pageIndex = index,
                        pageSize = pageSize
                    };

                    using var content = request.SerializeJsonContent();

                    if (ClientToken != null) client.DefaultRequestHeaders.Add("x-token", ClientToken.Token);
                    var endpoint = new Uri($"{baseEndPoint}{EMPLOYEERS}");

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

                    var result = responseContent.DeserializeJson<Response<List<EmployeeRequestDto>>>();

                    // response.Payload = result.Payload;

                    return result;

                }

            }
            catch (Exception ex)
            {

                Console.WriteLine($"Exception {ex.Message}");
                return null;
            }

        }

        public async Task<Response<EmployeeRequestDto>> DeleteEmployee(int id)
        {

            var response = new Response<EmployeeRequestDto>();

            if (string.IsNullOrEmpty(baseEndPoint))
                throw new ArgumentException("Parametros de configuración EmployeersClient Api vacios o nulos");

            try
            {

                using (var client = GetClient())
                {
                    if (client == null)
                        return response.AddError("Error al crear el Http Client EmployeersClient");

                    var request = new
                    {
                        EmployeeId = id
                    };

                    using var content = request.SerializeJsonContent();

                    if (ClientToken != null) client.DefaultRequestHeaders.Add("x-token", ClientToken.Token);
                    var endpoint = new Uri($"{baseEndPoint}{DELETE}");

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

                    var result = responseContent.DeserializeJson<Response<EmployeeRequestDto>>();

                    // response.Payload = result.Payload;

                    return result;

                }

            }
            catch (Exception ex)
            {

                Console.WriteLine($"Exception {ex.Message}");
                return null;
            }

        }

        public async Task<Response<EmployeeRequestDto>> ModifyEmployee(EmployeeRequestDto request)
        {

            var response = new Response<EmployeeRequestDto>();

            if (string.IsNullOrEmpty(baseEndPoint))
                throw new ArgumentException("Parametros de configuración EmployeersClient Api vacios o nulos");

            try
            {

                using (var client = GetClient())
                {
                    if (client == null)
                        return response.AddError("Error al crear el Http Client EmployeersClient");

                    
                    using var content = request.SerializeJsonContent();

                    if (ClientToken != null) client.DefaultRequestHeaders.Add("x-token", ClientToken.Token);
                    var endpoint = new Uri($"{baseEndPoint}{MODIFY}");

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

                    var result = responseContent.DeserializeJson<Response<EmployeeRequestDto>>();

                    // response.Payload = result.Payload;

                    return result;

                }

            }
            catch (Exception ex)
            {

                Console.WriteLine($"Exception {ex.Message}");
                return null;
            }

        }

    }
}
