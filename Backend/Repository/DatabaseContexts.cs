using lab3.Models;
using Microsoft.EntityFrameworkCore;
using lab3.DTOs;

namespace lab3.Repository
{
    public class DatabaseContexts: DbContext
    {   
        public DatabaseContexts(DbContextOptions<DatabaseContexts> options): base(options) {}
        public DatabaseContexts() { }

        public virtual DbSet<Client> Clients { get; set; } = null!;
        public virtual DbSet<Drink> Drinks { get; set; } = null!;
        public virtual DbSet<DrinkType> DrinkTypes { get; set; } = null!;
        public virtual DbSet<Order> Orders { get; set;} = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {   
            //Many to many for Order
            modelBuilder.Entity<Order>()
                .HasKey(o => new { o.DrinkId, o.ClientId });
            modelBuilder.Entity<Order>()
                .HasOne(o => o.Drink)
                .WithMany(o => o.Orders)
                .HasForeignKey(o => o.DrinkId);

            modelBuilder.Entity<Order>()
                .HasOne(o => o.Client)
                .WithMany(c => c.Orders)
                .HasForeignKey(o => o.ClientId);

            //One to many for Drink-DrinkType
            modelBuilder.Entity<Drink>()
                .HasOne(d => d.DrinkType)
                .WithMany(dt => dt.Drinks)
                .HasForeignKey(d => d.DrinkTypeId);
                    
        }
     
        //public DbSet<ClientDTO>? ClientDTO { get; set; }
        //public DbSet<DrinkDTO>? DrinkDTO { get; set; }
        //public DbSet<DrinkTypeDTO>? DrinkTypeDTO { get; set; }
        //public DbSet<OrderDTO>? OrderDTO { get; set; }

    }
}
