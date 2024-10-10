using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BackEnd.Models;
public class Post
{
    [Key]
    public uint IdPost { get; set; }

    [Required]
    [StringLength(255)]
    public string UrlImagen { get; set; } // URL de la imagen

    public uint IdUsuario { get; set; }

    [Required]
    public List<UsuarioLikes> ListLikes { get; set; } = new List<UsuarioLikes>();

    [Required]
    public DateTime FechaPublicacion { get; set; }

    [ForeignKey("IdUsuario")]
    public Usuario? Usuario { get; set; }
}
