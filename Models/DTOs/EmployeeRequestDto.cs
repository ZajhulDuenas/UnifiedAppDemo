using Newtonsoft.Json;

namespace Models.DTOs
{
    public class EmployeeRequestDto
    {
        [JsonProperty("idEmpleado")]
        public int Id;

        [JsonProperty("nombre")]
        public string Name;

        [JsonProperty("rfc")]
        public string Rfc;

        [JsonProperty("fechaNacimiento")]
        public DateTime DateBirth;
    }
}


