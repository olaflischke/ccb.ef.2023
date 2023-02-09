using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace NorthwindDal.Model
{

    // Scaffold-DbContext -Connection "datasource=C:\ProgramData\SQLite\data\northwind.db" -Provider Microsoft.EntityFrameworkCore.Sqlite -OutputDir Model -Context NorthwindContext -Tables Customers, Orders, "Order Details", Products

    public partial class NorthwindContext : DbContext
    {
        public NorthwindContext()
        {
        }

        public NorthwindContext(DbContextOptions<NorthwindContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Customer> Customers { get; set; }

        public virtual DbSet<Order> Orders { get; set; }

        public virtual DbSet<OrderDetail> OrderDetails { get; set; }

        public virtual DbSet<Product> Products { get; set; }

        public Action<string> Log { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlite("datasource=C:\\ProgramData\\SQLite\\data\\northwind.db");
                optionsBuilder.LogTo(log => this.Log?.Invoke(log), Microsoft.Extensions.Logging.LogLevel.Information);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            // FluentAPI
            modelBuilder.Entity<Customer>(entity =>
            {
                entity.Property(e => e.CustomerId).HasColumnName("CustomerID");
                entity.HasKey(e => e.CustomerId);
            });

            modelBuilder.Entity<Order>(entity =>
            {
                entity.Property(e => e.OrderId).HasColumnName("OrderID");
                entity.Property(e => e.CustomerId).HasColumnName("CustomerID");
                entity.Property(e => e.EmployeeId).HasColumnName("EmployeeID");
                entity.Property(e => e.Freight)
                    .HasDefaultValueSql("0")
                    .HasColumnType("NUMERIC");
                entity.Property(e => e.OrderDate).HasColumnType("DATETIME");
                entity.Property(e => e.RequiredDate).HasColumnType("DATETIME");
                entity.Property(e => e.ShippedDate).HasColumnType("DATETIME");

                entity.HasOne(d => d.Customer).WithMany(p => p.Orders).HasForeignKey(d => d.CustomerId);
            });

            modelBuilder.Entity<OrderDetail>(entity =>
            {
                entity.HasKey(e => new { e.OrderId, e.ProductId });

                entity.ToTable("Order Details");

                entity.Property(e => e.OrderId).HasColumnName("OrderID");
                entity.Property(e => e.ProductId).HasColumnName("ProductID");
                entity.Property(e => e.Quantity).HasDefaultValueSql("1");
                entity.Property(e => e.UnitPrice)
                    .HasDefaultValueSql("0")
                    .HasColumnType("NUMERIC");

                entity.HasOne(d => d.Order).WithMany(p => p.OrderDetails)
                    .HasForeignKey(d => d.OrderId)
                    .OnDelete(DeleteBehavior.ClientSetNull);

                entity.HasOne(d => d.Product).WithMany(p => p.OrderDetails)
                    .HasForeignKey(d => d.ProductId)
                    .OnDelete(DeleteBehavior.ClientSetNull);
            });

            modelBuilder.Entity<Product>(entity =>
            {
                entity.Property(e => e.ProductId).HasColumnName("ProductID");
                entity.Property(e => e.CategoryId).HasColumnName("CategoryID");
                entity.Property(e => e.Discontinued).HasDefaultValueSql("'0'");
                entity.Property(e => e.ReorderLevel).HasDefaultValueSql("0");
                entity.Property(e => e.SupplierId).HasColumnName("SupplierID");
                entity.Property(e => e.UnitPrice)
                    .HasDefaultValueSql("0")
                    .HasColumnType("NUMERIC");
                entity.Property(e => e.UnitsInStock).HasDefaultValueSql("0");
                entity.Property(e => e.UnitsOnOrder).HasDefaultValueSql("0");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}