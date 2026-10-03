using Microsoft.AspNetCore.Mvc;
using MyMvcApp.Data;
using MyMvcApp.Models;

namespace MyMvcApp.Controllers
{
    public class TechniquesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public TechniquesController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Technique technique)
        {
            _context.Techniques.Add(technique);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }

        public IActionResult Edit(int id)
        {
            var technique = _context.Techniques.Find(id);
            return View(technique);
        }

        [HttpPost]
        public IActionResult Edit(Technique technique)
        {
            _context.Techniques.Update(technique);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult Delete(int id)
        {
            var technique = _context.Techniques.Find(id);
            _context.Techniques.Remove(technique);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        public IActionResult Index()
        {
            var technique = _context.Techniques.ToList();

            return View(technique);
        }
    }
}
