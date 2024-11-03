using Front.Infrastructure.ClientApi;
using Microsoft.AspNetCore.Components;
using Models;
using Models.ClientApi;
using System.Net.Http.Json;
using System.Reflection;
using UserStories.login;

namespace WepApp.Pages
{
    public partial class Employeers
    {
        [Inject]
        private ITokenServiceClient tokenServiceClient { get; set; } = default!;

        [Inject]
        public IConfiguration configuration { get; set; } = default!;

        private EmployeersForecast[]? forecasts;

        protected override async Task OnInitializedAsync()
        {
            // forecasts = await Http.GetFromJsonAsync<EmployeersForecast[]>("sample-data/weather.json");

            tokenServiceClient.baseEndPoint = configuration["TokenServiceSettings:UrlBase"];

            tokenServiceClient.userName = "jperez";

            tokenServiceClient.password = "123";

            var response = await tokenServiceClient.OnGetToken();
        }

       
    }

    public class EmployeersForecast
    {
        public DateOnly Date { get; set; }

        public int TemperatureC { get; set; }

        public string? Summary { get; set; }

        public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
    }
}
