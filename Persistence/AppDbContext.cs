using car_heap.Models;
using Microsoft.EntityFrameworkCore;

namespace car_heap.Persistence
{
    public class AppDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }

        public DbSet<Vehicle> Vehicles { get; set; }

        public DbSet<Make> Makes { get; set; }

        public DbSet<Model> Models { get; set; }

        public DbSet<Order> Orders { get; set; }

        public DbSet<Contact> Contacts { get; set; }

        public DbSet<Integration> Integrations { get; set; }

        public DbSet<Integration> Features { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder) 
        {

            // One to one: User - Contact
            modelBuilder.Entity<User>()
                .HasOne(u => u.Contact)
                .WithOne(c => c.User)
                .HasForeignKey<Contact>(c => c.UserId);
            // One to Many: User - Vehicles 
            modelBuilder.Entity<User>()
                .HasMany(u => u.OfferedVehicles)
                .WithOne(v => v.User);
            // Many to Many: Vehicle - Feature 
            modelBuilder.Entity<Integration>()
                .HasKey(i => new { i.FeatureId, i.VehicleId });

            // Many to Many: User - Vehicle
            modelBuilder.Entity<Order>()
                .HasKey(o => new { o.UserId, o.VehicleId });
            
            modelBuilder.Entity<Order>()
                .HasOne(o => o.Vehicle)
                .WithMany(v => v.Orders)
                .HasForeignKey(o => o.VehicleId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Order>()
                .HasOne(o => o.User)
                .WithMany(v => v.Orders)
                .HasForeignKey(o => o.UserId);
            
        }
    }
}