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
    public class AcademiesController : Controller
    {
        private readonly IAcademyService _academyService;

        private readonly UserManager<ApplicationUser> _userManager;
        
        private readonly IMapper _mapper;

        public AcademiesController(IAcademyService academyService, UserManager<ApplicationUser> userManager, IMapper mapper)
        {
            _academyService = academyService;
            _userManager = userManager;
            _mapper = mapper;
        }
        [Authorize(Roles = "Academy")]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [Authorize(Roles = "Academy")]
        public async Task<IActionResult> Create(AcademyViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return Challenge();
            }
            if (user.AcademyId != null)
            {
                return RedirectToAction("Index");
            }

            var academy = _mapper.Map<Academy>(model);
            await _academyService.CreateAsync(academy);

            user.AcademyId = academy.Id;
            await _userManager.UpdateAsync(user);

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
            if (user.AcademyId != id)
            {
                return Forbid();
            }
            var academy = await _academyService.GetByIdAsync(id);
            if (academy == null)
            {
                return NotFound();
            }
            return View(_mapper.Map<AcademyViewModel>(academy));
        }
        [HttpPost]
        [Authorize(Roles = "Academy")]
        public async Task<IActionResult> Edit(AcademyViewModel model)
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return Challenge();
            }
            if (user.AcademyId != model.Id)
            {
                return Forbid();
            }
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var academy = _mapper.Map<Academy>(model);
            await _academyService.UpdateAsync(academy);

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
            if (user.AcademyId != id)
            {
                return Forbid();
            }
            await _academyService.DeleteAsync(id);

            return RedirectToAction("Index");
        }
        [Authorize]
        public async Task<IActionResult> Index()
        {
            var user = await _userManager.GetUserAsync(User);
            ViewBag.UserAcademyId = user?.AcademyId;

            var academies = await _academyService.GetAllAsync();

            return View(_mapper.Map<List<AcademyViewModel>>(academies));
        }
    }
}
