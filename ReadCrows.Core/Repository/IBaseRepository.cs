

using ReadCrows.Core.Models;
using System.Linq.Expressions;

namespace ReadCrows.Core.Repository
{
    public interface IBaseRepository<TEntity> where TEntity : class

    {
        Task<OperationResult> Save(TEntity entity);


        Task<List<TEntity>> GetEntities(PaginParams paginParams);
        Task<List<TEntity>> GetEntities();
        Task<TEntity> GetEntity(int Id);
        Task<bool> Exists(Expression<Func<TEntity, bool>> filter);
       Task<List<TEntity>> FindAll(Expression<Func<TEntity, bool>> filter);
    }
}
