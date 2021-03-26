using System.Linq;
using AutoMapper;
using CustomerManager.API.DTOs;
using CustomerManager.API.Models;

namespace CustomerManager.API.Profiles
{
    public class CustomerProfiles : Profile
    {
        public CustomerProfiles()
        {
            CreateMap<AppUser, CustomerDTO>()
                .ForMember(dest => dest.MainPhotoUrl, opt => opt.MapFrom(src => 
                src.Photos.FirstOrDefault(p => p.IsMain).Url));
            // for this member
            //option is map from source
            //src is main photo

            CreateMap<CustomerUpdateDTO, AppUser>();
            CreateMap<Photo, PhotoDTO>();
        }
    }
}
