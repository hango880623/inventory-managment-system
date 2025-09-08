using Microsoft.EntityFrameworkCore;
using InventoryManagementSystem.Models;

namespace InventoryManagementSystem.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        // DbSets for all entities
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Supplier> Suppliers { get; set; }
        public DbSet<PurchaseOrder> PurchaseOrders { get; set; }
        public DbSet<PurchaseOrderDetail> PurchaseOrderDetails { get; set; }
        public DbSet<SalesOrder> SalesOrders { get; set; }
        public DbSet<SalesOrderDetail> SalesOrderDetails { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configure User entity
            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("tblUsers");
                entity.HasKey(e => e.UserId);
                entity.Property(e => e.UserId).HasColumnName("userid");
                entity.Property(e => e.Email).HasColumnName("email").HasMaxLength(100).IsRequired();
                entity.Property(e => e.Password).HasColumnName("password").HasMaxLength(255).IsRequired();
                entity.Property(e => e.RoleId).HasColumnName("roleid").IsRequired();
                entity.Property(e => e.Name).HasColumnName("name").HasMaxLength(100);
                entity.Property(e => e.AccountStatus).HasColumnName("accountstatus").HasMaxLength(50);
                entity.Property(e => e.Photo).HasColumnName("photo");
                entity.Property(e => e.ResetToken).HasColumnName("resettoken").HasMaxLength(255);

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.Users)
                    .HasForeignKey(d => d.RoleId)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            // Configure Role entity
            modelBuilder.Entity<Role>(entity =>
            {
                entity.ToTable("tblRoles");
                entity.HasKey(e => e.RoleId);
                entity.Property(e => e.RoleId).HasColumnName("roleid");
                entity.Property(e => e.RoleName).HasColumnName("role").HasMaxLength(50).IsRequired();
                entity.Property(e => e.Description).HasColumnName("description").HasMaxLength(255);
            });

            // Configure Product entity
            modelBuilder.Entity<Product>(entity =>
            {
                entity.ToTable("tblProducts");
                entity.HasKey(e => e.ProductId);
                entity.Property(e => e.ProductId).HasColumnName("productid");
                entity.Property(e => e.ProductName).HasColumnName("productname").HasMaxLength(100).IsRequired();
                entity.Property(e => e.Description).HasColumnName("description").HasMaxLength(255);
                entity.Property(e => e.PurchasePrice).HasColumnName("purchaseprice").HasColumnType("decimal(18,2)");
                entity.Property(e => e.SalesPrice).HasColumnName("salesprice").HasColumnType("decimal(18,2)");
                entity.Property(e => e.InStock).HasColumnName("instock");
                entity.Property(e => e.ValueHand).HasColumnName("valuehand").HasColumnType("decimal(18,2)");
                entity.Property(e => e.Photo).HasColumnName("photo");
            });

            // Configure Customer entity
            modelBuilder.Entity<Customer>(entity =>
            {
                entity.ToTable("tblCustomers");
                entity.HasKey(e => e.CustomerId);
                entity.Property(e => e.CustomerId).HasColumnName("customerid");
                entity.Property(e => e.CustomerName).HasColumnName("customername").HasMaxLength(100).IsRequired();
                entity.Property(e => e.Address).HasColumnName("address").HasMaxLength(255);
                entity.Property(e => e.City).HasColumnName("city").HasMaxLength(50);
                entity.Property(e => e.Province).HasColumnName("province").HasMaxLength(50);
                entity.Property(e => e.ZipCode).HasColumnName("zipcode").HasMaxLength(20);
                entity.Property(e => e.Phone).HasColumnName("phone").HasMaxLength(20);
                entity.Property(e => e.Email).HasColumnName("email").HasMaxLength(100);
            });

            // Configure Supplier entity
            modelBuilder.Entity<Supplier>(entity =>
            {
                entity.ToTable("tblSuppliers");
                entity.HasKey(e => e.SupplierId);
                entity.Property(e => e.SupplierId).HasColumnName("supplierid");
                entity.Property(e => e.SupplierName).HasColumnName("suppliername").HasMaxLength(100).IsRequired();
                entity.Property(e => e.Address).HasColumnName("address").HasMaxLength(255);
                entity.Property(e => e.City).HasColumnName("city").HasMaxLength(50);
                entity.Property(e => e.Province).HasColumnName("province").HasMaxLength(50);
                entity.Property(e => e.ZipCode).HasColumnName("zipcode").HasMaxLength(20);
                entity.Property(e => e.Phone).HasColumnName("phone").HasMaxLength(20);
                entity.Property(e => e.Email).HasColumnName("email").HasMaxLength(100);
            });

            // Configure PurchaseOrder entity
            modelBuilder.Entity<PurchaseOrder>(entity =>
            {
                entity.ToTable("tblPurchaseOrders");
                entity.HasKey(e => e.PurchaseOrderId);
                entity.Property(e => e.PurchaseOrderId).HasColumnName("purchaseorderid");
                entity.Property(e => e.PurchaseOrderNo).HasColumnName("purchaseorderno").HasMaxLength(50).IsRequired();
                entity.Property(e => e.SupplierId).HasColumnName("supplierid").IsRequired();
                entity.Property(e => e.PurchaseDate).HasColumnName("purchasedate");
                entity.Property(e => e.Subtotal).HasColumnName("subtotal").HasColumnType("decimal(18,2)");
                entity.Property(e => e.Tax).HasColumnName("tax").HasColumnType("decimal(18,2)");
                entity.Property(e => e.Total).HasColumnName("total").HasColumnType("decimal(18,2)");

                entity.HasOne(d => d.Supplier)
                    .WithMany(p => p.PurchaseOrders)
                    .HasForeignKey(d => d.SupplierId)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            // Configure PurchaseOrderDetail entity
            modelBuilder.Entity<PurchaseOrderDetail>(entity =>
            {
                entity.ToTable("tblPurchaseOrderDetails");
                entity.HasKey(e => e.PurchaseOrderDetailsId);
                entity.Property(e => e.PurchaseOrderDetailsId).HasColumnName("purchaseorderdetailsid");
                entity.Property(e => e.PurchaseOrderId).HasColumnName("purchaseorderid").IsRequired();
                entity.Property(e => e.ProductId).HasColumnName("productid").IsRequired();
                entity.Property(e => e.Description).HasColumnName("description").HasMaxLength(255);
                entity.Property(e => e.Qty).HasColumnName("qty");
                entity.Property(e => e.SalesPrice).HasColumnName("salesprice").HasColumnType("decimal(18,2)");
                entity.Property(e => e.Total).HasColumnName("total").HasColumnType("decimal(18,2)");

                entity.HasOne(d => d.PurchaseOrder)
                    .WithMany(p => p.PurchaseOrderDetails)
                    .HasForeignKey(d => d.PurchaseOrderId)
                    .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.PurchaseOrderDetails)
                    .HasForeignKey(d => d.ProductId)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            // Configure SalesOrder entity
            modelBuilder.Entity<SalesOrder>(entity =>
            {
                entity.ToTable("tblSalesOrders");
                entity.HasKey(e => e.SalesOrderId);
                entity.Property(e => e.SalesOrderId).HasColumnName("salesorderid");
                entity.Property(e => e.SalesOrderNo).HasColumnName("salesorderno").HasMaxLength(50).IsRequired();
                entity.Property(e => e.CustomerId).HasColumnName("customerid").IsRequired();
                entity.Property(e => e.OrderDate).HasColumnName("orderdate");
                entity.Property(e => e.Subtotal).HasColumnName("subtotal").HasColumnType("decimal(18,2)");
                entity.Property(e => e.Tax).HasColumnName("tax").HasColumnType("decimal(18,2)");
                entity.Property(e => e.Total).HasColumnName("total").HasColumnType("decimal(18,2)");

                entity.HasOne(d => d.Customer)
                    .WithMany(p => p.SalesOrders)
                    .HasForeignKey(d => d.CustomerId)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            // Configure SalesOrderDetail entity
            modelBuilder.Entity<SalesOrderDetail>(entity =>
            {
                entity.ToTable("tblSalesOrderDetails");
                entity.HasKey(e => e.SalesOrderDetailsId);
                entity.Property(e => e.SalesOrderDetailsId).HasColumnName("salesorderdetailsid");
                entity.Property(e => e.SalesOrderId).HasColumnName("salesorderid").IsRequired();
                entity.Property(e => e.ProductId).HasColumnName("productid").IsRequired();
                entity.Property(e => e.Description).HasColumnName("description").HasMaxLength(255);
                entity.Property(e => e.Qty).HasColumnName("qty");
                entity.Property(e => e.SalesPrice).HasColumnName("salesprice").HasColumnType("decimal(18,2)");
                entity.Property(e => e.Total).HasColumnName("total").HasColumnType("decimal(18,2)");

                entity.HasOne(d => d.SalesOrder)
                    .WithMany(p => p.SalesOrderDetails)
                    .HasForeignKey(d => d.SalesOrderId)
                    .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.SalesOrderDetails)
                    .HasForeignKey(d => d.ProductId)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            // Seed initial data
            SeedData(modelBuilder);
        }

        private void SeedData(ModelBuilder modelBuilder)
        {
            // Seed Roles
            modelBuilder.Entity<Role>().HasData(
                new Role { RoleId = 1, RoleName = "Admin", Description = "System Administrator" },
                new Role { RoleId = 2, RoleName = "Manager", Description = "Inventory Manager" },
                new Role { RoleId = 3, RoleName = "User", Description = "Regular User" }
            );

            // Seed default admin user (password: Admin123!)
            modelBuilder.Entity<User>().HasData(
                new User 
                { 
                    UserId = 1, 
                    Email = "admin@inventory.com", 
                    Password = BCrypt.Net.BCrypt.HashPassword("Admin123!"),
                    RoleId = 1, 
                    Name = "System Administrator", 
                    AccountStatus = "Active" 
                }
            );
        }
    }
}
