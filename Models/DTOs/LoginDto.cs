using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.DTOs
{
    public class LoginDto
    {
        [Required(ErrorMessage = "El nombre de usuario es obligatorio")] // Campo obligatorio
        [StringLength(50, MinimumLength = 3, ErrorMessage = "El nombre debe tener entre 3 y 50 caracteres")]
        public string Username { get; set; }

        [Required(ErrorMessage = "La contraseña es obligatoria")]
        [DataType(DataType.Password)] // Especifica el tipo de entrada como contraseña
        [StringLength(100, MinimumLength = 3, ErrorMessage = "La contraseña debe tener al menos 3 caracteres")]
        public string Password { get; set; }
    }
}
