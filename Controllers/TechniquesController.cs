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
        public async Task<IActionResult> Create(Technique technique)
        {
            if (!ModelState.IsValid)
            {
                return View(technique);
            }

            await _techniqueService.CreateAsync(technique);

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Edit(int id)
        {
            var techniques = await _techniqueService.GetByIdAsync(id);

            if (techniques == null)
            {
                return NotFound();
            }
            return View(techniques);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Technique technique)
        {
            if (!ModelState.IsValid)
            {
                return View(technique);
            }

            await _techniqueService.UpdateAsync(technique);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            await _techniqueService.DeleteAsync(id);

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Index()
        {
            var techniques = await _techniqueService.GetAllAsync();
            return View(techniques);
        }
    }
}
