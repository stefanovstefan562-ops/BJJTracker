using BjjTracker.Models;
using System.Threading.Tasks;

namespace BjjTracker.Services
{
    public interface ITrainingSessionService
    {
        Task<List<TrainingSession>> GetAllAsync();
        Task<TrainingSession?> GetByIdAsync(int id);
        Task<List<TrainingSession>> GetUpcomingAsync(int count);
        Task CreateAsync(TrainingSession trainingSession);
        Task UpdateAsync(TrainingSession trainingSession);
        Task DeleteAsync(int id);
    }
}