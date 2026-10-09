using Microsoft.AspNetCore.Mvc;
using MyMvcApp.Data;
using MyMvcApp.Models;

namespace MyMvcApp.Controllers
{
    public class TrainingSessionsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public TrainingSessionsController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(TrainingSession trainingSession)
        {
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
            var trainingSessions = _context.TrainingSessions.ToList();
            return View(trainingSessions);
        }


    }
}