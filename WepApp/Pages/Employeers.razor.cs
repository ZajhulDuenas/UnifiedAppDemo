using Front.Infrastructure.ClientApi;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.JSInterop;
using Models.ClientApi;
using Models.DTOs;
using System.ComponentModel.DataAnnotations;
using WepApp.Models;
using WepApp.Services;
using static WepApp.Pages.Employeers;

namespace WepApp.Pages
{
    public partial class Employeers
    {

        [Inject] protected IJSRuntime JS { get; set; }
        [Inject] protected AuthService AuthService { get; set; }
        [Inject] protected VarsService VarsService { get; set; }

        [Inject] private IEmployeersClient employeersClient { get; set; } = default!;
        [Inject] public IConfiguration configuration { get; set; } = default!;

        private bool isAuthenticated { get; set; } = false;

        private int IndexPagination { get; set; } = 1;

        private string message { get; set; } = "";

        private UserModel userModel { get; set; } = new UserModel();

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

        private async Task refreshEmployeTableAsync()
        {
            var res = await CheckStatusUsr();

            if (res)
            {
                string token = "";

                // Usa AuthService para guardar el token
                token = await AuthService.GetToken();

                employeersClient.baseEndPoint = configuration["TokenServiceSettings:UrlEmployeers"];
                employeersClient.ClientToken = new ClientToken() { Token = token };

                var response = await employeersClient.GetEmployeersByIndex(IndexPagination, 10).ConfigureAwait(false);

                EmployeeList = response.Payload;

            }

        }

        private async Task<bool> CheckStatusUsr()
        {

            bool result = true;

            if (AuthService == null)
            {
                return false;
            }

            isAuthenticated = await AuthService.IsAuthenticated();
            //UserModel info = new UserModel();

            if (!isAuthenticated)
            {
                message = "Por favor, inicia sesión para continuar.";
                return false;
            }

            message = "";
            userModel = await VarsService.ExtractObject<UserModel>("UserModel");

            if (isAuthenticated && userModel != null && !userModel.ReadList)
            {
                message = "No tienes privilegios para ver listas";
                return false;
            }

            return result;
        }

        #region Delete Employee

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

                // Usa AuthService para recuperar el token
                token = await AuthService.GetToken();

                employeersClient.baseEndPoint = configuration["TokenServiceSettings:UrlEmployeers"];
                employeersClient.ClientToken = new ClientToken() { Token = token };

                var response = await employeersClient.DeleteEmployee(EmployeeIdToDelete.Value).ConfigureAwait(false);

                // EmployeeList = response.Payload;

                EmployeeList = EmployeeList.Where(e => e.Id != EmployeeIdToDelete).ToList();
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

        #region Modify Employee
        private bool ShowEditDialog = false;
        private EmployeeRequestDto SelectedEmployee = new EmployeeRequestDto();

        private void ShowEditModal(EmployeeRequestDto employee)
        {
            // Copia los datos del empleado seleccionado para edición
            SelectedEmployee = new EmployeeRequestDto
            {
                Id = employee.Id,
                Name = employee.Name,
                Rfc = employee.Rfc,
                DateBirth = employee.DateBirth
            };
            ShowEditDialog = true;
        }

        private async Task SaveChangesAsync()
        {
            // Lógica para salvar cambios del empleado

            string token = "";

            // Usa AuthService para recuperar el token
            token = await AuthService.GetToken();

            employeersClient.baseEndPoint = configuration["TokenServiceSettings:UrlEmployeers"];
            employeersClient.ClientToken = new ClientToken() { Token = token };

            var response = await employeersClient.ModifyEmployee(SelectedEmployee).ConfigureAwait(false);

            // EmployeeList = response.Payload;

            // Encuentra el empleado en la lista y actualiza sus datos
            var employee = EmployeeList.FirstOrDefault(e => e.Id == SelectedEmployee.Id);
            if (employee != null)
            {
                employee.Name = SelectedEmployee.Name;
                employee.Rfc = SelectedEmployee.Rfc;
                employee.DateBirth = SelectedEmployee.DateBirth;
            }

            ShowEditDialog = false;
        }

