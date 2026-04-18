using AuthenticatedClubManagerMVC.Models;
using AuthenticatedClubManagerMVC.ViewModels.Identity;
using AuthenticatedClubManagerMVC.ViewModels.Identity.Users;
using AutoMapper;

namespace AuthenticatedClubManagerMVC.Mapping
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            //source            //dest 
            CreateMap<RegisterViewModel, User>()
                .ForMember(des => des.UserName, opt => opt.MapFrom(src => src.Username))
                .ForMember(des => des.Email, opt => opt.MapFrom(src => src.Email))
                ;

            CreateMap<User, UsersViewModel>()
                .ForMember(des => des.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(des => des.Name, opt => opt.MapFrom(src => src.UserName))
                .ForMember(des => des.Email, opt => opt.MapFrom(src => src.Email))
                ;



        }
    }
}
