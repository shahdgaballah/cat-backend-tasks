using AuthenticatedClubManagerMVC.Models;
using AuthenticatedClubManagerMVC.ViewModels.Identity.Roles;
using AutoMapper;
using Microsoft.AspNetCore.Identity;

namespace AuthenticatedClubManagerMVC.Mapping
{
    public class RoleProfile : Profile
    {
        public RoleProfile()
        {
            CreateMap<IdentityRole, RolesViewModel>().ReverseMap();
                
            CreateMap<User, ManageUserRolesViewModel>()
                .ForMember(des=> des.UserId, opt=>opt.MapFrom(src=>src.Id))
                .ForMember(des=> des.Username, opt=>opt.MapFrom(src=>src.UserName))
                
                ;

                
               
        }
    }
}
