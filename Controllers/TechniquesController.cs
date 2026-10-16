using Microsoft.AspNetCore.Mvc;
using MyMvcApp.Models;
using MyMvcApp.Services;

namespace MyMvcApp.Controllers
{
    public class TechniquesController : Controller
    {
        private readonly ITechniqueService _techniqueService;

        public TechniquesController(ITechniqueService techniqueService)
        {
            _techniqueService = techniqueService;
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Technique technique)
        {
            if (!ModelState.IsValid)
            {
                return View(technique);
            }

            _techniqueService.Create(technique);

            return RedirectToAction("Index");
        }

        public IActionResult Edit(int id)
        {
            var practition = _techniqueService.GetById(id);
            return View(practition);
        }

        [HttpPost]
        public IActionResult Edit(Technique technique)
        {
            if (!ModelState.IsValid)
            {
                return View(technique);
            }
            _techniqueService.Update(technique);

            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult Delete(int id)
        {
            _techniqueService.Delete(id);

            return RedirectToAction("Index");
        }

        public IActionResult Index()
        {
            var technique = _techniqueService.GetAll();

            return View(technique);
        }
    }
}
