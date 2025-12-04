using WebSite.Entities;
using Microsoft.EntityFrameworkCore;

namespace WebSite.Database;
public class ApplicationDbContext(DbContextOptions options) : DbContext(options)
{
    public DbSet<Store> Stores => Set<Store>();
    public DbSet<TransactionStore> Transactions => Set<TransactionStore>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<Store>(entity =>
        {
            entity.ToTable("Store");
            entity.HasIndex(e => e.Name).IsUnique(false);
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Name).IsRequired();
            entity.Property(e => e.Owner).IsRequired();
        });

        modelBuilder.Entity<TransactionStore>(entity =>
        {
            entity.ToTable("TransactionStore");
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Cpf).IsRequired();
            entity.Property(e => e.Card).IsRequired();
            entity.HasOne(e => e.Store)
            .WithMany(e => e.Transactions)
            .HasForeignKey(e => e.StoreId);
            entity.Property(e => e.Amount).HasPrecision(18, 2);
        });
    }
}

