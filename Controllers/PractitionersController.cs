using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using BjjTracker.Models;
using BjjTracker.Services;

namespace BjjTracker.Controllers
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
        [Authorize(Roles = "Practitioner")]
        public async Task<IActionResult> Edit(int id)
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return Challenge();
            }
            if (user.PractitionerId != id)
            {
                return Forbid();
            }
            var practitioner = await _practitionerService.GetByIdAsync(id);
            if (practitioner == null)
            {
                return NotFound();
            }
            ViewBag.Academy = await _academyService.GetAllAsync();

            return View(practitioner);
        }
        [HttpPost]
        [Authorize(Roles = "Practitioner")]
        public async Task<IActionResult> Edit(Practitioner practitioner)
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return Challenge();
            }
            if (user.PractitionerId != practitioner.Id)
            {
                return Forbid();
            }
            if (!ModelState.IsValid)
            {
                ViewBag.Academy = await _academyService.GetAllAsync();
                return View(practitioner);
            }

            await _practitionerService.UpdateAsync(practitioner);
            return RedirectToAction("Index");
        }

        [HttpPost]
        [Authorize(Roles = "Practitioner")]
        public async Task<IActionResult> Delete(int id)
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return Challenge();
            }
            if (user.PractitionerId != id)
            {
                return Forbid();
            }
            await _practitionerService.DeleteAsync(id);

            return RedirectToAction("Index");
        }
        public async Task<IActionResult> Index()
        {
            var user = await _userManager.GetUserAsync(User);
            ViewBag.UserPractitionerId = user?.PractitionerId;

            var practitioners = await _practitionerService.GetAllAsync();
            return View(practitioners);
        }
    }
}
