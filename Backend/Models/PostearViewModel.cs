using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace BackEnd.Models
{
    public class PostearViewModel
    {
        [Required(ErrorMessage = "Por favor, seleccione una imagen.")]
        public IFormFile Imagen { get; set; }

        [StringLength(500, ErrorMessage = "La descripción no puede exceder los 500 caracteres.")]
        public string Descripcion { get; set; }
    }
}