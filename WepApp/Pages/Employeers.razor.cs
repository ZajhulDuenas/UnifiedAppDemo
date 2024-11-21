using Front.Infrastructure.ClientApi;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.JSInterop;
using Models.ClientApi;
using Models.DTOs;
using System.ComponentModel.DataAnnotations;
using WepApp.Core.Entities;

namespace WepApp.Pages
{
    public partial class Employeers
    {

        [Inject] protected IJSRuntime JS { get; set; }
        [Inject] protected AuthService AuthService { get; set; }
       
        [Inject] protected NavigationManager Navigation { get; set; }

        [Inject] private IEmployeersClient employeersClient { get; set; } = default!;
        [Inject] public IConfiguration configuration { get; set; } = default!;

        private bool IsLoading { get; set; } = false;
        private bool isAuthenticated { get; set; } = false;
        private int IndexPagination { get; set; } = 1;
        private string message { get; set; } = "";
        private UserEntitie userModel { get; set; } = new UserEntitie();
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

            isAuthenticated = await AuthService.CheckIsAuthenticated();

            if (!isAuthenticated)
            {
                message = "Por favor, inicia sesión para continuar.";
                // Redirigir al login si no está autenticado
                Navigation.NavigateTo("/login", forceLoad: true);
                return false;
            }

            // Usa AuthService para guardar el token
            var token = await AuthService.GetToken();
            employeersClient.ClientToken = new ClientToken() { Token = token };

            message = "";
            userModel = await AuthService.GetInfoUser(); // await VarsService.ExtractObject<UserEntitie>("UserModel");

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
                 var response = await employeersClient.DeleteEmployee(id).ConfigureAwait(false);

                // EmployeeList = response.Payload;
            }
        }

        private async Task ConfirmDeleteAsync()
        {
            var res = await CheckStatusUsr();


            if (res && EmployeeIdToDelete.HasValue)
            {
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

        #region Export
        private async Task ExportList()
        {
            var res = await CheckStatusUsr();

            if (res && userModel.ExportList)
            {

                IsLoading = true; // Muestra el modal
                StateHasChanged(); // Forzar renderización del componente
                string token = "";

                try
                {
                    var response = await employeersClient.ExportData().ConfigureAwait(false);

                    if (response.StatusCode == 200)
                    {
                        byte[] contentStream = response.Payload;

                        var base64Content = Convert.ToBase64String(contentStream);

                        var fileName = "archivo.xlsx"; // Nombre del archivo para la descarga
                        var contentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet"; // Tipo de contenido

                        // Invocar el método de JavaScript para crear el archivo descargable
                        await JS.InvokeVoidAsync("downloadFileFromBlazor", fileName, contentType, base64Content);

                    }

                }
                finally
                {
                    IsLoading = false; // Oculta el modal después de la descarga
                    StateHasChanged(); // Forzar renderización del componente
                }

                Console.WriteLine("Error al descargar el archivo.");
                
            }

            Console.WriteLine("Permisos insuficientes");

        }

        #endregion

        #region Import

        private bool isUploading = false;
        private bool ShowUploadModal = false;
        private IBrowserFile selectedFile ;

        private void ModalImportList()
        {
            ShowUploadModal = true;
        }

        private void CerrarModal()
        {
            ShowUploadModal = false;
        }

        private void HandleFileSelected(InputFileChangeEventArgs e)
        {
            // Obtener el archivo seleccionado
            selectedFile  = e.File;
        }

        private async Task OpenModalToUploadFile()
        {
            if (selectedFile != null)
            {
                isUploading = true;

                var content = new MultipartFormDataContent();

                var fileContent = new StreamContent(selectedFile.OpenReadStream());

                fileContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue(selectedFile.ContentType);

                content.Add(fileContent, "file", selectedFile.Name);

                var response = await employeersClient.ImportData(content).ConfigureAwait(false);

                EmployeeList = new List<EmployeeRequestDto>();

                IndexPagination = 1;
                await refreshEmployeTableAsync();
                // Lógica para procesar o subir el archivo (por ejemplo, enviar a una API)
                Console.WriteLine($"Archivo {selectedFile .Name} cargado correctamente.");

                // Cerrar el modal
                ShowUploadModal = false;
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
