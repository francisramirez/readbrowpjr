using System.ComponentModel.DataAnnotations;

namespace ReadCrows.Api.Models
{
    public class UsuarioBaseModel
    {
        [Required(ErrorMessage = "El nombre del usuario es requerido.")]
        [MaxLength(200, ErrorMessage = "Ha excedido la cantidad máxima de carácteres.")]
        public string? Nombre { get; set; }

        [Required(ErrorMessage = "El correo del usuario es requerido.")]
        [MaxLength(200, ErrorMessage = "Ha excedido la cantidad máxima de carácteres.")]
        [EmailAddress(ErrorMessage = "El correo introduccido es inválido")]
        public string? Correo { get; set; }

        [Required(ErrorMessage = "La edad del usuario es requerida.")]
        public int? Edad { get; set; }
    }
}
