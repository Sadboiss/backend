using Microsoft.EntityFrameworkCore;
using WebApi.Entities;

namespace WebApi.Helpers
{
    public class SadboisContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Address> Addresses { get; set; }
        public DbSet<Item> Items { get; set; }
        public DbSet<Inventory> Inventories { get; set; }
        public DbSet<InventoryItem> InventoryItems { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Transaction> Transactions { get; set; }

        public SadboisContext(DbContextOptions<SadboisContext> options) : base(options) { }
        
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseMySql("server=localhost;database=mtlstore;user=tony;pwd=habibiana;");
            }
        }
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .HasOne(u => u.Address)
                .WithMany()
                .HasForeignKey(u => u.AddressId);

            modelBuilder.Entity<Inventory>()
                .HasMany(i => i.InventoryItems)
                .WithOne(i => i.Inventory);

            modelBuilder.Entity<Inventory>()
                .HasOne(i => i.User)
                .WithMany()
                .HasForeignKey(i => i.UserId)
                .IsRequired();

            modelBuilder.Entity<InventoryItem>()
                .HasOne(ii => ii.Inventory)
                .WithMany()
                .HasForeignKey(ii => ii.InventoryId)
                .IsRequired();

            modelBuilder.Entity<InventoryItem>()
                .HasOne(ii => ii.Item)
                .WithMany()
                .HasForeignKey(ii => ii.ItemId)
                .IsRequired();
        }
    }
}