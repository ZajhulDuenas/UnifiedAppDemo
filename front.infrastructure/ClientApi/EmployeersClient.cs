using common;
using Microsoft.Extensions.Configuration;
using Models;
using Models.ClientApi;
using Models.ClientApi.Base;
using Models.DTOs;
using System.Net.Http.Json;

namespace Front.Infrastructure.ClientApi
{
    public class EmployeersClient(IConfiguration configuration) : BaseClientApi, IEmployeersClient
    {
        private readonly IConfiguration configuration = configuration;

        const string EMPLOYEERS = "/GetEmployees";
        const string DELETE = "/DeleteEmployee";
        const string MODIFY = "/ModifyEmployee";
        const string ADD = "/AddEmployee";

        const string IMPORT = "/ImportEmployeeList";
        const string EXPORT = "/ExportEmployeeList";

        public string baseEndPoint { get; set; } = string.Empty;

        public override ClientToken? ClientToken { get; set; }


        public async Task<Models.Response<List<EmployeeRequestDto>>> GetEmployeersByIndex(int index = 1, int pageSize = 10)
        {
            CheckParameters();

            var response = new Response<List<EmployeeRequestDto>>();

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

        public async Task<Models.Response<EmployeeRequestDto>> DeleteEmployee(int id)
        {
            CheckParameters();

            var response = new Response<EmployeeRequestDto>();

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

        public async Task<Models.Response<EmployeeRequestDto>> ModifyEmployee(EmployeeRequestDto request)
        {
            CheckParameters();

            var response = new Response<EmployeeRequestDto>();

            try
            {

                using (var client = GetClient())
                {
                    if (client == null)
                        return response.AddError("Error al crear el Http Client EmployeersClient");

                    var dataIn = new {
                        EmployeeId = request.Id,
                        Name = request.Name,
                        Rfc = request.Rfc,
                        DateBirth = request.DateBirth.ToString("yyyy-MM-dd")
                    };

                    using var content = dataIn.SerializeJsonContent();

                    if (ClientToken != null) client.DefaultRequestHeaders.Add("x-token", ClientToken.Token);
                    var endpoint = new Uri($"{baseEndPoint}{MODIFY}");

                    var responseApi = await client.PostAsJsonAsync(endpoint, dataIn).ConfigureAwait(false);
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

        public async Task<Models.Response<EmployeeRequestDto>> AddEmployee(EmployeeRequestDto request)
        {
            CheckParameters();

            var response = new Models.Response<EmployeeRequestDto>();

            try
            {

                using (var client = GetClient())
                {
                    if (client == null)
                        return response.AddError("Error al crear el Http Client EmployeersClient");

                    var dataIn = new
                    {
                        Name = request.Name,
                        Rfc = request.Rfc,
                        DateBirth = request.DateBirth.ToString("yyyy-MM-dd")
                    };

                    using var content = dataIn.SerializeJsonContent();

                    if (ClientToken != null) client.DefaultRequestHeaders.Add("x-token", ClientToken.Token);
                    var endpoint = new Uri($"{baseEndPoint}{ADD}");

                    var responseApi = await client.PostAsJsonAsync(endpoint, dataIn).ConfigureAwait(false);
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

                    var result = responseContent.DeserializeJson<Models.Response<EmployeeRequestDto>>();

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

        public async Task<Models.Response<byte[]>> ExportData()
        {
            CheckParameters();

            Models.Response<byte[]> result = new Models.Response<byte[]>();

            try
            {

                using (var client = GetClient())
                {
                    if (ClientToken != null) client.DefaultRequestHeaders.Add("x-token", ClientToken.Token);
                    var endpoint = new Uri($"{baseEndPoint}{EXPORT}");

                    var responseApi = await client.GetAsync(endpoint).ConfigureAwait(false);
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

                        return result.AddError($"Ocurrió un error al consumir: {endpoint}");
                    }

                    // response.Payload = result.Payload;

                    result.StatusCode = 200;
                    result.Payload = await responseApi.Content.ReadAsByteArrayAsync().ConfigureAwait(false);
                    return result;

                }

            }
            catch (Exception ex)
            {

                Console.WriteLine($"Exception {ex.Message}");
                return null;
            }

        }

        public async Task<object> ImportData(MultipartFormDataContent content)
        {
            CheckParameters();

            Models.Response<object> result = new Models.Response<object>();

            try
            {

                using (var client = GetClient())
                {
                    if (ClientToken != null) client.DefaultRequestHeaders.Add("x-token", ClientToken.Token);
                    var endpoint = new Uri($"{baseEndPoint}{IMPORT}");

                    var responseApi = await client.PostAsync(endpoint, content).ConfigureAwait(false);

                    if (!responseApi.IsSuccessStatusCode)
                    {
                        var log = new
                        {

                            Response = responseApi.ToString(),
                           // Content = responseContent,
                            EndPoint = baseEndPoint
                        }.SerializeJson();

                        // Logger.LogError("Error de API: {Data}", log);

                        return result.AddError($"Ocurrió un error al consumir: {endpoint}");
                    }

                    // response.Payload = result.Payload;

                    result.StatusCode = 200;
                    result.Payload = null;
                    return result;

                }

            }
            catch (Exception ex)
            {

                Console.WriteLine($"Exception {ex.Message}");
                return null;
            }

        }

        public override Task<Response<ClientToken>> OnGetToken()
        {
            throw new NotImplementedException();
        }

        private void CheckParameters() 
        {

            baseEndPoint = configuration["UrlsServices:UrlEmployeers"];

            if (ClientToken == null)
                throw new ArgumentException("Api Token vacios o nulos");


            if (string.IsNullOrEmpty(baseEndPoint))
                throw new ArgumentException("Parametros de configuración EmployeersClient Api vacios o nulos");

        }
    }
}
