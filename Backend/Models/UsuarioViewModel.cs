using System.ComponentModel.DataAnnotations;

namespace BackEnd.Models;

    public class UsuarioViewModel
    {
        public string? NombreUsuario { get; set; }
        public string? Descripcion { get; set; }

        public string? Nombre { get; set; }

        public string? Apellido { get; set; }
        
        public string? Email { get; set; }
        public string? FotoPerfil { get; set; }
        public string Que { get; set; }
    }
