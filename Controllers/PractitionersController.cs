using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyMvcApp.Data;
using MyMvcApp.Models;
namespace MyMvcApp.Controllers
{
    public class PractitionersController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PractitionersController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Create()
        {
            ViewBag.Academy = _context.Academies.ToList();

            return View();
        }

        [HttpPost]
        public IActionResult Create(Practitioner practitioner)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Academy = _context.Academies.ToList();
                return View(practitioner);
            }
            _context.Practitioners.Add(practitioner);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }

        public IActionResult Edit(int id)
        {
            var practition = _context.Practitioners.Find(id);
            ViewBag.Academy = _context.Academies.ToList();
            return View(practition);
        }

        [HttpPost]
        public IActionResult Edit(Practitioner practitioner)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Academy = _context.Academies.ToList();
                return View(practitioner);
            }
            _context.Practitioners.Update(practitioner);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult Delete(int id)
        {
            var practition = _context.Practitioners.Find(id);
            _context.Practitioners.Remove(practition);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
        public IActionResult Index()
        {
            var practitioners = _context.Practitioners.Include(p => p.Academy).ToList();

            return View(practitioners);
        }
    }
}
