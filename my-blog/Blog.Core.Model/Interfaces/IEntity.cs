namespace Blog.Core.Model.interfaces
{
    public interface IEntity<TKey> : IEntity
    {
        TKey Id { get; set; }
    }
    public interface IEntity
    {
    }
}