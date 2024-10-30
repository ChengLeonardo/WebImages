using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace BackEnd.Models
{
    public class PostearViewModel
    {
        [Required(ErrorMessage = "Por favor, seleccione una imagen.")]
        public IFormFile Imagen { get; set; }

        [StringLength(500, ErrorMessage = "La descripción no puede exceder los 500 caracteres.")]
        public string? Descripcion { get; set; }
        [Required(ErrorMessage = "Por favor, introduzca un título.")]
        [StringLength(100, ErrorMessage = "El título no puede exceder los 100 caracteres.")]
        public string Titulo { get; set; }
    }
}