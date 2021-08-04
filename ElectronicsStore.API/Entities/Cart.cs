using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ElectronicsStore.API.Entities
{
    public class Cart
    {
        [Key]
        public string Id { get; set; }
        [ForeignKey("User")] 
        public string UserId { get; set; }
        
        public virtual User User { get; set; }
        
        //public ICollection<Product> Products { get; set; }
        public List<CartProduct> CartProducts { get; set; }
        
        public decimal TotalPrice { get; set; }
        
    }
}
