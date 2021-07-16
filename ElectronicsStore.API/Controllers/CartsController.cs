using AutoMapper;
using ElectronicsStore.API.Model;
using ElectronicsStore.API.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ElectronicsStore.API.Controllers
{
    [ApiController]
    [Route("api/carts")]
    public class CartsControllers : ControllerBase
    {
        private readonly IElectronicsStoreRepository _electronicsStoreRepository;
        private readonly IMapper _mapper;

        public CartsControllers(IElectronicsStoreRepository electronicsStoreRepository, IMapper mapper)
        {
            _electronicsStoreRepository = electronicsStoreRepository ??
               throw new ArgumentNullException(nameof(electronicsStoreRepository));
            _mapper = mapper ??
                throw new ArgumentNullException(nameof(mapper));
        }

        [HttpGet]
        public ActionResult GetCarts(string cartId)
        {
            var cartProductFromRepo = _electronicsStoreRepository.GetCartProduct(cartId);
            if (cartProductFromRepo == null)
            {
                return NotFound();
            }
            return Ok(_mapper.Map<CartProductDto>(cartProductFromRepo));
        }

        [Authorize]
        [HttpPost("{securityStamp}/carts")]
        public ActionResult<CartDto> CreateCartForUser(
           string securityStamp, CartForCreationDto cart)
        {
            if (!_electronicsStoreRepository.UserExists(securityStamp))
            {
                throw new ArgumentNullException("No User Created");
            }

            var cartEntity = _mapper.Map<Entities.Cart>(cart);
            _electronicsStoreRepository.AddCart(securityStamp, cartEntity);
            _electronicsStoreRepository.Save();

            var cartToReturn = _mapper.Map<CartDto>(cartEntity);

            return CreatedAtRoute("GetCartForUser",
                new { CartId = cartToReturn.CartId, cart = cartToReturn.CartId },
                cartToReturn);
        }
        //[HttpPost]
        //public ActionResult AddProductToCart(string securityStamp, string productId)
        //{
        //    if (!_electronicsStoreRepository.ProductExists(productId))
        //    {
        //        return NotFound("Product does not exist");
        //    }

        //}


    }
}
