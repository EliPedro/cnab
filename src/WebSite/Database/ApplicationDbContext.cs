using WebSite.Entities;
using Microsoft.EntityFrameworkCore;

namespace WebSite.Database;

public class ApplicationDbContext(DbContextOptions options) : DbContext(options)
{
    public DbSet<Store> Stores => Set<Store>();
    public DbSet<Transaction> Transactions => Set<Transaction>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<Store>(entity =>
        {
            entity.ToTable("Store");
            entity.HasIndex(e => new { e.Name, e.Owner }).IsUnique();
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Name).IsRequired();
            entity.Property(e => e.Owner).IsRequired();
        });

        modelBuilder.Entity<Transaction>(entity =>
        {
            entity.ToTable("Transaction");
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Cpf).IsRequired();
            entity.Property(e => e.Card).IsRequired();
            entity.HasOne(e => e.Store)
            .WithMany(e => e.Transactions)
            .HasForeignKey(e => e.StoreId);
        });
    }
}

