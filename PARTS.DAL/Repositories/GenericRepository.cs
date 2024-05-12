using Microsoft.EntityFrameworkCore;
using PARTS.DAL.Data;
using PARTS.DAL.Excepstions;
using PARTS.DAL.Interfaces;

namespace PARTS.DAL.Repositories
{
    public abstract class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : class
    {
        protected readonly PartsDBContext databaseContext;

        protected readonly DbSet<TEntity> table;

        public virtual async Task<IEnumerable<TEntity>> GetAsync() => 
            await table.ToListAsync() ?? throw new EntityNotFoundException("No entities in this table");

        public virtual async Task<TEntity> GetByIdAsync(Guid id)
        {
            return await table.FindAsync(id)
                ?? throw new EntityNotFoundException(GetEntityNotFoundErrorMessage(id));
        }


        public virtual async Task InsertAsync(TEntity entity)
        {
            await table.AddAsync(entity);
            await databaseContext.SaveChangesAsync();
            
        }
        public virtual async Task UpdateAsync(TEntity entity)
        {
            await Task.Run(() => table.Update(entity));
            await databaseContext.SaveChangesAsync();
        }
        public virtual async Task DeleteAsync(Guid id)
        {
            var entity = await GetByIdAsync(id);
            if (entity == null)
                throw new EntityNotFoundException(GetEntityNotFoundErrorMessage(id));
            await Task.Run(() => table.Remove(entity));
            await databaseContext.SaveChangesAsync();
        }

        protected static string GetEntityNotFoundErrorMessage(Guid id) =>
            $"{typeof(TEntity).Name} with id {id.ToString()} not found.";

        public GenericRepository(PartsDBContext databaseContext)
        {
            this.databaseContext = databaseContext;
            table = this.databaseContext.Set<TEntity>();
        }
    }
}
