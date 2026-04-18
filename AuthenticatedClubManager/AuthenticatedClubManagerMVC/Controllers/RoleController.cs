using AuthenticatedClubManagerMVC.Data;
using AuthenticatedClubManagerMVC.Models;
using AuthenticatedClubManagerMVC.ViewModels.Identity.Roles;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AuthenticatedClubManagerMVC.Controllers
{
    public class RoleController : Controller
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<User> _userManager;
        private readonly ApplicationDbContext _dbcontext;
        private readonly IMapper _mapper;
        public RoleController(RoleManager<IdentityRole> roleManager, UserManager<User> userManager, IMapper mapper, ApplicationDbContext dbcontext)
        {
            _roleManager = roleManager;
            _userManager = userManager;
            _dbcontext = dbcontext;
            _mapper = mapper;

        }
        public IActionResult Index()
        {
            var roles = _roleManager.Roles.ToList();

            var res = _mapper.Map<List<RolesViewModel>>(roles);
            return View(res);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> CreateAsync(RolesViewModel roleModel)
        {
            if (ModelState.IsValid)
            {
                var role = new IdentityRole()
                {
                    Name = roleModel.Name
                };

               var res = await _roleManager.CreateAsync(role);
                if (res.Succeeded)
                {
                    return RedirectToAction("Index");
                }
                return View(roleModel);

            }

            return View(roleModel);
        }

        [HttpGet]
        public async Task<IActionResult> Update(string id)
        {
            var role = await _roleManager.FindByIdAsync(id);
            if (role==null)
            {
                return NotFound();
            }
            var res = _mapper.Map<RolesViewModel>(role);
            return View(res);
        }

        [HttpPost]
        public async Task<IActionResult> Update(RolesViewModel roleModel)
        {
            if (ModelState.IsValid)
            {
                //oldrole
                var role = await _roleManager.FindByIdAsync(roleModel.Id);
                if (role == null) return NotFound();
                var newRole = _mapper.Map(roleModel, role);
                var res = await _roleManager.UpdateAsync(newRole);

                if(res.Succeeded)
                {
                    return RedirectToAction("Index");
                }
                return View(roleModel);
            }
            
           
            return View(roleModel);
        }

        [HttpGet]
        public async Task<IActionResult> Delete(string id)
        {
            var role = await _roleManager.FindByIdAsync(id);
            if (role == null) return NotFound();
            var res = _mapper.Map<RolesViewModel>(role);
            return View(res);


        }
        [HttpPost]
        [ActionName("Delete")]
        public async Task<IActionResult> DeleteRole(string id)
        {
            if (ModelState.IsValid)
            {
                var role = await _roleManager.FindByIdAsync(id);
                if (role == null) return NotFound();
                var res = await _roleManager.DeleteAsync(role);
                if (res.Succeeded)
                {
                    return RedirectToAction("Index");
                }
                return View(res);
            }
           
            return View();
        }
        [HttpGet]
        public async Task<IActionResult> ManageUserRole(string id){
            var role = await _roleManager.FindByIdAsync(id);
            if (role == null) return NotFound();

            var users = await _userManager.Users.ToListAsync();

            var manageUsersList = new List<ManageUserRolesViewModel>();

            foreach(var user in users)
            {
                var managedUser = _mapper.Map<ManageUserRolesViewModel>(user);
                var isInRole = await _userManager.IsInRoleAsync(user, role.Name);
                managedUser.IsSelected = isInRole;
                manageUsersList.Add(managedUser);
            }

            return View(manageUsersList);
        
        }
        [HttpPost]
        public async Task<IActionResult> ManageUserRole(string id, List<ManageUserRolesViewModel> model){
            var role = await _roleManager.FindByIdAsync(id);
            if (role == null) return NotFound();

            await using var transaction = await _dbcontext.Database.BeginTransactionAsync();

            try
            {
                foreach (var m in model)
                {
                    var user = await _userManager.FindByIdAsync(m.UserId);
                    if (user == null) return NotFound();

                    var isInRole = await _userManager.IsInRoleAsync(user, role.Name);
                    IdentityResult res;

                    //add only if user not already in role
                    if (m.IsSelected && !isInRole)
                    {
                        res = await _userManager.AddToRoleAsync(user, role.Name);
                    }
                    else if (!m.IsSelected && isInRole)
                    {
                        //remove user if is in role
                        res = await _userManager.RemoveFromRoleAsync(user, role.Name);
                    }
                    //if all is ok continue
                    else continue;

                    if (!res.Succeeded)
                    {
                        //if role operation failed, rollback all previous changes
                        await transaction.RollbackAsync();
                        foreach (var e in res.Errors)
                        {
                            ModelState.AddModelError(string.Empty, e.Description);

                        }
                        return View(model);
                    }
                    //refresh stamp so role reflects in cookie immediately w/o making the user to relogin
                    await _userManager.UpdateSecurityStampAsync(user);
                }
                //else all operations succeeded, save everything
                await transaction.CommitAsync();
            }
            catch (Exception e) {
                // if an exception is caught, rollback everything so the db goes back to exactly the state it was before this request
                await transaction.RollbackAsync();
                ModelState.AddModelError(string.Empty, e.Message);
                return View(model);
            }
            return RedirectToAction("Index");
        
        }

    }
}
