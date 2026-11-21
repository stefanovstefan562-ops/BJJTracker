using AutoMapper;
using BjjTracker.Models;
using BjjTracker.Services;
using BjjTracker.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace BjjTracker.Controllers
{
    [Authorize(Roles = "Practitioner,Administrator")]
    public class SparringSessionsController : Controller
    {
        private readonly ISparringSessionService _sparringSessionService;

        private readonly ITrainingSessionService _trainingSessionService;

        private readonly IAttendanceService _attendanceService;

        private readonly UserManager<ApplicationUser> _userManager;

        private readonly IMapper _mapper;

        public SparringSessionsController(ISparringSessionService sparringSessionService, ITrainingSessionService trainingSessionService,
            IAttendanceService attendanceService,UserManager<ApplicationUser> userManager, IMapper mapper)
        {
            _sparringSessionService = sparringSessionService;
            _trainingSessionService = trainingSessionService;
            _attendanceService = attendanceService;
            _userManager = userManager;
            _mapper = mapper;
        }
        [Authorize(Roles = "Practitioner")]
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
        [Authorize(Roles = "Practitioner")]
        public async Task<IActionResult> Create(SparringSessionViewModel model)
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
            model.PractitionerId = user.PractitionerId.Value;
            if (!await _attendanceService.ExistsAsync(model.TrainingSessionId, user.PractitionerId.Value))
            {
                ModelState.AddModelError(nameof(SparringSession.TrainingSessionId),
                    "You can add a sparring session only for a training you attended.");
            }
            if (!ModelState.IsValid)
            {
                var attendances = await _attendanceService.GetAllByPractitionerAsync(user.PractitionerId.Value);
                ViewBag.TrainingSessions = attendances.Select(a => a.TrainingSession);
                return View(model);
            }

            var sparringSession = _mapper.Map<SparringSession>(model);
            await _sparringSessionService.CreateAsync(sparringSession);
            return RedirectToAction("Index");
        }
        [Authorize(Roles = "Practitioner")]
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
            return View(_mapper.Map<SparringSessionViewModel>(existing));
        }
        [HttpPost]
        [Authorize(Roles = "Practitioner")]
        public async Task<IActionResult> Edit(SparringSessionViewModel model)
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
            var existing = await _sparringSessionService.GetByIdAsync(model.Id);
            if (existing == null)
            {
                return NotFound();
            }
            if (user.PractitionerId != existing.PractitionerId)
            {
                return Forbid();
            }
            if (!await _attendanceService.ExistsAsync(model.TrainingSessionId, user.PractitionerId.Value))
            {
                ModelState.AddModelError(nameof(SparringSession.TrainingSessionId),
                    "You can add a sparring session only for a training you attended.");
            }
            if (!ModelState.IsValid)
            {
                var attendances = await _attendanceService.GetAllByPractitionerAsync(user.PractitionerId.Value);
                ViewBag.TrainingSessions = attendances.Select(a => a.TrainingSession);
                return View(model);
            }
            existing.TrainingSessionId = model.TrainingSessionId;
            existing.SparringPrtnerName = model.SparringPrtnerName;
            existing.SparringParatnerBelt = model.SparringParatnerBelt;
            existing.SparringPartnerAcademy = model.SparringPartnerAcademy;
            existing.SparringResult = model.SparringResult;

            await _sparringSessionService.UpdateAsync(existing);
            return RedirectToAction("Index");
        }
        [HttpPost]
        [Authorize(Roles = "Practitioner")]
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
            if (User.IsInRole("Administrator"))
            {
                var all = await _sparringSessionService.GetAllAsync(null);
                return View(_mapper.Map<List<SparringSessionViewModel>>(all));
            }
                if (user.PractitionerId == null)
            {
                return RedirectToAction("Create", "Practitioners");
            }
            var sparringSessions = await _sparringSessionService.GetAllAsync(user.PractitionerId.Value);
            return View(_mapper.Map<List<SparringSessionViewModel>>(sparringSessions));
        }
    }
}
