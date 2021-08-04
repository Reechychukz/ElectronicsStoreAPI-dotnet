using ElectronicsStore.API.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ElectronicsStore.API.Entities
{
    public class Product
    {
        [Key]
        public string Id { get; set; }
        

        [Required]
        [MaxLength(50)]
        public string ProductName { get; set; }
        [Required]
        public string ProductCategory { get; set; }

        [Required]
        [Column(TypeName = "decimal(18, 4)")]
        public decimal ProductPrice { get; set; }
        public string ProductDescription { get; set; }
        [Required]
        public int NumberOfProductsInStock { get; set; }
        
        public List<CartProduct> CartProducts { get; set; }
        public int Quantity { get; set; }

        [ForeignKey("CategoryId")]
        public Category Category { get; set; }
        public string CategoryId { get; set; }
    }
}
