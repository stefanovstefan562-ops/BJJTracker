using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MyMvcApp.Models;
using MyMvcApp.Services;
using System.Collections.Concurrent;
using System.Runtime.ConstrainedExecution;

namespace MyMvcApp.Controllers
{
    [Authorize(Roles = "Practitioner")]
    public class SparringSessionsController : Controller
    {
        private readonly ISparringSessionService _sparringSessionService;

        private readonly ITrainingSessionService _trainingSessionService;

        private readonly UserManager<ApplicationUser> _userManager;
        public SparringSessionsController(ISparringSessionService sparringSessionService, ITrainingSessionService trainingSessionService,
            UserManager<ApplicationUser> userManager)
        {
            _sparringSessionService = sparringSessionService;
            _trainingSessionService = trainingSessionService;
            _userManager = userManager;
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
