using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MyMvcApp.Models;
using MyMvcApp.Services;

namespace MyMvcApp.Controllers
{
    [Authorize]
    public class PractitionersController : Controller
    {
        private readonly IPractitionerService _practitionerService;

        private readonly IAcademyService _academyService;

        private readonly UserManager<ApplicationUser> _userManager;

        public PractitionersController(IPractitionerService practitionerService, IAcademyService academyService,
            UserManager<ApplicationUser> userManager)
        {
            _practitionerService = practitionerService;

            _academyService = academyService;
            _userManager = userManager;
        }
        [Authorize(Roles = "Practitioner")]
        public async Task<IActionResult> Create()
        {
            ViewBag.Academy = await _academyService.GetAllAsync();

            return View();
        }

        [HttpPost]
        [Authorize(Roles = "Practitioner")]
        public async Task<IActionResult> Create(Practitioner practitioner)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Academy = await _academyService.GetAllAsync();
                return View(practitioner);
            }
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return Challenge();
            }
            if (user.PractitionerId != null)
            {
                return RedirectToAction("Index");
            }
            await _practitionerService.CreateAsync(practitioner);

            user.PractitionerId = practitioner.Id;
            await _userManager.UpdateAsync(user);

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Edit(int id)
        {
            var practition = await _practitionerService.GetByIdAsync(id);
            ViewBag.Academy = await _academyService.GetAllAsync();
            return View(practition);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(Practitioner practitioner)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Academy = await _academyService.GetAllAsync();
                return View(practitioner);
            }

            await _practitionerService.UpdateAsync(practitioner);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            await _practitionerService.DeleteAsync(id);

            return RedirectToAction("Index");
        }
        public async Task<IActionResult> Index()
        {
            var practitioner = await _practitionerService.GetAllAsync();
            return View(practitioner);
        }
    }
}
