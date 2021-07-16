using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ElectronicsStore.API.Mappers
{
    public class UserMapping : Profile
    {
        public UserMapping()
        {
            CreateMap<Model.UserForRegistrationDto, Entities.User>();
            CreateMap<Entities.User, Model.UserDto>()
                .ForMember(
                    dest => dest.Name,
                    opt => opt.MapFrom(src => $"{src.FirstName} {src.LastName}"));

            CreateMap<Model.UserForLoginDto, Entities.User>();








        }
    }
}
