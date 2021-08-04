using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace ElectronicsStore.API.Entities
{
    public class CartProduct
    {
        [Key]
        public string Id { get; set; }
        
        public string CartId { get; set; }
        public virtual Cart Cart { get; set; }
        public string ProductId { get; set; }
        public virtual Product Product { get; set; }
        public int Quantity { get; set; }
        

    }
}
