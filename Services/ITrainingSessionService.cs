using BjjTracker.Models;

namespace BjjTracker.Services
{
    public interface ITrainingSessionService
    {
        Task<List<TrainingSession>> GetAllAsync();
        Task<TrainingSession?> GetByIdAsync(int id);
        Task CreateAsync(TrainingSession trainingSession);
        Task UpdateAsync(TrainingSession trainingSession);
        Task DeleteAsync(int id);
    }
}