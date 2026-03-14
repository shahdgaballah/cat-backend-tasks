using Microsoft.AspNetCore.Mvc;
using AuthenticatedClubManagerMVC.Data;
using AuthenticatedClubManagerMVC.Models;
using AuthenticatedClubManagerMVC.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using System.Threading.Tasks;

namespace AuthenticatedClubManagerMVC.Controllers
{
    [Authorize]
    public class ClubController : Controller
    {
       
        private readonly IClubService _clubService;
        public ClubController(IClubService clubService)
        {
            _clubService = clubService;
        }


        public async Task<IActionResult> Index()
        {
            var ClubList = await _clubService.GetAllAsync();
            return View(ClubList);
        }

        //create
        [HttpGet]
        public IActionResult Create()
        {
            return View();
           
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Club club)
        {
            if (ModelState.IsValid)
            {
                if (club.File == null || club.File.Length == 0)
                {
                    ModelState.AddModelError("File", "Image is required");
                    return View(club);
                }

                await _clubService.AddAsync(club);
                return RedirectToAction("Index");
            }
            return View(club);

        }

        //update

        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if(id==null || id==0) return NotFound();

            var club = await _clubService.GetByIdAsync(id.Value);///??? what is value

            if(club==null) return NotFound();

            return View(club);

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditAsync(Club club)
        {
            if (ModelState.IsValid)
            {
                //if (Club.File == null || Club.File.Length == 0)
                //{
                //    ModelState.AddModelError("File", "Image is required");
                //    return View(Club);
                //}
                return RedirectToAction("Index");
            }
            await _clubService.UpdateAsync(club);
            return View(club);

        }

        [HttpGet]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || id == 0) return NotFound();

            var club = await _clubService.GetByIdAsync(id.Value);
            if (club == null) return NotFound();

            return View(club);

        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteClub(int? id)
        {
            if (id == null || id == 0) return NotFound();
            var club = await _clubService.GetByIdAsync(id.Value);

            if (club == null) return NotFound();
            await _clubService.DeleteAsync(club);

            return RedirectToAction("Index");

        }

        [HttpPost]
        //param name should match the model attribute
        public async Task<IActionResult> IsClubNameExist(string name)
        {
            var exists = await _clubService.IsNameExistAsync(name);
            if (exists)
            {
                return Json(false);
            }
            return Json(true);
        }
    }
}
