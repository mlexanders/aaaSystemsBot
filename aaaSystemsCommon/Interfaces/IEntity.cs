namespace aaaSystemsCommon.Interfaces
{
    public interface IEntity<TKey>
    {
        TKey Id { get; }
    }
}