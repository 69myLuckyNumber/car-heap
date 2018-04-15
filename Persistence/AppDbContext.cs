using car_heap.Core.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace car_heap.Persistence
{
    public class AppDbContext : IdentityDbContext<ApplicationUser>
    {

        public DbSet<Vehicle> Vehicles { get; set; }

        public DbSet<Make> Makes { get; set; }

        public DbSet<Model> Models { get; set; }

        public DbSet<Order> Orders { get; set; }

        public DbSet<Contact> Contacts { get; set; }

        public DbSet<Feature> Features { get; set; }

        public DbSet<Integration> Integrations { get; set; }

        public DbSet<Photo> Photos { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options): base(options)
        { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // One to one: User - Contact
            modelBuilder.Entity<ApplicationUser>()
                .HasOne(u => u.Contact)
                .WithOne(c => c.Identity)
                .HasForeignKey<Contact>(c => c.IdentityId);
            // One to Many: User - Vehicles 
            modelBuilder.Entity<ApplicationUser>()
                .HasMany(u => u.OfferedVehicles)
                .WithOne(v => v.Identity);
            // Many to Many: Vehicle - Feature 
            modelBuilder.Entity<Integration>()
                .HasKey(i => new { i.FeatureId, i.VehicleId });

            // Many to Many: User - Vehicle
            modelBuilder.Entity<Order>()
                .HasKey(o => new { o.IdentityId, o.VehicleId });

            modelBuilder.Entity<Order>()
                .HasOne(o => o.Vehicle)
                .WithMany(v => v.Orders)
                .HasForeignKey(o => o.VehicleId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Order>()
                .HasOne(o => o.Identity)
                .WithMany(v => v.Orders)
                .HasForeignKey(o => o.IdentityId);

            modelBuilder.Entity<Vehicle>()
                .HasMany(v => v.Photos)
                .WithOne(p => p.Vehicle)
                .HasForeignKey(p=> p.VehicleId)
                .OnDelete(DeleteBehavior.Cascade);
                
            base.OnModelCreating(modelBuilder);

        }
    }
}