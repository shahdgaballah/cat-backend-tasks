using AuthenticatedClubManagerMVC.Models;
using AuthenticatedClubManagerMVC.ViewModels.Identity.Users;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AuthenticatedClubManagerMVC.Controllers
{
    public class UserController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly IMapper _mapper;

        public UserController(UserManager<User> userManager, IMapper mapper)
        {
            _userManager = userManager;
            _mapper = mapper;
        }
        public async Task<IActionResult> IndexAsync()
        {
            var users = await _userManager.Users.ToListAsync();
            var res = _mapper.Map <List<UsersViewModel>>(users);
            return View(res);
        }
    }
}
