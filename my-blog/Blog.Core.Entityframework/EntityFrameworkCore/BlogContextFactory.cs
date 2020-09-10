using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Blog.Core.Entityframework.EntityFrameworkCore
{
    public class BlogContextFactory: IDesignTimeDbContextFactory<BlogDbContext>
    {
        public BlogDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<BlogDbContext>();
            optionsBuilder.UseMySql("server=47.104.206.56;user=yan;pwd=yan;database=blog;");

            return new BlogDbContext(optionsBuilder.Options);
        }
    }
   
}