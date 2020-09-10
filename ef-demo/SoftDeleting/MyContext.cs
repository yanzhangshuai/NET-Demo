
using Microsoft.EntityFrameworkCore;

namespace SoftDeleting
{
    public class MyContext:DbContext
    {
        public virtual DbSet<Book> Books { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySql("Server=47.104.206.56;Database=fastconnectdb;charset=utf8;uid=yan;pwd=yan;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Book>().HasQueryFilter(p => p.SoftDeleted);
            base.OnModelCreating(modelBuilder);
        }
    }
}