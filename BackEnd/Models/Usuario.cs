using System.ComponentModel.DataAnnotations;

namespace BackEnd.Models;

public class Usuario
{
    public uint IdUsuario { get; set; }
    public uint IdRol { get; set; } = 2;
    [Required]
    public string Nombre { get; set; }
    [Required]
    public string Apellido { get; set; }
    [Required]
    public string Contrasena { get; set; }
    [Required]
    public string Email { get; set; }
    public string? FotoPerfil { get; set; }    
}