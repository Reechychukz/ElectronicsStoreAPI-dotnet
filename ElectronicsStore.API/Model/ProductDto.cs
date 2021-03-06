using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ElectronicsStore.API.Model
{
    public class ProductDto
    {
        public Guid Id { get; set; }
        public string ProductName { get; set; }
        public string ProductCategory { get; set; }
        public string ProductPrice { get; set; }
        public string ProductDescription { get; set; }
        public int NumberOfProductsInStock { get; set; }
        public Guid CategoryId { get; set; }
    }
}
