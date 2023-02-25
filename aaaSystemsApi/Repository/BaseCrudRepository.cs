using aaaSystemsCommon.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace aaaSystemsApi.Repository
{
    public class BaseCrudRepository<TEntity, TKey> where TEntity : class, IEntity<TKey>
    {
        private readonly AppDbContext dbContext;
        private readonly DbSet<TEntity> dbSet;

        public BaseCrudRepository(AppDbContext dbContext)
        {
            this.dbContext = dbContext;
            dbSet = dbContext.Set<TEntity>();
        }

        public virtual Task Create(TEntity entities)
        {
            return Create(new List<TEntity>() { entities });
        }

        public virtual async Task Create(List<TEntity> entities)
        {
            await dbSet.AddRangeAsync(entities);
            await dbContext.SaveChangesAsync();
        }

        public virtual async Task<TEntity> ReadFirst(Expression<Func<TEntity, bool>> query, params Expression<Func<TEntity, object>>[] includedProperties)
        {
            var entity = await IncludeProperties(includedProperties).FirstOrDefaultAsync(query);
            if (entity != null) dbContext.Entry(entity).State = EntityState.Detached;
            return entity;
        }

        public virtual async Task<List<TEntity>> Read(Func<TEntity, bool> query = null!, params Expression<Func<TEntity, object>>[] includedProperties)
        {
            var entities = query != null ? IncludeProperties(includedProperties).Where(query).ToList() : IncludeProperties(includedProperties).ToList();
            foreach (var entity in entities)
            {
                dbContext.Entry(entity).State = EntityState.Detached;
            }
            return entities;
        }

        public virtual async Task Update(TEntity entity)
        {
            dbContext.Attach(entity);
            MarkModified(entity);
            dbSet.Update(entity);
            await dbContext.SaveChangesAsync();
            dbContext.Entry(entity).State = EntityState.Detached;
        }

        public virtual Task Delete(TKey id)
        {
            return Delete(entity => entity.Id.Equals(id));
        }

        public virtual async Task Delete(Func<TEntity, bool> query)
        {
            dbSet.RemoveRange(dbSet.Where(query));
            await dbContext.SaveChangesAsync();
        }

        protected void MarkModified(TEntity entity)
        {
            var collections = dbContext.Entry(entity).Collections;
            foreach (var collection in collections)
            {
                if (collection.CurrentValue == null) continue;
                foreach (var element in collection.CurrentValue)
                {
                    if (dbContext.Entry(element).State == EntityState.Unchanged)
                    {
                        dbContext.Entry(element).State = EntityState.Modified;
                    }
                }
            }

            foreach (var element in dbContext.Entry(entity).References)
            {
                if (element.CurrentValue == null)
                {
                    continue;
                }

                if (dbContext.Entry(element.CurrentValue).State == EntityState.Unchanged)
                {
                    dbContext.Entry(element.CurrentValue).State = EntityState.Modified;
                }
            }
        }

        protected IQueryable<TEntity> IncludeProperties(Expression<Func<TEntity, object>>[] includeProperties)
        {
            return includeProperties.Aggregate(dbSet.AsNoTracking(), (query, includeProperty) => query.Include(includeProperty));
        }
    }
}
