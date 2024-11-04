using Front.Infrastructure.ClientApi;
using Microsoft.AspNetCore.Components;
using Models.ClientApi;
using Models.DTOs;
using WepApp.Models;
using WepApp.Services;

namespace WepApp.Pages
{
    public partial class Employeers
    {
        [Inject] protected AuthService AuthService { get; set; }
        [Inject] protected VarsService VarsService { get; set; }

        [Inject] private IEmployeersClient employeersClient { get; set; } = default!;
        [Inject] public IConfiguration configuration { get; set; } = default!;

        private bool isAuthenticated { get; set; } = false;

        private int IndexPagination { get; set; } = 1;

        private string message { get; set; } = "";

        private EmployeersForecast[]? forecasts;

        private List<EmployeeRequestDto> EmployeeList { get; set; } = default!;

        protected override async Task OnInitializedAsync()
        {
            await refreshEmployeTableAsync();
        }

        private async Task OnButtonBeforeClickAsync()
        {

            if (IndexPagination > 1)
            {
                IndexPagination--;
                await refreshEmployeTableAsync();
            }

        }

        private async Task OnButtonAfterClickAsync()
        {

            IndexPagination++;
            await refreshEmployeTableAsync();

        }

        #region Delete Functions

        private bool ShowConfirmDialog = false;
        private int? EmployeeIdToDelete;
        private void ShowDeleteConfirmation(int idEmpleado)
        {
            EmployeeIdToDelete = idEmpleado;
            ShowConfirmDialog = true;
        }

        private async Task OnButtonDeleteClickAsync(int id)
        {
            var res = await CheckStatusUsr();

            if (res)
            {

                string token = "";

                // Usa AuthService para guardar el token
                token = await AuthService.GetToken();

                employeersClient.baseEndPoint = configuration["TokenServiceSettings:UrlEmployeers"];
                employeersClient.ClientToken = new ClientToken() { Token = token };

                var response = await employeersClient.DeleteEmployee(id).ConfigureAwait(false);

                // EmployeeList = response.Payload;
            }
        }

        private async Task ConfirmDeleteAsync()
        {
            var res = await CheckStatusUsr();


            if (res && EmployeeIdToDelete.HasValue)
            {
                // Lógica para eliminar el empleado

                string token = "";

                // Usa AuthService para guardar el token
                token = await AuthService.GetToken();

                employeersClient.baseEndPoint = configuration["TokenServiceSettings:UrlEmployeers"];
                employeersClient.ClientToken = new ClientToken() { Token = token };

                var response = await employeersClient.DeleteEmployee(EmployeeIdToDelete.Value).ConfigureAwait(false);

                // EmployeeList = response.Payload;

                EmployeeList = EmployeeList.Where(e => e.IdEmpleado != EmployeeIdToDelete).ToList();
                EmployeeIdToDelete = null;
                ShowConfirmDialog = false;

            }
        }

        private void CloseConfirmDialog()
        {
            ShowConfirmDialog = false;
            EmployeeIdToDelete = null;
        }
        #endregion



        private async Task refreshEmployeTableAsync() 
        {
            var res = await CheckStatusUsr();

            if (res) {
                string token = "";

                // Usa AuthService para guardar el token
                token = await AuthService.GetToken();

                employeersClient.baseEndPoint = configuration["TokenServiceSettings:UrlEmployeers"];
                employeersClient.ClientToken = new ClientToken() { Token = token };

                var response = await employeersClient.GetEmployeersByIndex(IndexPagination, 10).ConfigureAwait(false);

                EmployeeList = response.Payload;

            }

        }

        private async Task<bool> CheckStatusUsr() { 
        
            bool result = true;

            if (AuthService == null)
            {
                return false;
            }

            isAuthenticated = await AuthService.IsAuthenticated();
            UserModel info = new UserModel();
            
            if (!isAuthenticated)
            {
                message = "Por favor, inicia sesión para continuar.";
                return false;
            }

            message = "";
            info = await VarsService.ExtractObject<UserModel>("UserModel");

            if (isAuthenticated && info != null && !info.ReadList)
            {
                message = "No tienes privilegios para ver listas";
                return false;
            }

            return result;
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
