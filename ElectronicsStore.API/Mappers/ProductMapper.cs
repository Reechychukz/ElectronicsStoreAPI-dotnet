using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ElectronicsStore.API.Mappers
{
    public class ProductMapper : Profile
    {
        public ProductMapper()
        {
            CreateMap<Entities.Product, Model.ProductDto>()
            .ForMember(
                    dest => dest.ProductPrice,
                    opt => opt.MapFrom(src => $"${src.ProductPrice}"));

            CreateMap<Model.ProductForCreationDto, Entities.Product>();
            CreateMap<Model.ProductForUpdateDto, Entities.Product>();
            CreateMap<Entities.Product, Model.ProductForUpdateDto>();


        }
    }
}
