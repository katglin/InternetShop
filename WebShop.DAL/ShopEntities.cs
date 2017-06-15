using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;

namespace WebShop.DAL
{
    public class ShopEntities: DbContext  
    {
        public ShopEntities() : base("name=ShopEntities")
        {
        }

        //protected override void OnModelCreating(DbModelBuilder modelBuilder)
        //{
        //    throw new UnintentionalCodeFirstException();
        //}
        public DbSet<Client> Clients { get; set; }

        public DbSet<ClState> ClStates { get; set; }

        public DbSet<Image> Images { get; set; }

        public DbSet<Order> Orders { get; set; }

        public DbSet<OrderState> OrderStates { get; set; }

        public DbSet<Product> Products { get; set; }

        public DbSet<ProductCategory> ProductCategories { get; set; }

        public DbSet<ProductOrder> ProductOrders { get; set; } 

        public DbSet<PrCategory> PrCategories { get; set; }

        public DbSet<Static> Statics { get; set; }

        public DbSet<Cart> Carts { get; set; }

        public DbSet<OrderCart> OrderCarts { get; set; }
         
        public DbSet<Comment> Comments { get; set; }

        public DbSet <HardDrive> HardDrives { get; set; }

        public DbSet<Memory> Memories { get; set; }

        public DbSet<Processor> Processors { get; set; }

        public DbSet<VideoCard> VideoCards { get; set; } 

        public virtual DbSet<ExampleTable> ExampleTable { get; set; }

    }
}
