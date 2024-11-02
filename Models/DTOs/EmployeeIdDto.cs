using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.DTOs
{
    public class EmployeeIdDto
    {
        [Required(ErrorMessage = "id de empleado requerido")] // Campo obligatorio
        public int EmployeeId { get; set; }
    }
}
