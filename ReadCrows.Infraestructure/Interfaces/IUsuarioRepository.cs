

using ReadCrows.Core.Entities;
using ReadCrows.Core.Models;
using ReadCrows.Core.Repository;

namespace ReadCrows.Infraestructure.Interfaces
{
    /// <summary>
    /// Repositorio para manejar las operaciones que son exclusivas de los usuarios
    /// </summary>
    public interface IUsuarioRepository : IBaseRepository<Usuario>
    {
        public Task<OperationResult> GetUserInfo(string email);
    }
}
