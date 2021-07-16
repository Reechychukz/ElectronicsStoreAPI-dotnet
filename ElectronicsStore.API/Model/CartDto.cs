using ElectronicsStore.API.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace ElectronicsStore.API.Model
{
    public class CartDto
    {
        public string CartId { get; set; }

        [DefaultValue(0)]
        public int TotalPriceOfProductsIncart { get; set; }
       
        

    }
}
