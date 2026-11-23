using BjjTracker.Models;
using BjjTracker.Repositories;
using Microsoft.EntityFrameworkCore;

namespace BjjTracker.Services
{
    public class TrainingSessionService : ITrainingSessionService
    {
        private readonly IRepository<TrainingSession> _repository;

        public TrainingSessionService(IRepository<TrainingSession> repository)
        {
            _repository = repository;
        }

        public async Task<TrainingSession?> GetByIdAsync(int id)
        {
            return await _repository.GetByIdAsync(id);
        }

        public async Task DeleteAsync(int id)
        {
            var trainingSession = await _repository.GetByIdAsync(id);

            if (trainingSession == null)
            {
                return;
            }

            _repository.Delete(trainingSession);
            await _repository.SaveChangesAsync();
        }

        public async Task UpdateAsync(TrainingSession trainingSession)
        {
            _repository.Update(trainingSession);
            await _repository.SaveChangesAsync();
        }
        public async Task<List<TrainingSession>>GetAllAsync()
        {
            return await _repository.All().Include(t => t.Academy).ToListAsync();
        }
        public async Task<List<TrainingSession>> GetUpcomingAsync(int count)
        {
            var now = DateTime.Now;
            var today = DateOnly.FromDateTime(now);
            var currentTime = TimeOnly.FromDateTime(now);

            return await _repository.All()
                .Include(t => t.Academy)
                .Where(t => t.DateOfTheTraining > today
                         || (t.DateOfTheTraining == today && t.StartTime >= currentTime))
                .OrderBy(t => t.DateOfTheTraining)
                .ThenBy(t => t.StartTime)
                .Take(count)
                .ToListAsync();
        }
        public async Task CreateAsync(TrainingSession trainingSession)
        {
            await _repository.AddAsync(trainingSession);
            await _repository.SaveChangesAsync();
        }
        public async Task<(List<TrainingSession> Items, int TotalCount)> GetPagedAsync(string? search, int page, int pageSize)
        {
            IQueryable<TrainingSession> query = _repository.All().Include(t => t.Academy);

            if (!string.IsNullOrWhiteSpace(search))
            {
                query = query.Where(t => t.Academy.AcademyName.Contains(search));
            }

            var totalCount = await query.CountAsync();

            var items = await query
                .OrderBy(t => t.DateOfTheTraining)
                .ThenBy(t => t.StartTime)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return (items, totalCount);
        }
    }
}
