using Newtonsoft.Json;

namespace Models.DTOs
{
    public class EmployeeRequestDto
    {
        [JsonProperty("idEmpleado")]
        public int IdEmpleado;

        [JsonProperty("nombre")]
        public string Nombre;

        [JsonProperty("rfc")]
        public string Rfc;

        [JsonProperty("fechaNacimiento")]
        public string FechaNacimiento;
    }
}
