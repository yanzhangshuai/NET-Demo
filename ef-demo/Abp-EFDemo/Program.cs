using System;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace Abp_EFDemo
{
    class Program
    {
       async static Task Main(string[] args)
        {
            using (var context = new MyContext(new DbContextOptions<MyContext>()))
            {
               // //  context.Books.Add(new Book()
               // //  {
               // //      Title = "123"
               // //  });
               // // await context.SaveChangesAsync();
               //  context.Books.Remove(Enumerable.ToList(context.Books.Where(y=>true))[2]);
               //  await context.SaveChangesAsync();
               var data = await context.Books.Where(s => true).CountAsync();
                 Console.WriteLine(data);
            }
        }
    }
}