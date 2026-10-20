using Microsoft.AspNetCore.Mvc;
using MyMvcApp.Models;
using MyMvcApp.Services;
using Microsoft.AspNetCore.Authorization;

namespace MyMvcApp.Controllers
{
    [Authorize]
    public class TrainingSessionsController : Controller
    {
        private readonly ITrainingSessionService _trainingSessionService;

        private readonly IAcademyService _academyService;

        public TrainingSessionsController(ITrainingSessionService trainingSessionService, IAcademyService academyService)
        {
            _trainingSessionService = trainingSessionService;

            _academyService = academyService;
        }
        [Authorize(Roles = "Academy")]
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
        [Authorize(Roles = "Academy")]
        public async Task<IActionResult> Create(TrainingSession trainingSession)
        {
            ValidateTrainingDateTime(trainingSession);

            if (!ModelState.IsValid)
            {
                return View(trainingSession);
            }

            await _trainingSessionService.CreateAsync(trainingSession);
            return RedirectToAction("Index");
        }
        [Authorize(Roles = "Academy")]
        public async Task<IActionResult> Edit(int id)
        {
            var trainingSession = await _trainingSessionService.GetByIdAsync(id);
            if (trainingSession == null)
            {
                return NotFound();
            }
            return View(trainingSession);
        }

        [HttpPost]
        [Authorize(Roles = "Academy")]
        public async Task<IActionResult> Edit(TrainingSession trainingSession)
        {
            var existing = await _trainingSessionService.GetByIdAsync(trainingSession.Id);
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

            await _trainingSessionService.UpdateAsync(existing);

            return RedirectToAction("Index");
        }
        [HttpPost]
        [Authorize(Roles = "Academy")]
        public async Task<IActionResult> Delete(int id)
        {
            await _trainingSessionService.DeleteAsync(id);

            return RedirectToAction("Index");
        }
        [Authorize]
        public async Task<IActionResult> Index()
        {
            var trainingSessions = await _trainingSessionService.GetAllAsync();
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