using Microsoft.AspNetCore.Mvc;
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
        public async Task<IActionResult> Create(Academy academy)
        {
            if (!ModelState.IsValid)
            {
                return View(academy);
            }

            await _academyService.CreateAsync(academy);

            return RedirectToAction("Index");
        }
        public async Task<IActionResult> Edit(int id)
        {
            var academy = await _academyService.GetByIdAsync(id);
            return View(academy);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(Academy academy)
        {
            if (!ModelState.IsValid)
            {
                return View(academy);
            }

            await _academyService.UpdateAsync(academy);

            return RedirectToAction("Index");
        }
        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            await _academyService.DeleteAsync(id);

            return RedirectToAction("Index");
        }
        public async Task<IActionResult> Index()
        {
            var academies = await _academyService.GetAllAsync();
            return View(academies);
        }
    }
}
