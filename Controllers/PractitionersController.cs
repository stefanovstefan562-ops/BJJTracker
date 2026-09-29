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

        public IActionResult Index()
        {
            var practitioners = _context.Practitioners.ToList();

            return View(practitioners);
        }
    }
}
