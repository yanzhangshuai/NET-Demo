using Blog.Core.Model.interfaces;
using Blog.Core.Model.Models;
using Microsoft.EntityFrameworkCore;

namespace Blog.Core.Entityframework
{
    public class BlogDbContext:DbContext
    {
        public BlogDbContext(DbContextOptions<BlogDbContext> options)
            : base(options)
        {
        }
        public DbSet<Advertisement> Advertisements { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            foreach (var entityType in modelBuilder.Model.GetEntityTypes())
            {

                if (typeof(ISoftDelete).IsAssignableFrom(entityType.ClrType))
                    entityType.AddSoftDeleteQueryFilter();
            }
            base.OnModelCreating(modelBuilder);
        }
    }
}