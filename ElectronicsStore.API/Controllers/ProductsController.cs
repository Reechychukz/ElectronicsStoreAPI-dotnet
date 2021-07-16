using AutoMapper;
using ElectronicsStore.API.Model;
using ElectronicsStore.API.ResourceParameters;
using ElectronicsStore.API.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ElectronicsStore.API.Controllers
{
    [ApiController]
    [Route("api/categories")]
    public class ProductsController : ControllerBase
    {
        private readonly IElectronicsStoreRepository _electronicsStoreRepository;
        private readonly IMapper _mapper;

        public ProductsController(IElectronicsStoreRepository electronicsStoreRepository, IMapper mapper)
        {
            _electronicsStoreRepository = electronicsStoreRepository ??
                throw new ArgumentNullException(nameof(electronicsStoreRepository));
            _mapper = mapper ??
                throw new ArgumentNullException(nameof(electronicsStoreRepository));
        }

        [HttpGet("{categoryId}/products")]
        public ActionResult<IEnumerable<ProductDto>> GetProductsForCategory(string categoryId)
        {
            if (!_electronicsStoreRepository.CategoryExists(categoryId))
            {
                return NotFound();
            }
            var productsForCategoryFromRepo = _electronicsStoreRepository.GetProducts(categoryId);
            return Ok(_mapper.Map<IEnumerable<ProductDto>>(productsForCategoryFromRepo));

        }

        [HttpGet("{categoryId}/products/{productId}", Name = "GetProductForCategory")]
        public ActionResult<ProductDto> GetProductForCategory(string categoryId, string productId)
        {
            if (!_electronicsStoreRepository.CategoryExists(categoryId))
            {
                return NotFound();
            }
            var productForCategoryFromRepo = _electronicsStoreRepository.GetProduct(categoryId, productId);
            if (productForCategoryFromRepo == null)
            {
                return NotFound();
            }
            return Ok(_mapper.Map<ProductDto>(productForCategoryFromRepo));
        }


        [HttpGet("products")]
        public ActionResult<IEnumerable<ProductDto>> GetProducts(
            [FromQuery] ProductsResourceParameters productsResourceParameters)
        {
            var productsFromRepo = _electronicsStoreRepository.GetProducts(productsResourceParameters);
            return Ok(_mapper.Map<IEnumerable<ProductDto>>(productsFromRepo));
        }

        [Authorize(Roles = UserRoles.Admin)]
        [HttpPost("{categoryId}/products")]
        public ActionResult<ProductDto> CreateProductForCategory(
            string categoryId, ProductForCreationDto product)
        {
            if (!_electronicsStoreRepository.CategoryExists(categoryId))
            {
                return NotFound();
            }

            var productEntity = _mapper.Map<Entities.Product>(product);
            _electronicsStoreRepository.AddProduct(categoryId, productEntity);
            _electronicsStoreRepository.Save();

            var productToReturn = _mapper.Map<ProductDto>(productEntity);

            return CreatedAtRoute("GetProductForCategory",
                new { categoryId = categoryId, productId = productToReturn.Id },
                productToReturn);
        }

        [HttpDelete("{categoryId}/products/{productId}")]
        public ActionResult DeleteProductForCategory(string categoryId, string productId)
        {
            if (!_electronicsStoreRepository.CategoryExists(categoryId))
            {
                return NotFound();
            }

            var productForCategoryFromRepo = _electronicsStoreRepository.GetProduct(categoryId, productId);
            if (productForCategoryFromRepo == null)
            {
                return NotFound();
            }

            _electronicsStoreRepository.DeleteProduct(productForCategoryFromRepo);
            _electronicsStoreRepository.Save();
            return NoContent();

        }

        [Authorize]
        [HttpPut("{categoryId}/products/{productId}")]
        public ActionResult UpdateProductForCategory(string categoryId,
            string productId,
            ProductForUpdateDto product)
        {
            if (!_electronicsStoreRepository.CategoryExists(categoryId))
            {
                return NotFound();
            }

            var productForCategoryFromRepo = _electronicsStoreRepository.GetProduct(categoryId, productId);
            if (productForCategoryFromRepo == null)
            {
                return NotFound();
            }

            //Map the entity to a productForUpdateDto
            //Apply the updated fields to that Dto
            //Map the productForUpdateDto back to that entity
            _mapper.Map(product, productForCategoryFromRepo);

            _electronicsStoreRepository.UpdateProduct(productForCategoryFromRepo);
            _electronicsStoreRepository.Save();
            return NoContent();
        }

        

    

    }
}
