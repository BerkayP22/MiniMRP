using Microsoft.EntityFrameworkCore;
using MiniMRP.Core.Entities;

namespace MiniMRP.Web.Data;


public class MrpDbContext : DbContext
{
    public MrpDbContext(DbContextOptions<MrpDbContext> options) : base(options) { }
    public DbSet<Order> Orders => Set<Order>();
    public DbSet<Material> Materials => Set<Material>();
    public DbSet<Product> Products => Set<Product>();
    public DbSet<RecipeItem> RecipeItems => Set<RecipeItem>();


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Material>().HasIndex(m => m.Code).IsUnique();
        modelBuilder.Entity<Product>().HasIndex(p => p.Code).IsUnique();
        modelBuilder.Entity<RecipeItem>().HasIndex(r => new { r.ProductId, r.MaterialId }).IsUnique();
        modelBuilder.Entity<Order>().HasIndex(o => new { o.No, o.PositionNo }).IsUnique();

        modelBuilder.Entity<Order>()
          .HasOne(o => o.Product)
          .WithMany()
          .HasForeignKey(o => o.ProductId)
          .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<RecipeItem>()
     .HasOne(r => r.Product)                  // RecipeItem'daki navigation property
     .WithMany(p => p.RecipeItems)                // Product'taki liste
     .HasForeignKey(r => r.ProductId)           // RecipeItem'daki FK sayısı
     .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<RecipeItem>()
    .HasOne(r => r.Material)
    .WithMany(r => r.RecipeItems)                // Material'daki liste
    .HasForeignKey(m => m.MaterialId)
    .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Material>().HasData(
            new Material { Id = 1, Code = "TBL", FullName = "Tabla", Unit = "Adet", StockQuantity = 10 },
            new Material { Id = 2, Code = "AYK", FullName = "Ayaklık", Unit = "Adet", StockQuantity = 50 },
            new Material { Id = 3, Code = "VDA", FullName = "Vida", Unit = "Adet", StockQuantity = 100 }
        );

        modelBuilder.Entity<Product>().HasData(
            new Product { Id = 1, Code = "MASA", Name = "Masa" }
        );

        modelBuilder.Entity<RecipeItem>().HasData(
            new RecipeItem { Id = 1, ProductId = 1, MaterialId = 1, Quantity = 1 },
            new RecipeItem { Id = 2, ProductId = 1, MaterialId = 2, Quantity = 4 },
            new RecipeItem { Id = 3, ProductId = 1, MaterialId = 3, Quantity = 10 }
        );

        modelBuilder.Entity<Order>().HasData(
            new Order { Id = 1, No = "SIP-001", PositionNo = "10", ProductId = 1, Quantity = 15 }
        );

    }
}

