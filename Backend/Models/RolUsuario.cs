using System.ComponentModel.DataAnnotations;
namespace BackEnd.Models;
public class RolUsuario
{
    [Key]
    public uint IdRol { get; set; }

    [Required]
    [StringLength(50)]
    public string Descripcion { get; set; }
}
