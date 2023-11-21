using System.ComponentModel.DataAnnotations;

namespace ReadCrows.Usuario.Api.Models
{
    public class CrearUsuarioToken
    {
        [Required(ErrorMessage = "El correo del usuario es requerido.")]
        [MaxLength(200, ErrorMessage = "Ha excedido la cantidad máxima de carácteres.")]
        [EmailAddress(ErrorMessage = "El correo introduccido es inválido")]
        public string? Correo { get; set; }

       

    }
}
