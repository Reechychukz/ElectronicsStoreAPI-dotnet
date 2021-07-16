using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ElectronicsStore.API.Entities
{
    public class Category
    {
        [Key]
        public string CategoryId { get; set; }

        [Required]
        [MaxLength(100)]
        public string CategoryName { get; set; }
        public ICollection<Product> Products { get; set; }
            = new List<Product>();

    }
}
