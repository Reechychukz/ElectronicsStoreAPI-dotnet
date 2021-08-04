using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ElectronicsStore.API.Mappers
{
    public class CartMapper : Profile
    {
        public CartMapper()
        {
            AllowNullCollections = true;
            CreateMap<Entities.Cart, Model.CartForCreationDto>().ReverseMap();
            CreateMap<Model.CartForCreationDto, Entities.Product>();

            CreateMap<Entities.Cart, Model.CartDto>();
        }
    }
}
