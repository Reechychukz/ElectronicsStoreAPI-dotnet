using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ElectronicsStore.API.Model
{
    public class ProductForUpdateDto
    {
        public string ProductName { get; set; }
        public string ProductCategory { get; set; }
        public decimal ProductPrice { get; set; }
        public string ProductDescription { get; set; }
        public int NumberOfProducts { get; set; }
    }
}
