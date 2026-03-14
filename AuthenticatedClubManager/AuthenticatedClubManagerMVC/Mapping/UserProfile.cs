using AuthenticatedClubManagerMVC.Models;
using AuthenticatedClubManagerMVC.ViewModels.Identity;
using AutoMapper;

namespace AuthenticatedClubManagerMVC.Mapping
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            //source            //dest 
            CreateMap<RegisterViewModel, User>()
                .ForMember(des => des.UserName, opt => opt.MapFrom(src => src.Email))
                .ForMember(des => des.Email, opt => opt.MapFrom(src => src.Email))
                .ForMember(des => des.Name, opt => opt.MapFrom(src => src.Name))
                ;
                
                
                
        }
    }
}
