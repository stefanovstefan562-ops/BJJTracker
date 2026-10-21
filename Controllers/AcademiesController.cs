using Microsoft.AspNetCore.Mvc;
using MyMvcApp.Models;
using MyMvcApp.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;

namespace MyMvcApp.Controllers
{
    [Authorize]
    public class AcademiesController : Controller
    {
        private readonly IAcademyService _academyService;

        private readonly UserManager<ApplicationUser> _userManager;
        public AcademiesController(IAcademyService academyService, UserManager<ApplicationUser> userManager)
        {
            _academyService = academyService;
            _userManager = userManager;
        }
        [Authorize(Roles = "Academy")]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [Authorize(Roles = "Academy")]
        public async Task<IActionResult> Create(Academy academy)
        {
            if (!ModelState.IsValid)
            {
                return View(academy);
            }
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return Challenge();
            }
            if (user.AcademyId != null)
            {
                return RedirectToAction("Index");
            }
            await _academyService.CreateAsync(academy);

            user.AcademyId = academy.Id;
            await _userManager.UpdateAsync(user);

            return RedirectToAction("Index");
        }
        [Authorize(Roles = "Academy")]
        public async Task<IActionResult> Edit(int id)
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return Challenge();
            }
            if (user.AcademyId != id)
            {
                return Forbid();
            }
            var academy = await _academyService.GetByIdAsync(id);
            if (academy == null)
            {
                return NotFound();
            }
            return View(academy);
        }
        [HttpPost]
        [Authorize(Roles = "Academy")]
        public async Task<IActionResult> Edit(Academy academy)
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return Challenge();
            }
            if (user.AcademyId != academy.Id)
            {
                return Forbid();
            }
            if (!ModelState.IsValid)
            {
                return View(academy);
            }

            await _academyService.UpdateAsync(academy);

            return RedirectToAction("Index");
        }
        [HttpPost]
        [Authorize(Roles = "Academy")]
        public async Task<IActionResult> Delete(int id)
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return Challenge();
            }
            if (user.AcademyId != id)
            {
                return Forbid();
            }
            await _academyService.DeleteAsync(id);

            return RedirectToAction("Index");
        }
        [Authorize]
        public async Task<IActionResult> Index()
        {
            var academies = await _academyService.GetAllAsync();
            return View(academies);
        }
    }
}
