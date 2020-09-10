using Blog.Core.Entityframework;
using Microsoft.EntityFrameworkCore;

namespace Blog.Core.IRepository.Base
{
    public interface IDbContextProvider
    {
        DbContext GetDbContext();
    }

    public class DbContextProvider : IDbContextProvider
    {
        private DbContext _context;

        public DbContextProvider(BlogDbContext context)
        {
            _context = context;
        }
        
        public DbContext GetDbContext()
        {
            return _context;
        }
    }
}