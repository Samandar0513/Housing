using Housing.Models;
using Microsoft.EntityFrameworkCore;

namespace Housing.Context
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }
        public DbSet<User> Users { get; set; }
        public DbSet<Amenity> Amenities { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<District> Districts { get; set; }
        public DbSet<Region> Regions { get; set; }
        public DbSet<Property> Properties { get; set; }
        public DbSet<PropertyAmenity> PropertyAmenities { get; set; }
        public DbSet<PropertyPhoto> PropertyPhotos { get; set; }
        public DbSet<PropertyDocument> PropertyDocuments { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<PropertyAmenity>()
                .HasKey(pa => new { pa.PropertyId, pa.AmenityId });
            modelBuilder.Entity<User>()
                .HasIndex(u => u.Email)
                .IsUnique();
            modelBuilder.Entity<User>()
                .HasIndex(u => u.Phone)
                .IsUnique();

            modelBuilder.Entity<Property>()
            .Property(p => p.PropertyType)
            .HasConversion<string>()
            .HasMaxLength(20);

            modelBuilder.Entity<Property>()
                .Property(p => p.Currency)
                .HasConversion<string>()
                .HasMaxLength(10);
        }
    }
}
