using Microsoft.AspNetCore.Mvc;
using MyMvcApp.Data;
using MyMvcApp.Models;
using Microsoft.EntityFrameworkCore;

namespace MyMvcApp.Controllers
{
    public class AttendancesController : Controller
    {
        private readonly ApplicationDbContext _context;
        public AttendancesController(ApplicationDbContext context)
        {
            _context = context;
        }
        public IActionResult Create(int trainingSessionId)
        {
            var attendance = new Attendance { TrainingSessionId = trainingSessionId };
            ViewBag.Practitioners = _context.Practitioners.ToList();

            return View(attendance);
        }
        [HttpPost]
        public IActionResult Create(Attendance attendance)
        {
            _context.Attendances.Add(attendance);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }

        public IActionResult Index()
        {
            var attendances = _context.Attendances
            .Include(a => a.Practitioner)
            .Include(a => a.TrainingSession)
            .ToList();

            return View(attendances);
        }
    }
}