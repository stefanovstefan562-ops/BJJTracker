using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using MyMvcApp.Models;
using MyMvcApp.Services;

namespace MyMvcApp.Controllers
{
    public class AcademiesController : Controller
    {
        private readonly IAcademyService _academyService;

        public AcademiesController(IAcademyService academyService)
        {
            _academyService = academyService;
        }

        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Academy academy)
        {
            if (!ModelState.IsValid)
            {
                return View(academy);
            }

            _academyService.Create(academy);

            return RedirectToAction("Index");
        }
        public IActionResult Edit(int id)
        {
            var academy = _academyService.GetById(id);
            return View(academy);
        }
        [HttpPost]
        public IActionResult Edit(Academy academy)
        {
            if (!ModelState.IsValid)
            {
                return View(academy);
            }

            _academyService.Update(academy);

            return RedirectToAction("Index");
        }
        [HttpPost]
        public IActionResult Delete(int id)
        {
            _academyService.Delete(id);

            return RedirectToAction("Index");
        }
        public IActionResult Index()
        {
            var academies = _academyService.GetAll();

            return View(academies);
        }
    }
}
