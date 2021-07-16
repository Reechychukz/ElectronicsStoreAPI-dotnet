using ElectronicsStore.API.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ElectronicsStore.API.ValidationAttributes
{
    public class ProductNameMustBeDifferentFromProductDescriptionAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value,
            ValidationContext validationContext)
        {
            var product = (ProductForCreationDto)validationContext.ObjectInstance;
            if(product.ProductName == product.ProductDescription)
            {
                return new ValidationResult(
                    "The provided product name should be different from it's description.",
                    new[] { nameof(ProductForCreationDto) });
            }
            return ValidationResult.Success;
        }
    }
}
