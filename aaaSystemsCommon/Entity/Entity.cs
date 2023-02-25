namespace aaaSystemsCommon.Entity
{
    public abstract class Entity<TKey>
    {
        public abstract TKey PK { get; set; }
    }
}