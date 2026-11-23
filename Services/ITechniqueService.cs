using BjjTracker.Models;

namespace BjjTracker.Services
{
    public interface ITechniqueService
    {
        Task<List<Technique>> GetAllAsync();
        Task<Technique?> GetByIdAsync(int id);
        Task CreateAsync(Technique technique);
        Task UpdateAsync(Technique technique);
        Task DeleteAsync(int id);
        Task<(List<Technique> Items, int TotalCount)> GetPagedAsync(string? search, int page, int pageSize);
    }
}
