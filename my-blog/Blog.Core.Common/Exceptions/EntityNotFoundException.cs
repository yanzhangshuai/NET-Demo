using System;

namespace Blog.Core.Common.Exceptions
{
    public class EntityNotFoundException : Exception
    {
        public Type EntityType { get; set; }

        public object Id { get; set; }

        public EntityNotFoundException()
        {
        }

        public EntityNotFoundException(Type entityType, object id = null, Exception innerException = (Exception) null)
            : base(id == null ? "There is no such an entity given given id. Entity type: " + entityType.FullName : string.Format("There is no such an entity. Entity type: {0}, id: {1}", (object) entityType.FullName, id), innerException)
        {
            EntityType = entityType;
            Id = id;
        }

        public EntityNotFoundException(string message)
            : base(message)
        {
        }

        public EntityNotFoundException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}