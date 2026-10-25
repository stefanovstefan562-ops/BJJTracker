using BjjTracker.Models;

namespace BjjTracker.Services
{
    public interface IAcademyService
    {
        Task<List<Academy>> GetAllAsync();
        Task<Academy?> GetByIdAsync(int id);
        Task CreateAsync(Academy academy);
        Task UpdateAsync(Academy academy);
        Task DeleteAsync(int id);
    }
}
