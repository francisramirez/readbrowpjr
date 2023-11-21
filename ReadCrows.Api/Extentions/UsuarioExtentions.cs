using ReadCrows.Api.Models;

namespace ReadCrows.Usuario.Api.Extentions
{
    public static class UsuarioExtentions
    {
        public static Core.Entities.Usuario GetUsuarioFromUsuarioCreateModel(this UsuarioCreateModel usuarioCreate)
        {
            return new Core.Entities.Usuario()
            {
                Correo = usuarioCreate.Correo,
                Edad = usuarioCreate.Edad.Value,
                Nombre = usuarioCreate.Nombre
            };
        }
        public static Core.Entities.Usuario GetUsuarioFromUsuarioUpdateModel(this UsuarioUpdateModel usuarioUpdate)
        {
            return new Core.Entities.Usuario()
            {
                Correo = usuarioUpdate.Correo,
                Edad = usuarioUpdate.Edad.Value,
                Nombre = usuarioUpdate.Nombre,
                Id = usuarioUpdate.UsuarioId
            };
        }
    }
}
