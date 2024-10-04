using System.ComponentModel.DataAnnotations;

namespace BackEnd.Models;

public class LoginViewModel
{
    [Required]
    [StringLength(255)]
    public string NombreUsuario { get; set; }
    [StringLength(255)]
    public string Contrasena { get; set; }
}
