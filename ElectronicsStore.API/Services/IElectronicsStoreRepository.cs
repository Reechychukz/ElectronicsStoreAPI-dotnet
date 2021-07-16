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
        IEnumerable<Product> GetProducts(string categoryId);
        
        IEnumerable<Category> GetCategories();
        Product GetProduct(string categoryId, string productid);
        IEnumerable<Product> GetProducts(ProductsResourceParameters productsResourceParameters);

        IEnumerable<Product> GetProducts();
        User GetUser(string userId);
        CartProduct GetCartProduct(string cartProductId);
        Category GetCategory(string categoryId);
        bool CategoryNameExists(string categoryName);
        void AddCart(string securityStamp, Cart cart);
        IEnumerable<Category> GetCategories(IEnumerable<string> categoryIds);
        void AddCategory(Category category);
        void AddProduct(string categoryId, Product product);
        void UpdateProduct(Product product);
        void DeleteProduct(Product product);
        void DeleteCategory(Category category);
        void UpdateCategory(Category category);
        void UpdateUser(User user);
        bool UserExists(string userId);
        bool CategoryExists(string categoryId);
        bool ProductExists(string productId);
        
        
        
        bool Save();
    }
}
