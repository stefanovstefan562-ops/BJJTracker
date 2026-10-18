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

            _practitionerService.Create(practitioner);

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Edit(int id)
        {
            var practition = _practitionerService.GetById(id);
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

            _practitionerService.Update(practitioner);

            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult Delete(int id)
        {
            _practitionerService.Delete(id);

            return RedirectToAction("Index");
        }
        public IActionResult Index()
        {
            var practitioners = _practitionerService.GetAll();

            return View(practitioners);
        }
    }
}
