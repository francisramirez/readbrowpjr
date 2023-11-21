

using Microsoft.EntityFrameworkCore;
using ReadCrows.Core.Models;
using ReadCrows.Core.Repository;
using ReadCrows.Infraestructure.Context;
using System.Linq.Expressions;

namespace ReadCrows.Infraestructure.Core
{
    public abstract class BaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity : class
    {
        private readonly ReadCrowsContext context;
        private DbSet<TEntity> entities;

        public BaseRepository(ReadCrowsContext context) 
        {
            this.context = context;
            this.entities = this.context.Set<TEntity>();
        }
        public async virtual Task<bool> Exists(Expression<Func<TEntity, bool>> filter) => await this.entities.AnyAsync(filter);

        public async virtual Task<List<TEntity>> FindAll(Expression<Func<TEntity, bool>> filter) => await this.entities.Where(filter).ToListAsync();

        public async virtual Task<List<TEntity>> GetEntities(PaginParams paginParams) 
        {

            var query =  await this.entities.Skip((paginParams.PageNumber - 1) * paginParams.PageNumber)
                                      .Take(paginParams.PageNumber).ToListAsync();


            return query;
           
        }

        public async Task<List<TEntity>> GetEntities()
        {
            return await this.entities.ToListAsync();
        }

        public async virtual Task<TEntity> GetEntity(int Id)
        {
            TEntity? entity = await entities.FindAsync(Id);
            return entity;
        }

        public async virtual Task<OperationResult> Save(TEntity entity)
        {
            OperationResult operationResult = new OperationResult();

            try
            {
                this.entities.Add(entity);
                await this.context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                operationResult.Message = $"Error: { ex.Message } guardando el usuario";
                operationResult.Success = false; 


            }
            return operationResult;
        }

       
    }
}
