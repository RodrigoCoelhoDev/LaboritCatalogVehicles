using Microsoft.EntityFrameworkCore;

namespace LaboritCatalogVehicles.Models
{
    public class CatalogContext : DbContext
    {
        public CatalogContext(DbContextOptions<CatalogContext> options) :
            base(options)
        {

        }

        public DbSet<Brand> Brands { get; set; }
        public DbSet<Model> Models { get; set; }
        public DbSet<Vehicle> Vehicles { get; set; }
    }
}
