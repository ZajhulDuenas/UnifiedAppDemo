using System.ComponentModel.DataAnnotations;

namespace WepApp.Models
{
    public class LoginModel
    {
        [Required(ErrorMessage = "Usuario Requerido")]
        public string Username { get; set; }
        [Required(ErrorMessage = "Password Requerido")]
        public string Password { get; set; }
    }

}
