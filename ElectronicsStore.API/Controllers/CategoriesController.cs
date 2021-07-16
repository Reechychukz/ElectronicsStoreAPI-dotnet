using AutoMapper;
using ElectronicsStore.API.DbContexts;
using ElectronicsStore.API.Model;
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
    public class CategoriesController : ControllerBase
    {
        private readonly IElectronicsStoreRepository _electronicsStoreRepository;
        private readonly IMapper _mapper;

        public CategoriesController(IElectronicsStoreRepository electronicsStoreRepository, IMapper mapper)
        {
            _electronicsStoreRepository = electronicsStoreRepository ??
                throw new ArgumentNullException(nameof(electronicsStoreRepository));
            _mapper = mapper ??
                throw new ArgumentNullException(nameof(mapper));
        }

        [HttpGet]
        public ActionResult GetCategories()
        {
            var categoriesFromRepo = _electronicsStoreRepository.GetCategories();

            return Ok(_mapper.Map<IEnumerable<CategoryDto>>(categoriesFromRepo));
        }
        
        [HttpGet("{categoryId}", Name = "GetCategory")]
        public ActionResult GetCategory(string categoryId)
        {
            var categoryFromRepo = _electronicsStoreRepository.GetCategory(categoryId);
            if(categoryFromRepo == null)
            {
                return NotFound();
            }
            return Ok(_mapper.Map<CategoryDto>(categoryFromRepo));
        }

        [Authorize(Roles = UserRoles.Admin)]
        [HttpPost]
        public ActionResult<CategoryDto> CreateCategory(CategoryForCreationDto category)
        {
            var categoryEntity = _mapper.Map<Entities.Category>(category);
            

            _electronicsStoreRepository.AddCategory(categoryEntity);
            _electronicsStoreRepository.Save();

            var categoryToReturn = _mapper.Map<CategoryDto>(categoryEntity);
            return CreatedAtRoute("GetCategory",
                new { categoryId = categoryToReturn.CategoryId},
                categoryToReturn);
            
        }

        [HttpOptions]
        public IActionResult GetCategoriesOptions()
        {
            Response.Headers.Add("Allow", "GET.OPTIONS,POST");
            return Ok();
        }

        [Authorize(Roles = UserRoles.Admin)]
        [HttpPut("{categoryId}")]
        public ActionResult UpdateProductForCategory(string categoryId,
            CategoryForUpdateDto category)
        {
            if (!_electronicsStoreRepository.CategoryExists(categoryId))
            {
                return NotFound();
            }

            var CategoryFromRepo = _electronicsStoreRepository.GetCategory(categoryId);
            if (CategoryFromRepo == null)
            {
                return NotFound();
            }

            //Map the entity to a productForUpdateDto
            //Apply the updated fields to that Dto
            //Map the productForUpdateDto back to that entity
            _mapper.Map(category, CategoryFromRepo);

            _electronicsStoreRepository.UpdateCategory(CategoryFromRepo);
            _electronicsStoreRepository.Save();
            return NoContent();
        }

        [Authorize(Roles = UserRoles.Admin)]
        [HttpDelete("{categoryId}")]
        public ActionResult DeleteCategory(string categoryId)
        {
            var categoryFromRepo = _electronicsStoreRepository.GetCategory(categoryId);
            if(categoryFromRepo == null)
            {
                return NotFound();
            }

            _electronicsStoreRepository.DeleteCategory(categoryFromRepo);
            _electronicsStoreRepository.Save();
            return NoContent();
        }
    }
}
