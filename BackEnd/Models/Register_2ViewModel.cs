using System.ComponentModel.DataAnnotations;

namespace BackEnd.Models;

public class Register_2ViewModel
{
    [Required]
    [EmailAddress(ErrorMessage = "El formato del correo no es válido.")]
    public string Email { get; set; }
    
    [Required(ErrorMessage = "La contraseña es obligatoria.")]
    public string Contrasena { get; set; }

    [Required]
    [Compare("Contrasena", ErrorMessage = "Las contraseñas no coinciden.")]
    public string RepetirContrasena { get; set; }
}