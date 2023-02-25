using aaaSystemsCommon.Entity;

namespace aaaSystemsCommon.Interfaces
{
    public interface ICrud<TEntity, TKey> where TEntity : Entity<TKey>
    {
        Task<List<TEntity>> Get();
        Task<TEntity> Get(TKey id);
        Task Post(TEntity item);
        Task Post(List<TEntity> item);
        Task Patch(TEntity item);
        Task Delete(TKey key);
    }
}
