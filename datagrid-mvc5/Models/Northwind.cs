namespace datagrid_mvc5.Models
{
    using System;
    using System.Data.Entity;

    public partial class Northwind : DbContext
    {
        public Northwind()
            : base("name=Northwind")
        {
            Configuration.ProxyCreationEnabled = false;
        }

        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<Customer> Customers { get; set; }
        public virtual DbSet<Employee> Employees { get; set; }
        public virtual DbSet<Order_Detail> Order_Details { get; set; }
        public virtual DbSet<Order> Orders { get; set; }
        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<Shipper> Shippers { get; set; }
        public virtual DbSet<Supplier> Suppliers { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Customer>()
                .Property(e => e.CustomerID)
                .IsFixedLength();

            modelBuilder.Entity<Employee>()
                .Property(e => e.EmployeeID);
              //

            modelBuilder.Entity<Order_Detail>()
                .Property(e => e.UnitPrice)
                .HasPrecision(9, 2);

            modelBuilder.Entity<Order>()
                .Property(e => e.CustomerID)
                .IsFixedLength();

            modelBuilder.Entity<Order>()
                .Property(e => e.Freight)
                .HasPrecision(9, 2);

            modelBuilder.Entity<Order>()
                .HasMany(e => e.Order_Details)
                .WithRequired(e => e.Order)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Product>()
                .Property(e => e.UnitPrice)
                .HasPrecision(9, 2);

            modelBuilder.Entity<Product>()
                .HasMany(e => e.Order_Details)
                .WithRequired(e => e.Product)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Shipper>()
                .HasMany(e => e.Orders)
                .WithOptional(e => e.Shipper)
                .HasForeignKey(e => e.ShipVia);
        }
    }

    public partial class xe : DbContext
    {
        public xe()
            : base("name=OracleDbContext")
        {
            Configuration.ProxyCreationEnabled = false;


        }

        public virtual DbSet<Category> Categories { get; set; }

        public void Test()
        {
            try
            {
                var x = Database.SqlQuery<string>("SELECT 'f' FROM duaddl");
                foreach (var name in x)
                {
                    System.Diagnostics.Debug.WriteLine(name);
                }
            }
            catch (Exception ex)
            {

                throw;
            }
        }

    }
}
