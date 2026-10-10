using Microsoft.AspNetCore.Mvc;
using MyMvcApp.Data;
using MyMvcApp.Models;

namespace MyMvcApp.Controllers
{
    public class AcademiesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AcademiesController(ApplicationDbContext context)
        {
            _context = context;
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
            _context.Academies.Add(academy);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }
        public IActionResult Edit(int id)
        {
            var academy = _context.Academies.Find(id);
            return View(academy);
        }
        [HttpPost]
        public IActionResult Edit(Academy academy)
        {
            if (!ModelState.IsValid)
            {
                return View(academy);
            }

            _context.Academies.Update(academy);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }
        [HttpPost]
        public IActionResult Delete(int id)
        {
            var academy = _context.Academies.Find(id);
            _context.Academies.Remove(academy);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
        public IActionResult Index()
        {
            var academies = _context.Academies.ToList();

            return View(academies);
        }
    }
}
