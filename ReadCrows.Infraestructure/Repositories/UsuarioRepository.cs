using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using ReadCrows.Core.Entities;
using ReadCrows.Core.Models;
using ReadCrows.Infraestructure.Context;
using ReadCrows.Infraestructure.Core;
using ReadCrows.Infraestructure.Interfaces;


namespace ReadCrows.Infraestructure.Repositories
{
    /// <summary>
    /// Repositorio para manejar las operaciones que son exclusivas de los usuarios
    /// </summary>
    public class UsuarioRepository : BaseRepository<Usuario>, IUsuarioRepository
    {
        private readonly ReadCrowsContext context;
        private readonly ILogger<UsuarioRepository> logger;

        public UsuarioRepository(ReadCrowsContext context, ILogger<UsuarioRepository> logger) : base(context)
        {
            this.context = context;
            this.logger = logger;
        }

        public async Task<OperationResult> GetUserInfo(string email)
        {
            OperationResult result = new OperationResult();
            try
            {
                var usuario = await this.context.Usuarios.FirstOrDefaultAsync(us => us.Correo == email);

                result.Data = usuario;
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = "Error obteniendo la información del usuario";
                this.logger.LogError($"{result.Message}", ex.Message);
            }
            return result;
        }

        public async override Task<OperationResult> Save(Usuario entity)
        {
            OperationResult operationResult = new OperationResult();

            try
            {
                ArgumentNullException.ThrowIfNull(entity);

               
                if (entity.Id == 0)
                {
                    this.context.Usuarios.Add(entity);
                    operationResult.Message = "Usuario guardado correctamente.";
                }
               
                else
                {
                    // Actualizar
                    Usuario usuarioToUpdate = await base.GetEntity(entity.Id);
                   
                    usuarioToUpdate.Edad = entity.Edad;
                    usuarioToUpdate.Correo = entity.Correo;
                    usuarioToUpdate.Nombre = entity.Nombre;
                    usuarioToUpdate.Id = entity.Id;
                    operationResult.Message = "Usuario actualizado correctamente.";

                }

              
                await this.context.SaveChangesAsync();

               
            }
            catch (Exception)
            {

                throw;
            }
            return operationResult;


        }
        


    }
}
