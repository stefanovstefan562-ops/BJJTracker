using BjjTracker.Models;
using BjjTracker.Repositories;
using BjjTracker.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
namespace BjjTracker.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Administrator")]
    public class HomeController : Controller
    {
        private readonly IRepository<Academy> _academies;

        private readonly IRepository<Practitioner> _practitioners;

        private readonly IRepository<Technique> _techniques;

        private readonly IRepository<TrainingSession> _trainingSessions;

        private readonly IRepository<Attendance> _attendances;

        private readonly IRepository<SparringSession> _sparringSessions;

        public HomeController(
            IRepository<Academy> academies,
            IRepository<Practitioner> practitioners,
            IRepository<Technique> techniques,
            IRepository<TrainingSession> trainingSessions,
            IRepository<Attendance> attendances,
            IRepository<SparringSession> sparringSessions)
        {
            _academies = academies;
            _practitioners = practitioners;
            _techniques = techniques;
            _trainingSessions = trainingSessions;
            _attendances = attendances;
            _sparringSessions = sparringSessions;
        }

        public async Task<IActionResult> Index()
        {
            var model = new AdminDashboardViewModel
            {
                AcademiesCount = await _academies.All().CountAsync(),
                 PractitionersCount = await _practitioners.All().CountAsync(),
                  TechniquesCount = await _techniques.All().CountAsync(),
                   TrainingSessionsCount = await _trainingSessions.All().CountAsync(),
                    AttendancesCount = await _attendances.All().CountAsync(),
                     SparringSessionsCount = await _sparringSessions.All().CountAsync()
            };

            return View(model);
        }
    }
}