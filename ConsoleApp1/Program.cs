using Front.Infrastructure.ClientApi;
using System;
using System.Net.Http;
using System.Net.Http.Json; // Necesario para PostAsJsonAsync
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;

namespace ConsoleHttpClientExample
{
    class Program
    {
        static async Task Main(string[] args)
        {

            ITokenServiceClient tokenServiceClient = new TokenServiceClient();


            //var handler = new HttpClientHandler();
            //handler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => true;

            // Create a custom certificate validation method
            bool ValidateCertificate(HttpRequestMessage request, X509Certificate2 certificate, X509Chain chain, SslPolicyErrors sslErrors)
            {
                // Your custom validation logic here
                // For example, you can check the certificate's subject, issuer, or expiration date
                return true; // or false, depending on your validation criteria
            }



            // Crear una instancia de HttpClient
            using var httpClient = new HttpClient(new HttpClientHandler
            {
                ServerCertificateCustomValidationCallback = ValidateCertificate
            });

            // Configura la URL base de la API
            // httpClient.BaseAddress = new Uri("https://192.168.0.8:7299/api/Auth");

            // Crear un objeto con los datos que quieres enviar
            var user = new PayLoad
            {
                Username = "jperez",
                Password = "123"
            };

            try
            {
                tokenServiceClient.password = "123";
                tokenServiceClient.userName = "jperez";

                tokenServiceClient.baseEndPoint = "https://192.168.0.8:7299/api/Auth";
                var res = await tokenServiceClient.OnGetToken();


                // Enviar la solicitud POST
                var response = await httpClient.PostAsJsonAsync("https://192.168.0.8:7299/api/Auth/GetToken", user);

                if (res.Success)
                {
                    Console.WriteLine("User registered successfully!");
                }
                else
                {
                    Console.WriteLine($"Error: {response.ReasonPhrase}");
                }
            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine($"Request error: {ex.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }
        }
    }

    public class PayLoad
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }
}
