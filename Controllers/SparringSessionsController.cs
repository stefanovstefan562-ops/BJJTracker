using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using BjjTracker.Models;
using BjjTracker.Services;
using System.Collections.Concurrent;
using System.Runtime.ConstrainedExecution;

namespace BjjTracker.Controllers
{
    [Authorize(Roles = "Practitioner")]
    public class SparringSessionsController : Controller
    {
        private readonly ISparringSessionService _sparringSessionService;

        private readonly ITrainingSessionService _trainingSessionService;

        private readonly IAttendanceService _attendanceService;

        private readonly UserManager<ApplicationUser> _userManager;
        public SparringSessionsController(ISparringSessionService sparringSessionService, ITrainingSessionService trainingSessionService,
            IAttendanceService attendanceService,UserManager<ApplicationUser> userManager)
        {
            _sparringSessionService = sparringSessionService;
            _trainingSessionService = trainingSessionService;
            _attendanceService = attendanceService;
            _userManager = userManager;
        }
        public async Task<IActionResult> Create()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return Challenge();
            }
            if (user.PractitionerId == null)
            {
                return RedirectToAction("Create", "Practitioners");
            }
            var attendances = await _attendanceService.GetAllByPractitionerAsync(user.PractitionerId.Value);
            if (!attendances.Any())
            {
                TempData["WelcomeMessage"] = "Register attendance for a training first, then you can add sparring sessions.";
                return RedirectToAction("Index", "TrainingSessions");
            }
            ViewBag.TrainingSessions = attendances.Select(a => a.TrainingSession);
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(SparringSession sparringSession)
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return Challenge();
            }
            if (user.PractitionerId == null)
            {
                return RedirectToAction("Create", "Practitioners");
            }
            sparringSession.PractitionerId = user.PractitionerId.Value;
            if (!await _attendanceService.ExistsAsync(sparringSession.TrainingSessionId, user.PractitionerId.Value))
            {
                ModelState.AddModelError(nameof(SparringSession.TrainingSessionId),
                    "You can add a sparring session only for a training you attended.");
            }
            if (!ModelState.IsValid)
            {
                var attendances = await _attendanceService.GetAllByPractitionerAsync(user.PractitionerId.Value);
                ViewBag.TrainingSessions = attendances.Select(a => a.TrainingSession);
                return View(sparringSession);
            }

                await _sparringSessionService.CreateAsync(sparringSession);
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> Edit(int id)
        {
            var user = await _userManager.GetUserAsync(User);

            if (user == null)
            {
                return Challenge();
            }
            if (user.PractitionerId == null)
            {
                return RedirectToAction("Create", "Practitioners");
            }
            var existing = await _sparringSessionService.GetByIdAsync(id);
            if (existing == null)
            {
                return NotFound();
            }
            if (existing.PractitionerId != user.PractitionerId)
            {
                return Forbid();
            }
            var attendances = await _attendanceService.GetAllByPractitionerAsync(user.PractitionerId.Value);
       
            ViewBag.TrainingSessions = attendances.Select(a => a.TrainingSession);
            return View(existing);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(SparringSession sparringSession)
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return Challenge();
            }
            if (user.PractitionerId == null)
            {
                return RedirectToAction("Create", "Practitioners");
            }
            var existing = await _sparringSessionService.GetByIdAsync(sparringSession.Id);
            if (existing == null)
            {
                return NotFound();
            }
            if (user.PractitionerId != existing.PractitionerId)
            {
                return Forbid();
            }
            if (!await _attendanceService.ExistsAsync(sparringSession.TrainingSessionId, user.PractitionerId.Value))
            {
                ModelState.AddModelError(nameof(SparringSession.TrainingSessionId),
                    "You can add a sparring session only for a training you attended.");
            }
            if (!ModelState.IsValid)
            {
                var attendances = await _attendanceService.GetAllByPractitionerAsync(user.PractitionerId.Value);
                ViewBag.TrainingSessions = attendances.Select(a => a.TrainingSession);
                return View(sparringSession);
            }
            existing.TrainingSessionId = sparringSession.TrainingSessionId;
            existing.SparringPrtnerName = sparringSession.SparringPrtnerName;
            existing.SparringParatnerBelt = sparringSession.SparringParatnerBelt;
            existing.SparringPartnerAcademy = sparringSession.SparringPartnerAcademy;
            existing.SparringResult = sparringSession.SparringResult;

            await _sparringSessionService.UpdateAsync(existing);
            return RedirectToAction("Index");
        }
        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            var user = await _userManager.GetUserAsync(User);

            if (user == null)
            {
                return Challenge();
            }
            if (user.PractitionerId == null)
            {
                return RedirectToAction("Create", "Practitioners");
            }
            var existing = await _sparringSessionService.GetByIdAsync(id);
            if (existing == null)
            {
                return NotFound();
            }
            if (existing.PractitionerId != user.PractitionerId) 
            {
                return Forbid();
            }
            await _sparringSessionService.DeleteAsync(id);
            return RedirectToAction("Index");

        }
        public async Task<IActionResult> Index()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return Challenge();
            }
            if (user.PractitionerId == null)
            {
                return RedirectToAction("Create", "Practitioners");
            }
            var sparringSessions = await _sparringSessionService.GetAllAsync(user.PractitionerId.Value);
            return View(sparringSessions);
        }
    }
}
