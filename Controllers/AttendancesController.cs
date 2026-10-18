using Microsoft.AspNetCore.Mvc;
using MyMvcApp.Models;
using MyMvcApp.Services;

namespace MyMvcApp.Controllers
{
    public class AttendancesController : Controller
    {
        private readonly IAttendanceService _attendanceService;

        private readonly ITrainingSessionService _trainingSessionService;

        private readonly IPractitionerService _practitionerService;

        private readonly IAcademyService _academyService;

        public AttendancesController(IAttendanceService attendanceService, ITrainingSessionService trainingSessionService, IPractitionerService practitionerService, IAcademyService academyService)
        {
            _attendanceService = attendanceService;
            _trainingSessionService = trainingSessionService;
            _practitionerService = practitionerService;
            _academyService = academyService;
        }
        public async Task<IActionResult> Create(int trainingSessionId)
        {
            var attendance = new Attendance { TrainingSessionId = trainingSessionId };
            ViewBag.Practitioners = await _practitionerService.GetAllAsync();

            var trainingSession = await _trainingSessionService.GetByIdAsync(trainingSessionId);
            {
                return RedirectToAction("Index", "TrainingSessions");
            }

            return View(attendance);
        }
        [HttpPost]
        public async Task<IActionResult> Create(Attendance attendance)
        {
            if (_attendanceService.Exists(attendance.TrainingSessionId, attendance.PractitionerId))
            {
                ModelState.AddModelError(nameof(Attendance.PractitionerId),
                    "This practitioner is already registered for this training session.");
            }
            if (!ModelState.IsValid)
            {
                ViewBag.Practitioners = await _practitionerService.GetAllAsync();
                return View(attendance);
            }

            _attendanceService.Create(attendance);

            return RedirectToAction("Index");
        }

        public async Task<IActionResult>Index(int? academyId)
        {
            if (academyId != null)
            {
                var academy = await _academyService.GetByIdAsync(academyId.Value);

                if (academy == null)
                {
                    return NotFound();
                }

                ViewBag.AcademyName = academy.AcademyName;
            }

            var attendances = _attendanceService.GetAll(academyId);

            return View(attendances);
        }
    }
}