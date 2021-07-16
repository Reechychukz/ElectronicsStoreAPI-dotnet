using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ElectronicsStore.API.Mappers
{
    public class CategoryMapper : Profile
    {
        public CategoryMapper()
        {
            CreateMap<Entities.Category, Model.CategoryDto>();

            CreateMap<Model.CategoryForCreationDto, Entities.Category>();
            CreateMap<Model.CategoryForUpdateDto, Entities.Category>();
            CreateMap<Entities.User, Model.UserForUpdateDto>();


        }
    }
}
