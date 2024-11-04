using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.DTOs
{
    public class Employee2RequestDto
    {
        [Required(ErrorMessage = "id de empleado requerido")] // Campo obligatorio
        public int EmployeeId { get; set; }

        [Required(ErrorMessage = "El nombre de usuario es obligatorio")] // Campo obligatorio
        [StringLength(50, MinimumLength = 3, ErrorMessage = "El nombre debe tener entre 3 y 50 caracteres")]
        public string? Name { get; set; }

        [Required(ErrorMessage = "RFC es obligatorio")] // Campo obligatorio
        [StringLength(12, MinimumLength = 3, ErrorMessage = "El RFC tener entre 3 y 12 caracteres")]
        public string? Rfc { get; set; }

        // [DataType(DataType.Date)]
        [Display(Name = "Fecha de Registro")]
        public DateTime DateBirth { get; set; }
    }

}
