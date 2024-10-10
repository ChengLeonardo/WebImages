namespace BackEnd.Models;

public class PerfilViewModel
{
    public Usuario Usuario { get; set; }
    public List<Post> PostsUsuario { get; set; }
    public bool EsUsuarioActual { get; set; }
}
