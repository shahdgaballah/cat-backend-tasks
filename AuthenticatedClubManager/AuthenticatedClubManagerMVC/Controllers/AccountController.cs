using AuthenticatedClubManagerMVC.ViewModels.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace AuthenticatedClubManagerMVC.Controllers
{
    public class AccountController : Controller
    {
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly UserManager<IdentityUser> _userManager;

        public AccountController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;

     
        }
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel register)
        {
            if (ModelState.IsValid)
            {
                //find email in the db
                var user = await _userManager.FindByEmailAsync(register.Email);
                //if not found (email doest exist in db)
                if (user == null)
                {
                    var newUser = new IdentityUser()
                    {
                        Email = register.Email,
                        UserName = register.Email

                    };
                    //creating user in the db => createasync takes two params newUser and password
                    var res = await _userManager.CreateAsync(newUser, register.Password);

                    //validating password
                    if (res.Succeeded)
                    {
                        await _signInManager.SignInAsync(newUser, isPersistent: false);
                        return RedirectToAction("Index", "Home");
                    }
                    foreach (var e in res.Errors)
                    {
                        ModelState.AddModelError(string.Empty, e.Description);

                    }
                    return View(register);
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "User already exists");
                    return View(register);
                }

            }
            return View(register);
        }

        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
    }
}
