using AutoMapper;
using Models;
using WebAPI.DTOs;

namespace WebAPI.Dbcontext
{
    public class MappingProfile:Profile
    {
        public MappingProfile()
        {
            // Add as many of these lines as you need to map your objects
            CreateMap<UsersDto, User>();
            CreateMap<productDto, Product>();
        }
    }
}
