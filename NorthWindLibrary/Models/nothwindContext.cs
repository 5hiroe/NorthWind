using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace NorthWindLibrary.Models
{
    public partial class nothwindContext : DbContext
    {
        public nothwindContext()
        {
        }

        public nothwindContext(DbContextOptions<nothwindContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<Customer> Customers { get; set; }
        public virtual DbSet<Employee> Employees { get; set; }
        public virtual DbSet<Order> Orders { get; set; }
        public virtual DbSet<Orderdetail> Orderdetails { get; set; }
        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<Shipper> Shippers { get; set; }
        public virtual DbSet<Supplier> Suppliers { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseMySql("server=localhost;database=nothwind;uid=root;pwd=root", Microsoft.EntityFrameworkCore.ServerVersion.Parse("10.3.31-mariadb"));
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasCharSet("utf8mb4")
                .UseCollation("utf8mb4_general_ci");

            modelBuilder.Entity<Category>(entity =>
            {
                entity.ToTable("categories");

                entity.HasCharSet("utf8")
                    .UseCollation("utf8_general_ci");

                entity.HasIndex(e => e.CategoryName, "CategoryName")
                    .IsUnique();

                entity.Property(e => e.CategoryId)
                    .HasColumnType("int(11)")
                    .HasColumnName("CategoryID");

                entity.Property(e => e.CategoryName)
                    .IsRequired()
                    .HasMaxLength(15);
            });

            modelBuilder.Entity<Customer>(entity =>
            {
                entity.ToTable("customers");

                entity.HasCharSet("utf8")
                    .UseCollation("utf8_general_ci");

                entity.HasIndex(e => e.City, "City");

                entity.HasIndex(e => e.CompanyName, "CompanyName");

                entity.HasIndex(e => e.PostalCode, "PostalCode");

                entity.HasIndex(e => e.Region, "Region");

                entity.Property(e => e.CustomerId)
                    .HasMaxLength(5)
                    .HasColumnName("CustomerID");

                entity.Property(e => e.Address).HasMaxLength(60);

                entity.Property(e => e.City).HasMaxLength(15);

                entity.Property(e => e.CompanyName)
                    .IsRequired()
                    .HasMaxLength(40);

                entity.Property(e => e.ContactName).HasMaxLength(30);

                entity.Property(e => e.ContactTitle).HasMaxLength(30);

                entity.Property(e => e.Country).HasMaxLength(15);

                entity.Property(e => e.Fax).HasMaxLength(24);

                entity.Property(e => e.Phone).HasMaxLength(24);

                entity.Property(e => e.PostalCode).HasMaxLength(10);

                entity.Property(e => e.Region).HasMaxLength(15);
            });

            modelBuilder.Entity<Employee>(entity =>
            {
                entity.ToTable("employees");

                entity.HasCharSet("utf8")
                    .UseCollation("utf8_general_ci");

                entity.HasIndex(e => e.LastName, "LastName");

                entity.HasIndex(e => e.PostalCode, "PostalCode");

                entity.Property(e => e.EmployeeId)
                    .HasColumnType("int(11)")
                    .HasColumnName("EmployeeID");

                entity.Property(e => e.Address).HasMaxLength(60);

                entity.Property(e => e.BirthDate).HasColumnType("datetime");

                entity.Property(e => e.City).HasMaxLength(15);

                entity.Property(e => e.Country).HasMaxLength(15);

                entity.Property(e => e.Extension).HasMaxLength(4);

                entity.Property(e => e.FirstName)
                    .IsRequired()
                    .HasMaxLength(10);

                entity.Property(e => e.HireDate).HasColumnType("datetime");

                entity.Property(e => e.HomePhone).HasMaxLength(24);

                entity.Property(e => e.LastName)
                    .IsRequired()
                    .HasMaxLength(20);

                entity.Property(e => e.PostalCode).HasMaxLength(10);

                entity.Property(e => e.Region).HasMaxLength(15);

                entity.Property(e => e.ReportsTo).HasColumnType("int(11)");

                entity.Property(e => e.Title).HasMaxLength(30);

                entity.Property(e => e.TitleOfCourtesy).HasMaxLength(25);
            });

            modelBuilder.Entity<Order>(entity =>
            {
                entity.ToTable("orders");

                entity.HasCharSet("utf8")
                    .UseCollation("utf8_general_ci");

                entity.HasIndex(e => e.CustomerId, "CustomerID");

                entity.HasIndex(e => e.EmployeeId, "EmployeeID");

                entity.HasIndex(e => e.OrderDate, "OrderDate");

                entity.HasIndex(e => e.ShipPostalCode, "ShipPostalCode");

                entity.HasIndex(e => e.ShipVia, "ShipVia");

                entity.HasIndex(e => e.ShippedDate, "ShippedDate");

                entity.Property(e => e.OrderId)
                    .HasColumnType("int(11)")
                    .HasColumnName("OrderID");

                entity.Property(e => e.CustomerId)
                    .HasMaxLength(5)
                    .HasColumnName("CustomerID");

                entity.Property(e => e.EmployeeId)
                    .HasColumnType("int(11)")
                    .HasColumnName("EmployeeID");

                entity.Property(e => e.Freight)
                    .HasPrecision(19, 4)
                    .HasDefaultValueSql("'0.0000'");

                entity.Property(e => e.OrderDate).HasColumnType("datetime");

                entity.Property(e => e.RequiredDate).HasColumnType("datetime");

                entity.Property(e => e.ShipAddress).HasMaxLength(60);

                entity.Property(e => e.ShipCity).HasMaxLength(15);

                entity.Property(e => e.ShipCountry).HasMaxLength(15);

                entity.Property(e => e.ShipName).HasMaxLength(40);

                entity.Property(e => e.ShipPostalCode).HasMaxLength(10);

                entity.Property(e => e.ShipRegion).HasMaxLength(15);

                entity.Property(e => e.ShipVia).HasColumnType("int(11)");

                entity.Property(e => e.ShippedDate).HasColumnType("datetime");

                entity.HasOne(d => d.Customer)
                    .WithMany(p => p.Orders)
                    .HasForeignKey(d => d.CustomerId)
                    .HasConstraintName("orders_ibfk_1");

                entity.HasOne(d => d.Employee)
                    .WithMany(p => p.Orders)
                    .HasForeignKey(d => d.EmployeeId)
                    .HasConstraintName("orders_ibfk_2");

                entity.HasOne(d => d.ShipViaNavigation)
                    .WithMany(p => p.Orders)
                    .HasForeignKey(d => d.ShipVia)
                    .HasConstraintName("orders_ibfk_3");
            });

            modelBuilder.Entity<Orderdetail>(entity =>
            {
                entity.HasKey(e => new { e.OrderId, e.ProductId })
                    .HasName("PRIMARY")
                    .HasAnnotation("MySql:IndexPrefixLength", new[] { 0, 0 });

                entity.ToTable("orderdetails");

                entity.HasCharSet("utf8")
                    .UseCollation("utf8_general_ci");

                entity.HasIndex(e => e.OrderId, "OrderID");

                entity.HasIndex(e => e.ProductId, "ProductID");

                entity.Property(e => e.OrderId)
                    .HasColumnType("int(11)")
                    .HasColumnName("OrderID");

                entity.Property(e => e.ProductId)
                    .HasColumnType("int(11)")
                    .HasColumnName("ProductID");

                entity.Property(e => e.Quantity)
                    .HasColumnType("int(11)")
                    .HasDefaultValueSql("'1'");

                entity.Property(e => e.UnitPrice).HasPrecision(19, 4);

                entity.HasOne(d => d.Order)
                    .WithMany(p => p.Orderdetails)
                    .HasForeignKey(d => d.OrderId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("orderdetails_ibfk_1");

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.Orderdetails)
                    .HasForeignKey(d => d.ProductId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("orderdetails_ibfk_2");
            });

            modelBuilder.Entity<Product>(entity =>
            {
                entity.ToTable("products");

                entity.HasCharSet("utf8")
                    .UseCollation("utf8_general_ci");

                entity.HasIndex(e => e.CategoryId, "CategoryID");

                entity.HasIndex(e => e.ProductName, "ProductName");

                entity.HasIndex(e => e.SupplierId, "SupplierID");

                entity.Property(e => e.ProductId)
                    .HasColumnType("int(11)")
                    .HasColumnName("ProductID");

                entity.Property(e => e.CategoryId)
                    .HasColumnType("int(11)")
                    .HasColumnName("CategoryID");

                entity.Property(e => e.Discontinued).HasDefaultValueSql("'0'");

                entity.Property(e => e.ProductName)
                    .IsRequired()
                    .HasMaxLength(40);

                entity.Property(e => e.QuantityPerUnit).HasMaxLength(20);

                entity.Property(e => e.ReorderLevel)
                    .HasColumnType("int(11)")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.SupplierId)
                    .HasColumnType("int(11)")
                    .HasColumnName("SupplierID");

                entity.Property(e => e.UnitPrice)
                    .HasPrecision(19, 4)
                    .HasDefaultValueSql("'0.0000'");

                entity.Property(e => e.UnitsInStock)
                    .HasColumnType("int(11)")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.UnitsOnOrder)
                    .HasColumnType("int(11)")
                    .HasDefaultValueSql("'0'");

                entity.HasOne(d => d.Category)
                    .WithMany(p => p.Products)
                    .HasForeignKey(d => d.CategoryId)
                    .HasConstraintName("products_ibfk_2");

                entity.HasOne(d => d.Supplier)
                    .WithMany(p => p.Products)
                    .HasForeignKey(d => d.SupplierId)
                    .HasConstraintName("products_ibfk_1");
            });

            modelBuilder.Entity<Shipper>(entity =>
            {
                entity.ToTable("shippers");

                entity.HasCharSet("utf8")
                    .UseCollation("utf8_general_ci");

                entity.Property(e => e.ShipperId)
                    .HasColumnType("int(11)")
                    .HasColumnName("ShipperID");

                entity.Property(e => e.CompanyName)
                    .IsRequired()
                    .HasMaxLength(40);

                entity.Property(e => e.Phone).HasMaxLength(24);
            });

            modelBuilder.Entity<Supplier>(entity =>
            {
                entity.ToTable("suppliers");

                entity.HasCharSet("utf8")
                    .UseCollation("utf8_general_ci");

                entity.HasIndex(e => e.CompanyName, "CompanyName");

                entity.HasIndex(e => e.PostalCode, "PostalCode");

                entity.Property(e => e.SupplierId)
                    .HasColumnType("int(11)")
                    .HasColumnName("SupplierID");

                entity.Property(e => e.Address).HasMaxLength(60);

                entity.Property(e => e.City).HasMaxLength(15);

                entity.Property(e => e.CompanyName)
                    .IsRequired()
                    .HasMaxLength(40);

                entity.Property(e => e.ContactName).HasMaxLength(30);

                entity.Property(e => e.ContactTitle).HasMaxLength(30);

                entity.Property(e => e.Country).HasMaxLength(15);

                entity.Property(e => e.Fax).HasMaxLength(24);

                entity.Property(e => e.Phone).HasMaxLength(24);

                entity.Property(e => e.PostalCode).HasMaxLength(10);

                entity.Property(e => e.Region).HasMaxLength(15);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
