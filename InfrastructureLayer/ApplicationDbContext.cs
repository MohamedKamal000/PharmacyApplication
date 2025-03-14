

using DomainLayer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace InfrastructureLayer
{
    public class ApplicationDbContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var configurationBuilder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("db_config.json").Build();

            var connectionString = configurationBuilder.GetConnectionString("SqlConnection");


            optionsBuilder.UseSqlServer(connectionString);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Users>().HasIndex(u => u.PhoneNumber).IsUnique();
            modelBuilder.Entity<Delivery>().HasIndex(d => d.PhoneNumber).IsUnique();
        }

        public DbSet<Users> Users { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Product> MedicalProducts { get; set; }
        public DbSet<Delivery> DeliveryMen { get; set; }
        public DbSet<MedicalCategory> MedicalCategories { get; set; }
        public DbSet<SubMedicalCategory> SubMedicalCategories { get; set; }

        public DbSet<OrderStatus> OrderStatus { get; set; }
    }
}
