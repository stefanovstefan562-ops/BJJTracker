using Microsoft.AspNetCore.Mvc;
using MyMvcApp.Models;
using MyMvcApp.Services;
using Microsoft.AspNetCore.Authorization;

namespace MyMvcApp.Controllers
{
    [Authorize]
    public class TechniquesController : Controller
    {
        private readonly ITechniqueService _techniqueService;

        public TechniquesController(ITechniqueService techniqueService)
        {
            _techniqueService = techniqueService;
        }
        [Authorize(Roles = "Academy")]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [Authorize(Roles = "Academy")]
        public async Task<IActionResult> Create(Technique technique)
        {
            if (!ModelState.IsValid)
            {
                return View(technique);
            }

            await _techniqueService.CreateAsync(technique);

            return RedirectToAction("Index");
        }
        [Authorize(Roles = "Academy")]
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
        [Authorize(Roles = "Academy")]
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
        [Authorize(Roles = "Academy")]
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
