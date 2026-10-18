using Microsoft.AspNetCore.Mvc;
using MyMvcApp.Models;
using MyMvcApp.Services;

namespace MyMvcApp.Controllers
{
    public class TrainingSessionsController : Controller
    {
        private readonly ITrainingSessionService _trainingSessionService;

        private readonly IAcademyService _academyService;

        public TrainingSessionsController(ITrainingSessionService trainingSessionService, IAcademyService academyService)
        {
            _trainingSessionService = trainingSessionService;

            _academyService = academyService;
        }

        public async Task<IActionResult> Create(int academyId)
        {
            if (await _academyService.GetByIdAsync(academyId) == null)
            {
                return RedirectToAction("Index", "Academies");
            }

            var trainingSession = new TrainingSession { AcademyId = academyId };

            return View(trainingSession);
        }

        [HttpPost]
        public IActionResult Create(TrainingSession trainingSession)
        {
            ValidateTrainingDateTime(trainingSession);

            if (!ModelState.IsValid)
            {
                return View(trainingSession);
            }

            _trainingSessionService.Create(trainingSession);

            return RedirectToAction("Index");
        }

        public IActionResult Edit(int id)
        {
            var trainingSession = _trainingSessionService.GetById(id);
            return View(trainingSession);
        }

        [HttpPost]
        public IActionResult Edit(TrainingSession trainingSession)
        {
            var existing = _trainingSessionService.GetById(trainingSession.Id);

            if (existing == null)
            {
                return NotFound();
            }

            bool scheduleChanged =
                existing.DateOfTheTraining != trainingSession.DateOfTheTraining ||
                existing.StartTime != trainingSession.StartTime;

            if (scheduleChanged)
            {
                ValidateTrainingDateTime(trainingSession);
            }

            if (!ModelState.IsValid)
            {
                return View(trainingSession);
            }

            existing.DateOfTheTraining = trainingSession.DateOfTheTraining;
            existing.StartTime = trainingSession.StartTime;
            existing.DurationOfTheTraining = trainingSession.DurationOfTheTraining;
            existing.TrainingType = trainingSession.TrainingType;

            _trainingSessionService.Update(existing);

            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult Delete(int id)
        {
            _trainingSessionService.Delete(id);

            return RedirectToAction("Index");
        }

        public IActionResult Index()
        {
            var trainingSessions = _trainingSessionService.GetAll();
            return View(trainingSessions);
        }

        private void ValidateTrainingDateTime(TrainingSession trainingSession)
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
        }
    }
}