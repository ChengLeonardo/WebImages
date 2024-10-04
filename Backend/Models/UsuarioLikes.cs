using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace BackEnd.Models;
public class UsuarioLikes
{
    [Key]
    public uint IdUsuario { get; set; }

    [Key]
    public uint IdPost { get; set; }

    [ForeignKey("IdUsuario")]
    public Usuario? Usuario { get; set; }

    [ForeignKey("IdPost")]
    public Post? Post { get; set; }
}
