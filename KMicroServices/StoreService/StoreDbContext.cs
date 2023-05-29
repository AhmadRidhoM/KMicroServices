using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage;
using StoreService.Models;

namespace StoreService
{
    public class StoreDbContext : DbContext
    {
        public StoreDbContext(DbContextOptions<StoreDbContext> options) : base(options) 
        {
			try
			{
				var dbCreator = Database.GetService<IDatabaseCreator>() as RelationalDatabaseCreator;
				if (dbCreator != null)
				{
					if (!dbCreator.CanConnect()) dbCreator.Create();
					if (!dbCreator.HasTables()) dbCreator.CreateTables();

				}
			}
			catch (Exception ex)
			{
                Console.WriteLine(ex.Message);
            }
        }

        public DbSet<Store> Stores { get; set; }
    }
}
