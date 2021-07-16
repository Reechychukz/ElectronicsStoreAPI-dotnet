using ElectronicsStore.API.Entities;
using ElectronicsStore.API.Model;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ElectronicsStore.API.DbContexts
{
    public class ElectronicsStoreContext : IdentityDbContext<User>
    {
        public ElectronicsStoreContext(DbContextOptions<ElectronicsStoreContext> options)
            : base(options)
        {
        }
        public DbSet<Cart> Carts { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Discount> Discounts { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<User> UserCollecton { get; set; }
        public DbSet<CartProduct> CartProducts { get; set; }
        
        

        public DbSet<User> GetUsers()
        {
            return UserCollecton;
        }

        public DbSet<Category> GetCategories()
        {
            return Categories;
        }

        public DbSet<Product> GetProducts()
        {
            return Products;
        }



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Cart>()
            .HasMany(p => p.Products)
            .WithMany(p => p.Carts)
            .UsingEntity<CartProduct>(
                j => j
                    .HasOne(pt => pt.Product)
                    .WithMany(t => t.CartProducts)
                    .HasForeignKey(pt => pt.ProductId),
                j => j
                    .HasOne(pt => pt.Cart)
                    .WithMany(p => p.CartProducts)
                    .HasForeignKey(pt => pt.CartId)
                );

            //    modelBuilder.Entity<Cart>()
            //.HasKey(cp => new { cp.CartId, cp.ProductId });

            //    modelBuilder.Entity<CartProduct>()
            //        .HasOne(bc => bc.Cart)
            //        .WithMany(b => b.CartProducts)
            //        .HasForeignKey(bc => bc.CartId);

            //    modelBuilder.Entity<CartProduct>()
            //        .HasOne(bc => bc.Product)
            //        .WithMany(c => c.Carts)
            //        .HasForeignKey(bc => bc.ProductId);

            /*modelBuilder.Entity<User>().HasData(
                new User()
                {
                    UserId = Guid.Parse("3676a72b-3838-4838-48b7-3728ba736263"),
                    EmailAddress = "richkeed94@gmail.com",
                    FirstName = "Richard",
                    LastName = "Chukwuma",
                    Address = "Bariga"


                },
                new User()
                {
                    UserId = Guid.Parse("3773452b-3838-4838-48b7-3728ba736263"),
                    EmailAddress = "richkeed94@gmail.com",
                    FirstName = "Chinedu",
                    LastName = "Adebayo",
                    Address = "Ondo"

                });
           modelBuilder.Entity<Cart>().HasData(
                new Cart()
                {
                    Id = Guid.Parse("3676a72b-3838-4838-48b7-3728ba736263"),
                    TotalPriceOfProductsIncart = 90,
                    
                },
                new Cart()
                {
                    Id = Guid.Parse("3673452b-3838-4838-48b7-3728ba736263"),
                    TotalPriceOfProductsIncart = 50,
                    
                });
            modelBuilder.Entity<Category>().HasData(
                new Category()
                {
                    Id = Guid.Parse("4076a72b-3838-4838-48b7-3728ba7362ab"),
                    CategoryName = "Wrist Watches",

                },
                new Category()
                {
                    Id = Guid.Parse("4073452b-3838-4838-48b7-3728ba7362ac"),
                    CategoryName = "Television",

                });
            modelBuilder.Entity<Product>().HasData(
                new Product()
                {
                    Id = Guid.Parse("5076a72b-3838-4838-48b7-3728ba736234"),
                    ProductName = "Apple Watch",
                    ProductPrice = 50,
                    ProductDescription = "Sophisticated",
                    NumberOfProductsInStock = 20,
                    ProductCategory ="Wrist Watch",
                    CategoryId = Guid.Parse("4076a72b-3838-4838-48b7-3728ba7362ab")
                    
                },
                new Product()
                {
                    Id = Guid.Parse("5076a72b-3838-4838-48b7-3728ba736235"),
                    ProductName = "Samsung Television",
                    ProductPrice = 400,
                    ProductDescription = "Sophisticated",
                    NumberOfProductsInStock = 20,
                    ProductCategory = "Television",
                    CategoryId = Guid.Parse("4073452b-3838-4838-48b7-3728ba7362ac")

                });*/

            base.OnModelCreating(modelBuilder);
        }
    
    }

    //public class ElectronicsStoreContextFactory : IDesignTimeDbContextFactory<ElectronicsStoreContext>
    //{
    //    public ElectronicsStoreContext CreateDbContext(string[] args)
    //    {
    //        var optionsBuilder = new DbContextOptionsBuilder<ElectronicsStoreContext>();
    //        optionsBuilder.UseSqlServer(@"Server = (localdb)\mssqllocaldb;Database=ElectronicsStoreDB;Trusted_Connection=True");

    //        return new ElectronicsStoreContext(optionsBuilder.Options);
    //    }
    //}
    //public class ElectronicsStoreContextFactory : IDesignTimeDbContextFactory<ElectronicsStoreContext>
    //{
    //    public ElectronicsStoreContextFactory CreateDbContext(string[] args)
    //    {
    //        var optionsBuilder = new DbContextOptionsBuilder<ElectronicsStoreContext>();
    //        optionsBuilder.UseSqlServer("");

    //        return new ElectronicsStoreContext(optionsBuilder.Options);
    //    }
    //}
}
