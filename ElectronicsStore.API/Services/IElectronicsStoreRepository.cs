using ElectronicsStore.API.Entities;
using ElectronicsStore.API.Model;
using ElectronicsStore.API.ResourceParameters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ElectronicsStore.API.Services
{
    public interface IElectronicsStoreRepository
    {
        IEnumerable<User> GetUsers();
        IEnumerable<Cart> GetCarts();
        IEnumerable<Product> GetProducts(string categoryId);
        
        IEnumerable<Category> GetCategories();
        Product GetProduct(string categoryId, string productid);
        IEnumerable<Product> GetProducts(ProductsResourceParameters productsResourceParameters);

        IEnumerable<Product> GetProducts();
        Product GetProduct(string productId);
        User GetUser(string userId);
        Cart GetCart(string securityStamp, string cartId);
        IEnumerable<CartProduct> GetCartProducts(string cartId);
        Category GetCategory(string categoryId);
        bool CategoryNameExists(string categoryName);
        void AddCart(string userId, Cart cart);
        IEnumerable<Category> GetCategories(IEnumerable<string> categoryIds);
        void AddCategory(Category category);
        void AddProduct(string categoryId, Product product);
        void AddCartProduct(string cartId, string productId, CartProduct cartProduct);
        void UpdateProduct(Product product);
        void DeleteProduct(Product product);
        void DeleteCategory(Category category);
        void UpdateCategory(Category category);
        void UpdateUser(User user);
        bool UserExists(string userId);
        bool CartExists(string cartId);
        bool CategoryExists(string categoryId);
        bool ProductExists(string productId);
        
        
        
        bool Save();
    }
}
