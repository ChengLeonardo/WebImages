using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BackEnd.Models;
public class Usuario
{
    [Key]
    public uint IdUsuario { get; set; }

    [Required]
    [StringLength(50)]
    public string Nombre { get; set; }

    [Required]
    [StringLength(50)]
    public string Apellido { get; set; }

    [Required]
    [StringLength(255)]
    public string NombreUsuario { get; set; }

    [Required]
    [StringLength(255)]
    public string Contrasena { get; set; }

    [Required]
    [StringLength(100)]
    public string Email { get; set; }

    [StringLength(255)]
    public string? FotoPerfil { get; set; } // URL de la imagen de perfil

    public uint? IdRol { get; set; }

    [ForeignKey("IdRol")]
    public RolUsuario? Rol { get; set; }
}
