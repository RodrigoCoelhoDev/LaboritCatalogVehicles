using Microsoft.EntityFrameworkCore;

namespace LaboritCatalogVehicles.Models
{
    public class CatalogContext : DbContext
    {
        public CatalogContext(DbContextOptions<CatalogContext> options) :
            base(options)
        {

        }

        public DbSet<Brand> Brand { get; set; }
        public DbSet<Model> Models { get; set; }
        public DbSet<Vehicle> Vehicles { get; set; }

        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //    modelBuilder.Entity<Vehicle>()
        //        .HasOne(b => b.Brand)
        //       .WithMany(a => a.Vehicles)
        //       .OnDelete(DeleteBehavior.NoAction);

        //    modelBuilder.Entity<Vehicle>()
        //    .HasOne(b => b.Model)
        //    .WithMany(a => a.Vehicles)
        //    .OnDelete(DeleteBehavior.NoAction);
        //}
    }
}
