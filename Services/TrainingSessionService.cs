using Microsoft.EntityFrameworkCore;
using BjjTracker.Data;
using BjjTracker.Models;

namespace BjjTracker.Services
{
    public class TrainingSessionService : ITrainingSessionService
    {
        private readonly ApplicationDbContext _context;

        public TrainingSessionService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<TrainingSession?> GetByIdAsync(int id)
        {
            return await _context.TrainingSessions.FindAsync(id);
        }

        public async Task DeleteAsync(int id)
        {
            var trainingSession = await _context.TrainingSessions.FindAsync(id);

            if (trainingSession == null)
            {
                return;
            }

            _context.TrainingSessions.Remove(trainingSession);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(TrainingSession trainingSession)
        {
            _context.TrainingSessions.Update(trainingSession);
            await _context.SaveChangesAsync();
        }
        public async Task<List<TrainingSession>>GetAllAsync()
        {
            return await _context.TrainingSessions.Include(t => t.Academy).ToListAsync();
        }
        public async Task<List<TrainingSession>> GetUpcomingAsync(int count)
        {
            var now = DateTime.Now;
            var today = DateOnly.FromDateTime(now);
            var currentTime = TimeOnly.FromDateTime(now);

            return await _context.TrainingSessions
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
            _context.TrainingSessions.Add(trainingSession);
            await _context.SaveChangesAsync();
        }
    }
}
