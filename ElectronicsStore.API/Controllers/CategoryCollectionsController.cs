using AutoMapper;
using ElectronicsStore.API.Helpers;
using ElectronicsStore.API.Model;
using ElectronicsStore.API.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ElectronicsStore.API.Controllers
{
    [ApiController]
    [Route("api/categorycollections")]
    public class CategoryCollectionsController : ControllerBase
    {
        private readonly IElectronicsStoreRepository _electronicsStoreRepository;
        private readonly IMapper _mapper;

        public CategoryCollectionsController(IElectronicsStoreRepository electronicsStoreRepository, IMapper mapper)
        {
            _electronicsStoreRepository = electronicsStoreRepository ??
                throw new ArgumentNullException(nameof(electronicsStoreRepository));
            _mapper = mapper ??
                throw new ArgumentNullException(nameof(mapper));
        }

        [HttpGet("({ids})", Name = "GetCategoryCollection")]
        public IActionResult GetCategoryCollection(
        [FromRoute] 
        [ModelBinder(BinderType = typeof(ArrayModelBinder))] IEnumerable<string> ids)
        {
            if(ids == null)
            {
                return BadRequest();
            }
            var categoryEntities = _electronicsStoreRepository.GetCategories(ids);

            if(ids.Count() != categoryEntities.Count())
            {
                return NotFound();
            }

            var categoriesToReturn = _mapper.Map<IEnumerable<CategoryDto>>(categoryEntities);

            return Ok(categoriesToReturn);
        }

        [HttpPost]
        public ActionResult<IEnumerable<CategoryDto>> CreateCategoryCollection(
            IEnumerable<CategoryForCreationDto> categoryCollection)
        {
            var categoryEntities = _mapper.Map<IEnumerable<Entities.Category>>(categoryCollection);
            foreach(var category in categoryEntities)
            {
                _electronicsStoreRepository.AddCategory(category);
            }
            _electronicsStoreRepository.Save();
            var categoryCollectionToReturn = _mapper.Map<IEnumerable<CategoryDto>>(categoryEntities);
            var idsAsString = string.Join(",", categoryCollectionToReturn.Select(a => a.Id));
            return CreatedAtRoute("GetCategoryCollection",
                new { ids = idsAsString },
                categoryCollectionToReturn);

        }
    }
}
