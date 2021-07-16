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
        public string CartId { get; set; }
        [ForeignKey("User")] 
        public string SecurityStamp { get; set; }
        
        public virtual User User { get; set; }
        public int Quantity { get; set; }

        public ICollection<Product> Products { get; set; }
        public List<CartProduct> CartProducts { get; set; }
        
        
        
    }
}
