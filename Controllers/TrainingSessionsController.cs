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
    public class TrainingSessionsController : Controller
    {
        private readonly ITrainingSessionService _trainingSessionService;

        private readonly IAcademyService _academyService;

        private readonly UserManager<ApplicationUser> _userManager;

        private readonly IMapper _mapper;
        public TrainingSessionsController(ITrainingSessionService trainingSessionService, IAcademyService academyService, UserManager<ApplicationUser> userManager, IMapper mapper)
        {
            _trainingSessionService = trainingSessionService;

            _academyService = academyService;

            _userManager = userManager;

            _mapper = mapper;
        }
        [Authorize(Roles = "Academy")]
        public async Task<IActionResult> Create(int academyId)
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return Challenge();
            }
            if (user.AcademyId != academyId)
            {
                return Forbid();
            }
            if (await _academyService.GetByIdAsync(academyId) == null)
            {
                return RedirectToAction("Index", "Academies");
            }
            var trainingSession = new TrainingSession { AcademyId = academyId };

            return View(_mapper.Map<TrainingSessionViewModel>(trainingSession));
        }
        [HttpPost]
        [Authorize(Roles = "Academy")]
        public async Task<IActionResult> Create(TrainingSessionViewModel model)
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return Challenge();
            }
            if (user.AcademyId != model.AcademyId)
            {
                return Forbid();
            }
            ValidateTrainingDateTime(model);

            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var trainingSession = _mapper.Map<TrainingSession>(model);

            await _trainingSessionService.CreateAsync(trainingSession);
            return RedirectToAction("Index");
        }
        [Authorize(Roles = "Academy")]
        public async Task<IActionResult> Edit(int id)
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return Challenge();
            }
            var trainingSession = await _trainingSessionService.GetByIdAsync(id);
            if (trainingSession == null)
            {
                return NotFound();
            }
            if (user.AcademyId != trainingSession.AcademyId)
            {
                return Forbid();
            }

            return View(_mapper.Map<TrainingSessionViewModel>(trainingSession));
        }
        [HttpPost]
        [Authorize(Roles = "Academy")]
        public async Task<IActionResult> Edit(TrainingSessionViewModel model)
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null) 
            {
                return Challenge();
            }
            var existing = await _trainingSessionService.GetByIdAsync(model.Id);
            if (existing == null)
            {
                return NotFound();
            }
            if (user.AcademyId != existing.AcademyId)
            {
                return Forbid();
            }
            bool scheduleChanged = existing.DateOfTheTraining != model.DateOfTheTraining ||existing.StartTime != model.StartTime;

            if (scheduleChanged)
            {
                ValidateTrainingDateTime(model);
            }

            if (!ModelState.IsValid)
            {
                return View(model);
            }

            existing.DateOfTheTraining = model.DateOfTheTraining;
            existing.StartTime = model.StartTime;
            existing.DurationOfTheTraining = model.DurationOfTheTraining;
            existing.TrainingType = model.TrainingType;

            await _trainingSessionService.UpdateAsync(existing);

            return RedirectToAction("Index");
        }
        [HttpPost]
        [Authorize(Roles = "Academy")]
        public async Task<IActionResult> Delete(int id)
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return Challenge();
            }
            var existing = await _trainingSessionService.GetByIdAsync(id);
            if (existing == null)
            {
                return NotFound();
            }
            if (user.AcademyId != existing.AcademyId)
            {
                return Forbid();
            }
            await _trainingSessionService.DeleteAsync(id);

            return RedirectToAction("Index");
        }
        [Authorize]
        public async Task<IActionResult> Index(string? search, int page = 1)
        {
            var user = await _userManager.GetUserAsync(User);

            if (user == null)
            {
                return Challenge();
            }
            const int pageSize = 5;
            if (page < 1)
            {
                page = 1;
            }
            var (items, totalCount) = await _trainingSessionService.GetPagedAsync(search, page, pageSize);

            var model = new PagedResult<TrainingSessionViewModel>
            {
                Items = _mapper.Map<List<TrainingSessionViewModel>>(items),
                Page = page,
                TotalPages = (int)Math.Ceiling(totalCount / (double)pageSize),
                Search = search
            };
            ViewBag.UserAcademyId = user.AcademyId;

            return View(model);
        }
        private void ValidateTrainingDateTime(TrainingSessionViewModel model)
        {
            var today = DateOnly.FromDateTime(DateTime.Today);

            if (model.DateOfTheTraining < today)
            {
                ModelState.AddModelError(nameof(TrainingSession.DateOfTheTraining),
                    "The training date cannot be in the past.");
            }
            var startsAt = model.DateOfTheTraining.ToDateTime(model.StartTime);

            if (startsAt < DateTime.Now)
            {
                ModelState.AddModelError(nameof(TrainingSession.StartTime),
                    "The training start time cannot be in the past.");
            }
        }
    }
}