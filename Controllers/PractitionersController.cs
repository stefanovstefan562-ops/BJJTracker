using Microsoft.AspNetCore.Mvc;
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
            return View();
        }

        [HttpPost]
        public IActionResult Create(Practitioner practitioner)
        {
            _context.Practitioners.Add(practitioner);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }

        public IActionResult Index()
        {
            var practitioners = _context.Practitioners.ToList();

            return View(practitioners);
        }
    }
}
