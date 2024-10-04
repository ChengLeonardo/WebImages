using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace BackEnd.Models;
public class Seguidor
{
    [Key]
    public uint IdUsuario { get; set; }

    [Key]
    public uint IdUsuarioSeguido { get; set; }

    [Required]
    public DateTime FechaSeguimiento { get; set; }

    [ForeignKey("IdUsuario")]
    public Usuario? Usuario { get; set; }

    [ForeignKey("IdUsuarioSeguido")]
    public Usuario? UsuarioSeguido { get; set; }
}
