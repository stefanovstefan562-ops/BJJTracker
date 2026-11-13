using BjjTracker.Services;
using Microsoft.AspNetCore.Mvc;

namespace BjjTracker.ViewComponents
{
    public class UpcomingTrainingsViewComponent : ViewComponent
    {
        private readonly ITrainingSessionService _trainingSessionService;

        public UpcomingTrainingsViewComponent(ITrainingSessionService trainingSessionService)
        {
            _trainingSessionService = trainingSessionService;
        }

        public async Task<IViewComponentResult> InvokeAsync(int count = 3)
        {
            var trainingSessions = await _trainingSessionService.GetUpcomingAsync(count);

            return View(trainingSessions);
        }
    }
}