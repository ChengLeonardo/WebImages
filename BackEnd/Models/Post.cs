namespace BackEnd.Models;

public class Post
{
    public uint IdPost { get; set; }
    public uint IdUsuario { get; set; }
    public string UrlImagen { get; set; }
    public uint CantidadLikes { get; set; }
    public DateTime FechaPublicacion { get; set; }    
}