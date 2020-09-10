using System;
using Volo.Abp;
using Volo.Abp.Auditing;
using Volo.Abp.Domain.Entities;

namespace Abp_EFDemo
{
    public class Book:Entity,ISoftDelete,IHasCreationTime
    {
         public int BookId { get; set; }
         public string Title { get; set; }
        public bool SoftDeleted { get; set; }
        public override object[] GetKeys()
        {
            return null;
        }

        public bool IsDeleted { get; set; }
        public DateTime CreationTime { get; set; }
    }
}