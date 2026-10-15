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

        public IActionResult Create()
        {
            ViewBag.Academy = _academyService.GetAll();

            return View();
        }

        [HttpPost]
        public IActionResult Create(Practitioner practitioner)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Academy = _academyService.GetAll();
                return View(practitioner);
            }

            _practitionerService.Create(practitioner);

            return RedirectToAction("Index");
        }

        public IActionResult Edit(int id)
        {
            var practition = _practitionerService.GetById(id);
            ViewBag.Academy = _academyService.GetAll();
            return View(practition);
        }

        [HttpPost]
        public IActionResult Edit(Practitioner practitioner)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Academy = _academyService.GetAll();
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
