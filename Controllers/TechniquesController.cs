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
    public class TechniquesController : Controller
    {
        private readonly ITechniqueService _techniqueService;

        private readonly UserManager<ApplicationUser> _userManager;

        private readonly IMapper _mapper;

        public TechniquesController(ITechniqueService techniqueService, UserManager<ApplicationUser> userManager, IMapper mapper)
        {
            _techniqueService = techniqueService;
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
        public async Task<IActionResult> Create(TechniqueViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var technique = _mapper.Map<Technique>(model);
            await _techniqueService.CreateAsync(technique);

            return RedirectToAction("Index");
        }
        [Authorize(Roles = "Academy")]
        public async Task<IActionResult> Edit(int id)
        {
            var techniques = await _techniqueService.GetByIdAsync(id);

            if (techniques == null)
            {
                return NotFound();
            }
            return View(_mapper.Map<TechniqueViewModel>(techniques));
        }

        [HttpPost]
        [Authorize(Roles = "Academy")]
        public async Task<IActionResult> Edit(TechniqueViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var technique = _mapper.Map<Technique>(model);
            await _techniqueService.UpdateAsync(technique);
            return RedirectToAction("Index");
        }

        [HttpPost]
        [Authorize(Roles = "Academy")]
        public async Task<IActionResult> Delete(int id)
        {
            await _techniqueService.DeleteAsync(id);

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Index(string? search, int page = 1)
        {
            const int pageSize = 5;

            if (page < 1)
            {
                page = 1;
            }

            var (items, totalCount) = await _techniqueService.GetPagedAsync(search, page, pageSize);

            var model = new PagedResult<TechniqueViewModel>
            {
                Items = _mapper.Map<List<TechniqueViewModel>>(items),
                Page = page,
                TotalPages = (int)Math.Ceiling(totalCount / (double)pageSize),
                Search = search
            };

            return View(model);
        }
    }
}
