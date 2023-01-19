namespace aaaSystemsCommon.Interfaces
{
    public interface ICrud<TEntity, TKey> where TEntity : class, IEntity<TKey>
    {
        Task<List<TEntity>> Get();
        Task<TEntity> Get(TKey id);
        Task Post(TEntity item);
        Task Post(List<TEntity> item);
        Task Patch(TEntity item);
        Task Delete(TKey key);
    }
}