        private void CloseEditModal()
        {
            ShowEditDialog = false;
        }
        #endregion

        #region Add Employee
        private Employee newEmployee = new Employee();
        private bool isAddEmployeeModalVisible = false;

        // Mostrar el modal
        private void ShowAddEmployeeModal()
        {
            isAddEmployeeModalVisible = true;
            newEmployee = new Employee(); // Reiniciar el modelo
        }

        // Cerrar el modal
        private void CloseAddEmployeeModal()
        {
            isAddEmployeeModalVisible = false;
        }

        // Agregar el empleado a la lista
        private async Task AddEmployeeAsync()
        {

            // Lógica para salvar cambios del empleado

            var employee = new EmployeeRequestDto()
            {
                DateBirth = newEmployee.FechaNacimiento,
                Id = newEmployee.IdEmpleado,
                Name = newEmployee.Nombre,
                Rfc = newEmployee.Rfc
            };


            string token = "";

            // Usa AuthService para recuperar el token
            token = await AuthService.GetToken();

            employeersClient.baseEndPoint = configuration["TokenServiceSettings:UrlEmployeers"];
            employeersClient.ClientToken = new ClientToken() { Token = token };

            var response = await employeersClient.AddEmployee(employee).ConfigureAwait(false);

            newEmployee.IdEmpleado = EmployeeList.Count + 1; // Generar un ID simple

            EmployeeList.Add(employee);

            CloseAddEmployeeModal(); // Cerrar el modal al finalizar
        }

        #endregion

        #region Import & Export

        private bool mostrarModal = false;
        private IBrowserFile archivoSeleccionado;

        private async Task ExportList()
        {
            var res = await CheckStatusUsr();

            if (res && userModel.ExportList)
            {
                string token = "";

                // Usa AuthService para recuperar el token
                token = await AuthService.GetToken();

                employeersClient.baseEndPoint = configuration["TokenServiceSettings:UrlEmployeers"];
                employeersClient.ClientToken = new ClientToken() { Token = token };

                var response = await employeersClient.ExportData().ConfigureAwait(false);

                if (response.StatusCode == 200)
                {

                    // Leer el archivo como bytes
                    var fileBytes = response.Payload;

                    // Invocar el método de JavaScript para crear el archivo descargable
                    await JS.InvokeVoidAsync("BlazorDownloadFile", "archivo.xlsx", "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", fileBytes);


                }

                Console.WriteLine("Error al descargar el archivo.");

            }

            Console.WriteLine("Permisos insuficientes");

        }

        private void AbrirModal()
        {
            mostrarModal = true;
        }

        private void CerrarModal()
        {
            mostrarModal = false;
        }

        private void HandleFileSelected(InputFileChangeEventArgs e)
        {
            // Obtener el archivo seleccionado
            archivoSeleccionado = e.File;
        }

        private async Task OpenModalToUploadFile()
        {
            if (archivoSeleccionado != null)
            {
                // Leer el archivo en un array de bytes
                var buffer = new byte[archivoSeleccionado.Size];
                await archivoSeleccionado.OpenReadStream().ReadAsync(buffer);

                // Lógica para procesar o subir el archivo (por ejemplo, enviar a una API)
                Console.WriteLine($"Archivo {archivoSeleccionado.Name} cargado correctamente.");

                // Cerrar el modal
                mostrarModal = false;
            }
        }

        #endregion


        // Clase de empleado para el formulario
        public class Employee
        {
            public int IdEmpleado { get; set; }

            [Required(ErrorMessage = "El nombre es obligatorio")]
            public string Nombre { get; set; }

            [Required(ErrorMessage = "El RFC es obligatorio")]
            public string Rfc { get; set; }

            [Required(ErrorMessage = "La fecha de nacimiento es obligatoria")]
            public DateTime FechaNacimiento { get; set; }
        }
    }
}
