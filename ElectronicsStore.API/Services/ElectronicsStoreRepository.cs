using ElectronicsStore.API.DbContexts;
using ElectronicsStore.API.Entities;
using ElectronicsStore.API.Model;
using ElectronicsStore.API.ResourceParameters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ElectronicsStore.API.Services
{
    public class ElectronicsStoreRepository : IElectronicsStoreRepository, IDisposable
    {
        private readonly ElectronicsStoreContext _context;
        

        public ElectronicsStoreRepository(ElectronicsStoreContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));

        }
        public IEnumerable<User> GetUsers()
        {
            return _context.GetUsers().ToList<User>();
        }

        public IEnumerable<Product> GetProducts()
        {
            return _context.Products.ToList<Product>();
        }
        public IEnumerable<Product> GetProducts(string categoryId)
        {
            if (categoryId == string.Empty)
            {
                throw new ArgumentNullException(nameof(categoryId));
            }

            return _context.Products
                        .Where(c => c.CategoryId == categoryId)
                        .OrderBy(c => c.ProductName).ToList();
        }
        public IEnumerable<Product> GetProducts(ProductsResourceParameters productsResourceParameters)
        {
            if(productsResourceParameters == null)
            {
                throw new ArgumentNullException(nameof(productsResourceParameters));
            }
            if (string.IsNullOrWhiteSpace(productsResourceParameters.ProductCategory) &&
                string.IsNullOrWhiteSpace(productsResourceParameters.SearchQuery))
            {
                return GetProducts();
            }

            var collection = _context.Products as IQueryable<Product>;

            if (!string.IsNullOrWhiteSpace(productsResourceParameters.ProductCategory))
            {
                var productCategory = productsResourceParameters.ProductCategory.Trim();
                collection = collection.Where(a => a.ProductCategory == productCategory);
            }

            if (!string.IsNullOrWhiteSpace(productsResourceParameters.SearchQuery))
            {
                var searchQuery = productsResourceParameters.SearchQuery.Trim();
                collection = collection.Where(a => a.ProductCategory.Contains(searchQuery)
                    || a.ProductName.Contains(searchQuery));
            }
            return collection.ToList();
        }
        
        public IEnumerable<Category> GetCategories()
        {
            return _context.Categories.ToList<Category>();
        }
        public User GetUser(string userId)
        {
            if(userId == string.Empty)
            {
                throw new ArgumentNullException(nameof(userId));
            }
            return _context.GetUsers().FirstOrDefault(a => a.SecurityStamp == userId);
        }

        

        public IEnumerable<Category> GetCategories(IEnumerable<string> categoryIds)
        {
            if (categoryIds == null)
            {
                throw new ArgumentNullException(nameof(categoryIds));
            }

            return _context.Categories.Where(a => categoryIds.Contains(a.CategoryId))
                .OrderBy(a => a.CategoryName)
                .OrderBy(a => a.Products)
                .ToList();
        }


        public Category GetCategory(string categoryId)
        {
            if (categoryId == string.Empty)
            {
                throw new ArgumentNullException(nameof(categoryId));
            }
            return _context.Categories.FirstOrDefault(a => a.CategoryId == categoryId);
        }

        public CartProduct GetCartProduct(string cartProductId)
        {
            if (cartProductId == String.Empty)
            {
                throw new ArgumentNullException(nameof(cartProductId));
            }
            return _context.CartProducts.FirstOrDefault(a => a.CartId == cartProductId);
        }

        

        public void DeleteProduct(Product product)
        {
            _context.Products.Remove(product);
        }

        public void DeleteCategory(Category category)
        {
            if (category == null)
            {
                throw new ArgumentNullException(nameof(category));
            }

            _context.Categories.Remove(category);
        }
        public Product GetProduct(string categoryId, string productId)
        {
            if (categoryId == string.Empty)
            {
                throw new ArgumentNullException(nameof(categoryId));
            }


            if (productId == string.Empty)
            {
                throw new ArgumentNullException(nameof(productId));
            }

            return _context.Products
                .Where(p => p.CategoryId == categoryId && p.ProductId == productId).FirstOrDefault();
        }
        public void AddCategory(Category category)
        {
            if (category == null)
            {
                throw new ArgumentNullException(nameof(category));
            }

            // the repository fills the id (instead of using identity columns)
            category.CategoryId = Guid.NewGuid().ToString();

            foreach (var product in category.Products)
            {
                product.ProductId = Guid.NewGuid().ToString();
            }

            _context.Categories.Add(category);
        }

        

        public void UpdateProduct(Product product)
        {
            // no code in this implementation
        }

        public void UpdateCategory(Category category)
        {
            // no code in this implementation
        }

        public void UpdateUser(User user)
        {
            // no code in this implementation
        }
        

        public void AddProduct(string categoryId, Product product)
        {
            if (categoryId == string.Empty)
            {
                throw new ArgumentNullException(nameof(categoryId));
            }

            if (product == null)
            {
                throw new ArgumentNullException(nameof(product));
            }
            // always set the CategoryId to the passed-in categoryId
            product.CategoryId = categoryId;
            _context.Products.Add(product);
        }
        public void AddCart(string securityStamp, Cart cart)
        {
            if (securityStamp == string.Empty)
            {
                throw new ArgumentNullException(nameof(securityStamp));
            }

            if (cart == null)
            {
                throw new ArgumentNullException(nameof(cart));
            }

            cart.CartId = Guid.NewGuid().ToString();
            // always set the CategoryId to the passed-in categoryId
            
            _context.Carts.Add(cart);
        }

        //public void CreateCartForUser(
        //  string securityStamp, CartForCreationDto cart)
        //{
        //    if (securityStamp == string.Empty)
        //    {
        //        throw new ArgumentNullException("No User Found");
        //    }

        //    var cartEntity = _mapper.Map<Entities.Cart>(cart);
        //    AddCart(securityStamp, cartEntity);
        //    Save();

        //    //var cartToReturn = _mapper.Map<CartDto>(cartEntity);

        //    //return CreatedAtRoute("GetProductForCategory",
        //    //    new { securityStamp = securityStamp, cartId = cartToReturn.Id },
        //    //    cartToReturn);
        //}



        public bool UserExists(string userId)
        {
            if (userId == string.Empty)
            {
                throw new ArgumentNullException(nameof(userId));
            }
            return _context.GetUsers().Any(a => a.SecurityStamp == userId);
        }
        public bool CategoryExists(string categoryId)
        {
            if (categoryId == string.Empty)
            {
                throw new ArgumentNullException(nameof(categoryId));
            }
            return _context.GetCategories().Any(a => a.CategoryId == categoryId);
        }

        public bool CategoryNameExists(string categoryName)
        {
            if(categoryName == string.Empty)
            {
                throw new ArgumentNullException(nameof(categoryName));
            }
            return _context.GetCategories().Any(a => a.CategoryName == categoryName);
        }
        

        public bool ProductExists(string productId)
        {
            if (productId == string.Empty)
            {
                throw new ArgumentNullException(nameof(productId));
            }
            
            return _context.GetProducts().Any(a => a.ProductId == productId);
        }


        public bool Save()
        {
            return (_context.SaveChanges() >= 0);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {

            }
        }
    }
}

