using Microsoft.AspNetCore.Mvc;
using MyMvcApp.Models;
using MyMvcApp.Services;
namespace MyMvcApp.Controllers
{
    public class PractitionersController : Controller
    {
        private readonly IPractitionerService _practitionerService;

        private readonly IAcademyService _academyService;

        public PractitionersController(IPractitionerService practitionerService, IAcademyService academyService)
        {
            _practitionerService = practitionerService;

            _academyService = academyService;
        }

        public async Task<IActionResult> Create()
        {
            ViewBag.Academy = await _academyService.GetAllAsync();

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Practitioner practitioner)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Academy = await _academyService.GetAllAsync();
                return View(practitioner);
            }

            await _practitionerService.CreateAsync(practitioner);

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
