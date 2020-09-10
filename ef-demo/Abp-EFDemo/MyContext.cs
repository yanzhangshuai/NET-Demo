
using Microsoft.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace Abp_EFDemo
{
    public class MyContext:AbpDbContext<MyContext>
    {
        public  DbSet<Book> Books { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
            => options.UseSqlite("Data Source=../../../blogging2.db");

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            
        }

        public MyContext()
            : base(new DbContextOptions<MyContext>())
        {
            
        }

        public MyContext(DbContextOptions<MyContext> options) : base(options)
        {
        }
    }
}