using System.ComponentModel.DataAnnotations;

namespace BackEnd.Models;

public class Register_1ViewModel
{
    [Required]
    [StringLength(255, ErrorMessage = "La longitud del nombre debe ser menos que {0}")]
    public string Nombre { get; set; }
    [Required]
    [StringLength(255, ErrorMessage = "La longitud del apellido debe ser menos que {0}")]
    public string Apellido { get; set; }
    [Required]
    [StringLength(255, ErrorMessage = "La longitud del NombreUsuario debe ser menos que {0}")]
    public string NombreUsuario { get; set; }

}
