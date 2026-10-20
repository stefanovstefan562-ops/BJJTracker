using Microsoft.AspNetCore.Mvc;
using MyMvcApp.Models;
using MyMvcApp.Services;
using Microsoft.AspNetCore.Authorization;

namespace MyMvcApp.Controllers
{
    [Authorize]
    public class AcademiesController : Controller
    {
        private readonly IAcademyService _academyService;

        public AcademiesController(IAcademyService academyService)
        {
            _academyService = academyService;
        }
        [Authorize(Roles = "Academy")]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [Authorize(Roles = "Academy")]
        public async Task<IActionResult> Create(Academy academy)
        {
            if (!ModelState.IsValid)
            {
                return View(academy);
            }

            await _academyService.CreateAsync(academy);

            return RedirectToAction("Index");
        }
        [Authorize(Roles = "Academy")]
        public async Task<IActionResult> Edit(int id)
        {
            var academy = await _academyService.GetByIdAsync(id);
            if (academy == null)
            {
                return NotFound();
            }
            return View(academy);
        }
        [HttpPost]
        [Authorize(Roles = "Academy")]
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
        [Authorize(Roles = "Academy")]
        public async Task<IActionResult> Delete(int id)
        {
            await _academyService.DeleteAsync(id);

            return RedirectToAction("Index");
        }
        [Authorize]
        public async Task<IActionResult> Index()
        {
            var academies = await _academyService.GetAllAsync();
            return View(academies);
        }
    }
}
