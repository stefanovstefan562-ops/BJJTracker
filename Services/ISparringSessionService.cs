using BjjTracker.Models;

namespace BjjTracker.Services
{
    public interface ISparringSessionService
    {
        Task<List<SparringSession>> GetAllAsync(int practitionerId);
        Task CreateAsync(SparringSession sparringSession);
        Task<SparringSession?> GetByIdAsync(int id);
        Task UpdateAsync(SparringSession sparringSession);
        Task DeleteAsync(int id);
    }
}
