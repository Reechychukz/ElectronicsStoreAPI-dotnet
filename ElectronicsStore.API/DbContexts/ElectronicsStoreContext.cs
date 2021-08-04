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

        public DbSet<Cart> GetCarts()
        {
            return Carts;
        }



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {


            base.OnModelCreating(modelBuilder);
        }


    }

}