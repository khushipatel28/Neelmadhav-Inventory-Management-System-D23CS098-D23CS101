using Clgproject.Models.MainSuppliers;
using Clgproject.Models.MainTrans;
using Clgproject.Models.Products;
using Clgproject.Models.Customers;
using Clgproject.Models.Suppliers;
using Microsoft.EntityFrameworkCore;
using Clgproject.Models.CustTrans;
using Clgproject.Controllers;

namespace Clgproject.Data
{
    public class ClgDbContext : DbContext
    {
        public ClgDbContext(DbContextOptions<ClgDbContext> options) : base(options)
        {

        }
        public DbSet<Product> Products { get; set; }
         
        public DbSet<MainTransaction> MainTransactions { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<MainSupplier>()
              .HasOne(p => p.mainTransactions)
              .WithMany(c => c.MainSuppliers)
              .HasForeignKey(p => p.MainTransactionId);

            modelBuilder.Entity<Customer>()
              .HasOne(p => p.cust_Transaction)
              .WithMany(c => c.Customers)
              .HasForeignKey(p => p.Cust_Transaction_id);

            modelBuilder.Entity<OrderC>()
             .HasOne(p => p.supplier)
             .WithMany(c => c.orderCs)
             .HasForeignKey(p => p.Supplier_id);

            modelBuilder.Entity<ReturnCOrder>()
              .HasOne(p => p.orderC)
           .WithMany(c => c.returnCOrders)
           .HasForeignKey(p => p.Order_id);

            modelBuilder.Entity<ReceiveCOrder>()
           .HasOne(p => p.orderC)
          .WithMany(c => c.receiveCOrders)
          .HasForeignKey(p => p.Order_id);

            modelBuilder.Entity<Product>()
            .HasOne(p => p.mainSupplier)
         .WithMany(c => c.products)
         .HasForeignKey(p => p.Mainsupp_id);

            modelBuilder.Entity<ReturnIOrder>()
          .HasOne(p => p.product)
       .WithMany(c => c.returnIOrders)
       .HasForeignKey(p => p.Order_id);

            modelBuilder.Entity<ReceiveIOrder>()
          .HasOne(p => p.product)
       .WithMany(c => c.receiveIOrders)
       .HasForeignKey(p => p.Order_id);


        }

        public DbSet<MainSupplierHistory> MainSupplierHistories { get; set; }

        //public DbSet<SupplierHistory> SupplierHistories { get; set; }
        public DbSet<MainSupplier> MainSuppliers { get; set; }
        public DbSet<Customer> Customers { get; set; }

        public DbSet<ReceiveIOrder> receiveIOrders { get; set; }

        public DbSet<ReturnIOrder> returnIOrders { get; set; }
        public DbSet<OrderC> C_orders { get; set; }
        public DbSet<Cust_Transaction> cust_Transactions { get; set; }
       
        public DbSet<ReceiveCOrder> receiveCOrders { get; set; }

        public DbSet<ReturnCOrder> returnCOrders { get; set; }

        public DbSet<Supplier> Suppliers { get; set; }

    }
}
