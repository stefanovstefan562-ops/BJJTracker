using Microsoft.AspNetCore.Mvc;
using MyMvcApp.Data;
using MyMvcApp.Models;
using Microsoft.EntityFrameworkCore;
using System.Globalization;

namespace MyMvcApp.Controllers
{
    public class TrainingSessionsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public TrainingSessionsController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Create(int academyId)
        {
            if (!_context.Academies.Any(a => a.Id == academyId))
            {
                return RedirectToAction("Index", "Academies");
            }

            var trainingSession = new TrainingSession { AcademyId = academyId };

            return View(trainingSession);


        }

        [HttpPost]
        public IActionResult Create(TrainingSession trainingSession)
        {
            var today = DateOnly.FromDateTime(DateTime.Today);

            if (trainingSession.DateOfTheTraining < today)
            {

                ModelState.AddModelError(nameof(TrainingSession.DateOfTheTraining),
                    "The training date cannot be in the past.");
            }

            var startsAt = trainingSession.DateOfTheTraining.ToDateTime(trainingSession.StartTime);

            if (startsAt < DateTime.Now)
            {
                ModelState.AddModelError(nameof(TrainingSession.StartTime),
                    "The training start time cannot be in the past.");
            }

            if (!ModelState.IsValid)
                {
                    return View(trainingSession);
                }

            _context.TrainingSessions.Add(trainingSession);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }

        public IActionResult Edit(int id)
        {
            var trainingSession = _context.TrainingSessions.Find(id);
            return View(trainingSession);
        }

        [HttpPost]
        public IActionResult Edit(TrainingSession trainingSession)
        {
            if (!ModelState.IsValid)
            {
                return View(trainingSession);
            }

            _context.TrainingSessions.Update(trainingSession);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult Delete(int id)
        {
            var trainingSession = _context.TrainingSessions.Find(id);
            _context.TrainingSessions.Remove(trainingSession);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        public IActionResult Index()
        {
            var trainingSessions = _context.TrainingSessions.Include(t => t.Academy).ToList();
            return View(trainingSessions);
        }


    }
}