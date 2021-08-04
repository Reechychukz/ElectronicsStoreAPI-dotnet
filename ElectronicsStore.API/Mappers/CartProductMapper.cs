using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;

namespace ElectronicsStore.API.Mappers
{
    public class CartProductMapper : Profile
    {
        public CartProductMapper()
        {
            CreateMap<Entities.CartProduct, Model.CartProductDto>().AfterMap((src, dest) =>
            {
                dest.ProductName= src.Product.ProductName;
                dest.ProductPrice = src.Product.ProductPrice;
            }); ;
            CreateMap<Entities.CartProduct, Model.AddProductToCartDto>();
            CreateMap<Model.AddProductToCartDto, Entities.CartProduct>();
            CreateMap<Model.CartProductDto, Entities.CartProduct>();
            CreateMap<Model.CartForCreationDto, Entities.CartProduct>();
        }
    }
}
