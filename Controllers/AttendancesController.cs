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

            if (!_context.TrainingSessions.Any(t => t.Id == trainingSessionId))
            {
                return RedirectToAction("Index", "TrainingSessions");
            }

            return View(attendance);
        }
        [HttpPost]
        public IActionResult Create(Attendance attendance)
        {
            if (_context.Attendances.Any(a => a.TrainingSessionId == attendance.TrainingSessionId
                               && a.PractitionerId == attendance.PractitionerId))
            {
                ModelState.AddModelError(nameof(Attendance.PractitionerId),
                    "This practitioner is already registered for this training session.");
            }
            if (!ModelState.IsValid)
            {
                ViewBag.Practitioners = _context.Practitioners.ToList();
                return View(attendance);
            }

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