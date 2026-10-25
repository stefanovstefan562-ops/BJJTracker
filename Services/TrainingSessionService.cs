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

        public async Task CreateAsync(TrainingSession trainingSession)
        {
            _context.TrainingSessions.Add(trainingSession);
            await _context.SaveChangesAsync();
        }
    }
}
