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
    [Route("api")]
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

        //[HttpGet("carts/{cartId}")]
        //public ActionResult GetCarts(string cartId)
        //{
        //    var cartProductFromRepo = _electronicsStoreRepository.GetCartProduct(cartId);
        //    if (cartProductFromRepo == null)
        //    {
        //        return NotFound();
        //    }
        //    return Ok(_mapper.Map<CartProductDto>(cartProductFromRepo));
        //}

        //[HttpGet("users/{securityStamp}/carts/{cartId}", Name = "GetCartForUser")]
        //public ActionResult<ProductDto> GetCartForUser(string securityStamp, string cartId)
        //{
        //    if (!_electronicsStoreRepository.UserExists(securityStamp))
        //    {
        //        return NotFound();
        //    }
        //    var cartForUserFromRepo = _electronicsStoreRepository.GetCart(securityStamp, cartId);
        //    if (cartForUserFromRepo == null)
        //    {
        //        return NotFound();
        //    }
        //    return Ok(_mapper.Map<CartDto>(cartForUserFromRepo));
        //}

        [Authorize]
        [HttpPost("users/{userId}/carts")]
        public ActionResult<CartDto> CreateCartForUser(
           string userId, CartForCreationDto cartForCreationDto)
        {
            if (!_electronicsStoreRepository.UserExists(userId))
            {
                return NotFound();
            }

            

            var cartEntity = _mapper.Map<Entities.Cart>(cartForCreationDto);
            _electronicsStoreRepository.AddCart(userId, cartEntity);
            _electronicsStoreRepository.Save();

            var cartToReturn = _mapper.Map<CartDto>(cartEntity);

            return CreatedAtRoute("GetCartForUser",
                new { securityStamp = userId, cartId = cartToReturn.Id },
                cartToReturn);
        }
        [Authorize]

        [HttpGet("carts/{cartId}/cartproducts")]
        public ActionResult<IEnumerable<CartProductDto>> GetProductsForCart(string cartId)
        {
            if (!_electronicsStoreRepository.CartExists(cartId))
            {
                return NotFound();
            }
            var productsForCartFromRepo = _electronicsStoreRepository.GetCartProducts(cartId);
            //foreach(var cartProduct in productsForCartFromRepo)
            //{
            //    cartProduct.ProductPrice += productsForCartFromRepo.p
            //}
            return Ok(_mapper.Map<IEnumerable<CartProductDto>>(productsForCartFromRepo));

        }

        [Authorize]
        [HttpPost("carts/{cartId}/products/productId")]
        public ActionResult AddProductToCart(string cartId, string productId, AddProductToCartDto cartProductDto)
        {

            if (!_electronicsStoreRepository.ProductExists(productId))
            {
                return NotFound("Product does not exist");
            }
            if (_electronicsStoreRepository.ProductExists(productId))
            {
                return NotFound("Product has already been added to cart");
            }

            if (!_electronicsStoreRepository.CartExists(cartId))
            {
                return NotFound("Cart Not Found");
            }

            var productFromRepo = _electronicsStoreRepository.GetProduct(productId);
            if(productFromRepo == null)
            {
                return NotFound();
            }

            
            var cartProduct = _mapper.Map<Entities.CartProduct>(cartProductDto);
            
            _electronicsStoreRepository.AddCartProduct(cartId, productId, cartProduct);
            _electronicsStoreRepository.Save();

            
            return Ok("Product Added To Cart");
        }


    }
}
