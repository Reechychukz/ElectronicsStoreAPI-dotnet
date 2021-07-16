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
            CreateMap<Entities.Cart, Model.CartDto>();
            CreateMap<Model.CartForCreationDto, Entities.Cart>();
        }
    }
}
