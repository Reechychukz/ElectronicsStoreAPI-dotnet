using ElectronicsStore.API.ValidationAttributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ElectronicsStore.API.Model
{
    [ProductNameMustBeDifferentFromProductDescriptionAttribute]
    public class ProductForCreationDto //: IValidatableObject
    {
        [Required]
        [MaxLength(50)]
        public string ProductName { get; set; }

        [Required]
        public string ProductCategory { get; set; }
        [Required]
        public int ProductPrice { get; set; }

        [MaxLength(1500)]
        public string ProductDescription { get; set; }

        [Required]
        public int NumberOfProductsInStock { get; set; }


        /*public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if(ProductName == ProductDescription)
            {
                yield return new ValidationResult(
                "The Provided Product description should be different from the Product Name.",
                new[] { "ProductForCreationDto" });
            }
        }*/
    }
}
