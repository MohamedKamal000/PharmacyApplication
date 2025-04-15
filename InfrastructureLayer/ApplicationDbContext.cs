

using DomainLayer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace InfrastructureLayer
{
    public class ApplicationDbContext : DbContext
    {
        
        private readonly string _connectionString;
        public ApplicationDbContext(IOptions<DataBaseOptions> options)
        {
            _connectionString = options.Value.SqlConnection;
        }
        
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(_connectionString);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().HasIndex(u => u.PhoneNumber).IsUnique();
            modelBuilder.Entity<Delivery>().HasIndex(d => d.PhoneNumber).IsUnique();
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Product> MedicalProducts { get; set; }
        public DbSet<Delivery> DeliveryMen { get; set; }
        public DbSet<MedicalCategory> MedicalCategories { get; set; }
        public DbSet<SubMedicalCategory> SubMedicalCategories { get; set; }

        public DbSet<OrderStatus> OrderStatus { get; set; }
    }
}
