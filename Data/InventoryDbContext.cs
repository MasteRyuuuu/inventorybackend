using Microsoft.EntityFrameworkCore;
using InventorySystem.Models;

namespace InventorySystem.Data
{
    public class InventoryDbContext : DbContext
    {
        public InventoryDbContext(DbContextOptions<InventoryDbContext> options) : base(options) { }

        public DbSet<Material> Materials { get; set; }
        public DbSet<Inventory> Inventory { get; set; }
        public DbSet<Transaction> Transactions { get; set; }
        public DbSet<Adjustment> Adjustments { get; set; }
        public DbSet<Barcode> Barcodes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
{
    modelBuilder.Entity<Material>(entity =>
    {
        entity.ToTable("materials");
        entity.Property(e => e.Id).HasColumnName("id");
        entity.Property(e => e.Name).HasColumnName("name");
        entity.Property(e => e.Unit).HasColumnName("unit");
        entity.Property(e => e.PerPackageQty).HasColumnName("per_package_qty");
        entity.Property(e => e.Threshold).HasColumnName("threshold");
    });

    modelBuilder.Entity<Inventory>(entity =>
    {
        entity.ToTable("inventory");
        entity.Property(e => e.Id).HasColumnName("id");
        entity.Property(e => e.MaterialId).HasColumnName("material_id");
        entity.Property(e => e.Quantity).HasColumnName("quantity");
        entity.Property(e => e.UpdatedAt).HasColumnName("updated_at");
    });

    modelBuilder.Entity<Transaction>(entity =>
    {
        entity.ToTable("transactions");
        entity.Property(e => e.Id).HasColumnName("id");
        entity.Property(e => e.MaterialId).HasColumnName("material_id");
        entity.Property(e => e.Operation).HasColumnName("operation");
        entity.Property(e => e.Quantity).HasColumnName("quantity");
        entity.Property(e => e.Barcode).HasColumnName("barcode");
        entity.Property(e => e.Source).HasColumnName("source");
        entity.Property(e => e.Operator).HasColumnName("operator");
        entity.Property(e => e.CreatedAt).HasColumnName("created_at");
    });

    modelBuilder.Entity<Adjustment>(entity =>
    {
        entity.ToTable("adjustments");
        entity.Property(e => e.Id).HasColumnName("id");
        entity.Property(e => e.MaterialId).HasColumnName("material_id");
        entity.Property(e => e.OldQuantity).HasColumnName("old_quantity");
        entity.Property(e => e.NewQuantity).HasColumnName("new_quantity");
        entity.Property(e => e.Reason).HasColumnName("reason");
        entity.Property(e => e.Operator).HasColumnName("operator");
        entity.Property(e => e.Remark).HasColumnName("remark");
        entity.Property(e => e.AdjustedAt).HasColumnName("adjusted_at");
    });

    modelBuilder.Entity<Barcode>(entity =>
    {
        entity.ToTable("barcodes");
        entity.Property(e => e.Id).HasColumnName("id");
        entity.Property(e => e.MaterialId).HasColumnName("material_id");
        entity.Property(e => e.Code).HasColumnName("barcode");
        entity.Property(e => e.Note).HasColumnName("note");
    });
}

    }
}
