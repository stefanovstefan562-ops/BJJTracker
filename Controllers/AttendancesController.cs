using Microsoft.AspNetCore.Mvc;
using MyMvcApp.Models;
using MyMvcApp.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;

namespace MyMvcApp.Controllers
{
    [Authorize]
    public class AttendancesController : Controller
    {
        private readonly IAttendanceService _attendanceService;

        private readonly ITrainingSessionService _trainingSessionService;

        private readonly IPractitionerService _practitionerService;

        private readonly IAcademyService _academyService;

        private readonly UserManager<ApplicationUser> _userManager;
        public AttendancesController(IAttendanceService attendanceService, ITrainingSessionService trainingSessionService,
            IPractitionerService practitionerService, IAcademyService academyService, UserManager<ApplicationUser> userManager)
        {
            _attendanceService = attendanceService;
            _trainingSessionService = trainingSessionService;
            _practitionerService = practitionerService;
            _academyService = academyService;
            _userManager = userManager;
        }
        [Authorize(Roles = "Practitioner")]
        public async Task<IActionResult> Create(int trainingSessionId)
        {
            var trainingSession = await _trainingSessionService.GetByIdAsync(trainingSessionId);

            if (trainingSession == null)
            {
                return RedirectToAction("Index", "TrainingSessions");
            }

            var attendance = new Attendance { TrainingSessionId = trainingSessionId };
            ViewBag.Practitioners = await _practitionerService.GetAllAsync();

            return View(attendance);
        }
        [HttpPost]
        [Authorize(Roles = "Practitioner")]
        public async Task<IActionResult> Create(Attendance attendance)
        {
            if (await _attendanceService.ExistsAsync(attendance.TrainingSessionId, attendance.PractitionerId))
            {
                ModelState.AddModelError(nameof(Attendance.PractitionerId),
                    "This practitioner is already registered for this training session.");
            }
            if (!ModelState.IsValid)
            {
                ViewBag.Practitioners = await _practitionerService.GetAllAsync();
                return View(attendance);
            }

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
            return View(attendances);
        }
    }
}