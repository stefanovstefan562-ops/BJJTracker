using AutoMapper;
using BjjTracker.Models;
using BjjTracker.Services;
using BjjTracker.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace BjjTracker.Controllers
{
    [Authorize]
    public class AttendancesController : Controller
    {
        private readonly IAttendanceService _attendanceService;

        private readonly ITrainingSessionService _trainingSessionService;

        private readonly IPractitionerService _practitionerService;

        private readonly IAcademyService _academyService;

        private readonly UserManager<ApplicationUser> _userManager;

        private readonly IMapper _mapper;
        public AttendancesController(IAttendanceService attendanceService, ITrainingSessionService trainingSessionService,
            IPractitionerService practitionerService, IAcademyService academyService, UserManager<ApplicationUser> userManager, IMapper mapper)
        {
            _attendanceService = attendanceService;
            _trainingSessionService = trainingSessionService;
            _practitionerService = practitionerService;
            _academyService = academyService;
            _userManager = userManager;
            _mapper = mapper;
        }
        [Authorize(Roles = "Practitioner")]
        public async Task<IActionResult> Create(int trainingSessionId)
        {
            var trainingSession = await _trainingSessionService.GetByIdAsync(trainingSessionId);

            if (trainingSession == null)
            {
                return RedirectToAction("Index", "TrainingSessions");
            }
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return Challenge();
            }
            if (user.PractitionerId == null)
            {
                return RedirectToAction("Create", "Practitioners");
            }
            var model = new AttendanceViewModel { TrainingSessionId = trainingSessionId };

            return View(model);
        }
        [HttpPost]
        [Authorize(Roles = "Practitioner")]
        public async Task<IActionResult> Create(AttendanceViewModel model)
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

            if (await _attendanceService.ExistsAsync(model.TrainingSessionId, model.PractitionerId))
            {
                ModelState.AddModelError(string.Empty, "You are already registered for this training session.");
            }
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var attendance = _mapper.Map<Attendance>(model);

            await _attendanceService.CreateAsync(attendance);

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Index(int? academyId)
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return Challenge();
            }
            if (User.IsInRole("Practitioner"))
            {
                if (user.PractitionerId == null)
                {
                    return RedirectToAction("Create", "Practitioners");
                }
                var ownAttendances = await _attendanceService.GetAllByPractitionerAsync(user.PractitionerId.Value);

                return View(_mapper.Map<List<AttendanceViewModel>>(ownAttendances));
            }
            if (User.IsInRole("Academy"))
            {
                academyId = user.AcademyId;
                if (academyId == null)
                {
                    return RedirectToAction("Index", "Academies");
                }
            }
            if (academyId != null)
            {
                var academy = await _academyService.GetByIdAsync(academyId.Value);
                if (academy == null)
                {
                    return NotFound();
                }
                ViewBag.AcademyName = academy.AcademyName;
            }
            var attendances = await _attendanceService.GetAllAsync(academyId);

            return View(_mapper.Map<List<AttendanceViewModel>>(attendances));
        }
    }
}