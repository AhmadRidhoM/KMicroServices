using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage;
using ProductService.Models;

namespace ProductService
{
    public class ProductDbContext : DbContext
    {
        public ProductDbContext(DbContextOptions<ProductDbContext> options) : base(options) 
        {
            try
            {
                var dbcontext = Database.GetService<IDatabaseCreator>() as RelationalDatabaseCreator;
                if (dbcontext != null)
                {
                    if (!dbcontext.CanConnect()) dbcontext.Create();
                    if (!dbcontext.HasTables()) dbcontext.CreateTables();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            

        }

        public DbSet<Product> Products { get; set; }
    }
}
