using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace WpfApp1
{
    public partial class ADOmodel : DbContext
    {
        public ADOmodel()
            : base("name=dbContext")
        {}

        public ADOmodel(string conString)
            : base("name=myContext")
        {
            Database.Connection.ConnectionString += conString;
        }

        public virtual DbSet<Certificate> Certificates { get; set; }
        public virtual DbSet<Client> Clients { get; set; }
        public virtual DbSet<Client_address> Client_address { get; set; }
        public virtual DbSet<Delivery> Deliveries { get; set; }
        public virtual DbSet<Delivery_type> Delivery_type { get; set; }
        public virtual DbSet<Employee> Employees { get; set; }
        public virtual DbSet<Item> Items { get; set; }
        public virtual DbSet<Item_list> Item_list { get; set; }
        public virtual DbSet<Item_type> Item_type { get; set; }
        public virtual DbSet<Loyality_card> Loyality_card { get; set; }
        public virtual DbSet<Orders> Orders { get; set; }
        public virtual DbSet<Position_list> Position_list { get; set; }
        public virtual DbSet<Provider> Providers { get; set; }
        public virtual DbSet<Role_list> Role_list { get; set; }
        public virtual DbSet<Supply> Supplies { get; set; }
        public virtual DbSet<Supply_list> Supply_list { get; set; }
        public virtual DbSet<sysdiagram> sysdiagrams { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Client>()
                .Property(e => e.Surname)
                .IsUnicode(false);

            modelBuilder.Entity<Client>()
                .Property(e => e.Name)
                .IsUnicode(false);

            modelBuilder.Entity<Client>()
                .Property(e => e.Patronymic)
                .IsUnicode(false);

            modelBuilder.Entity<Client>()
                .Property(e => e.Phone)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Client>()
                .HasMany(e => e.Orders)
                .WithRequired(e => e.Client)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Client_address>()
                .Property(e => e.Country)
                .IsUnicode(false);

            modelBuilder.Entity<Client_address>()
                .Property(e => e.City)
                .IsUnicode(false);

            modelBuilder.Entity<Client_address>()
                .Property(e => e.Street)
                .IsUnicode(false);

            modelBuilder.Entity<Client_address>()
                .Property(e => e.Building)
                .IsUnicode(false);

            modelBuilder.Entity<Client_address>()
                .Property(e => e.Flat)
                .IsUnicode(false);

            modelBuilder.Entity<Delivery>()
                .Property(e => e.Cost)
                .HasPrecision(18, 1);

            modelBuilder.Entity<Delivery_type>()
                .Property(e => e.Type_name)
                .IsUnicode(false);

            modelBuilder.Entity<Employee>()
                .Property(e => e.Surname)
                .IsUnicode(false);

            modelBuilder.Entity<Employee>()
                .Property(e => e.Name)
                .IsUnicode(false);

            modelBuilder.Entity<Employee>()
                .Property(e => e.Patronymic)
                .IsUnicode(false);

            modelBuilder.Entity<Employee>()
                .Property(e => e.Login)
                .IsUnicode(false);

            modelBuilder.Entity<Employee>()
                .Property(e => e.Password)
                .IsUnicode(false);

            modelBuilder.Entity<Employee>()
                .HasMany(e => e.Orders)
                .WithRequired(e => e.Employee)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Item>()
                .Property(e => e.Name)
                .IsUnicode(false);

            modelBuilder.Entity<Item>()
                .Property(e => e.Untits)
                .IsUnicode(false);

            modelBuilder.Entity<Item>()
                .Property(e => e.Photo)
                .IsUnicode(false);

            modelBuilder.Entity<Item>()
                .Property(e => e.Price)
                .HasPrecision(18, 1);

            modelBuilder.Entity<Item>()
                .HasMany(e => e.Item_list)
                .WithRequired(e => e.Item)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Item>()
                .HasMany(e => e.Supply_list)
                .WithRequired(e => e.Item)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Item_type>()
                .Property(e => e.Type_name)
                .IsUnicode(false);

            modelBuilder.Entity<Item_type>()
                .HasMany(e => e.Items)
                .WithRequired(e => e.Item_type)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Loyality_card>()
                .HasMany(e => e.Clients)
                .WithOptional(e => e.Loyality_card)
                .WillCascadeOnDelete();

            modelBuilder.Entity<Orders>()
                .Property(e => e.Order_cost)
                .HasPrecision(18, 1);

            modelBuilder.Entity<Orders>()
                .HasMany(e => e.Item_list)
                .WithRequired(e => e.Order)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Position_list>()
                .Property(e => e.Position_name)
                .IsUnicode(false);

            modelBuilder.Entity<Position_list>()
                .HasMany(e => e.Employees)
                .WithRequired(e => e.Position_list)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Provider>()
                .Property(e => e.Name)
                .IsUnicode(false);

            modelBuilder.Entity<Provider>()
                .Property(e => e.Phone)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Provider>()
                .HasMany(e => e.Supplies)
                .WithRequired(e => e.Provider)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Role_list>()
                .Property(e => e.Role_name)
                .IsUnicode(false);

            modelBuilder.Entity<Role_list>()
                .HasMany(e => e.Employees)
                .WithRequired(e => e.Role_list)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Supply>()
                .HasMany(e => e.Supply_list)
                .WithRequired(e => e.Supply)
                .WillCascadeOnDelete(false);
        }
    }
}
