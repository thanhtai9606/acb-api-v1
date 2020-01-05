using System;
using BecamexIDC.Pattern.EF.Factory;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace acb_app.Models
{
    public partial class ACBSystemContext : DataContext
    {
        public ACBSystemContext(DbContextOptions<ACBSystemContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Customer> Customer { get; set; }
        public virtual DbSet<Product> Product { get; set; }
       // public virtual DbSet<Sale> Sale { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {

            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Customer>(entity =>
            {
                entity.ToTable("customer");

                entity.HasIndex(e => e.CustomerId)
                    .HasName("customer_id");

                entity.Property(e => e.CustomerId)
                    .HasColumnName("customer_id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Address)
                    .IsRequired()
                    .HasColumnName("address")
                    .HasColumnType("text")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_vietnamese_ci");

                entity.Property(e => e.CustomerName)
                    .IsRequired()
                    .HasColumnName("customer_name")
                    .HasColumnType("text")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_vietnamese_ci");

                entity.Property(e => e.ModifiedDate)
                    .HasColumnName("modified_date")
                    .HasColumnType("timestamp")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP");

                entity.Property(e => e.Phone)
                    .IsRequired()
                    .HasColumnName("phone")
                    .HasColumnType("varchar(20)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_vietnamese_ci");
            });

            modelBuilder.Entity<Product>(entity =>
            {
                entity.ToTable("product");

                entity.HasIndex(e => e.ProductId)
                    .HasName("product_id");

                entity.Property(e => e.ProductId)
                    .HasColumnName("product_id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Model)
                    .IsRequired()
                    .HasColumnName("model")
                    .HasColumnType("text")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_vietnamese_ci");

                entity.Property(e => e.ModifiedDate)
                    .HasColumnName("modified_date")
                    .HasColumnType("timestamp")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP");

                entity.Property(e => e.ProductName)
                    .IsRequired()
                    .HasColumnName("product_name")
                    .HasColumnType("text")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_vietnamese_ci");

                entity.Property(e => e.Inventory)
                    .HasColumnName("inventory")
                    .HasColumnType("smallint(6)");

                entity.Property(e => e.Warranty)
                    .HasColumnName("warranty")
                    .HasColumnType("int(11)");
            });

            // modelBuilder.Entity<Sale>(entity =>
            // {
            //     entity.HasKey(e => e.SoId)
            //         .HasName("PRIMARY");

            //     entity.ToTable("sale");

            //     entity.HasIndex(e => e.CustomerId)
            //         .HasName("customer_id");

            //     entity.HasIndex(e => e.ProductId)
            //         .HasName("product_id");

            //     entity.Property(e => e.SoId)
            //         .HasColumnName("so_id")
            //         .HasColumnType("int(11)");

            //     entity.Property(e => e.CreateBy)
            //         .IsRequired()
            //         .HasColumnName("create_by")
            //         .HasColumnType("varchar(20)")
            //         .HasCharSet("utf8mb4")
            //         .HasCollation("utf8mb4_vietnamese_ci");

            //     entity.Property(e => e.CustomerId)
            //         .HasColumnName("customer_id")
            //         .HasColumnType("int(11)");

            //     entity.Property(e => e.ModifiedDate)
            //         .HasColumnName("modified_date")
            //         .HasColumnType("timestamp")
            //         .HasDefaultValueSql("CURRENT_TIMESTAMP");

            //     entity.Property(e => e.ProductId)
            //         .HasColumnName("product_id")
            //         .HasColumnType("int(11)");

            //     entity.Property(e => e.WarrantyEnd)
            //         .HasColumnName("warranty_end")
            //         .HasColumnType("datetime");

            //     entity.Property(e => e.Quantity)
            //         .HasColumnName("quantity")
            //         .HasColumnType("smallint(6)");

            //     entity.Property(e => e.WarrantyStart)
            //         .HasColumnName("warranty_start")
            //         .HasColumnType("datetime");
            // });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
