using System.ComponentModel.DataAnnotations;

namespace Api.Models.Request
{
    public class EmployeeIdRequest
    {
        [Required(ErrorMessage = "id de empleado requerido")] // Campo obligatorio
        public int EmployeeId { get; set; }
    }
}
