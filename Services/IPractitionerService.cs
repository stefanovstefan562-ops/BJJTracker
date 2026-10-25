using BjjTracker.Models;

namespace BjjTracker.Services
{
    public interface IPractitionerService
    {
        Task<List<Practitioner>> GetAllAsync();
        Task<Practitioner?> GetByIdAsync(int id);
        Task CreateAsync(Practitioner practitioner);
        Task UpdateAsync(Practitioner practitioner);
        Task DeleteAsync(int id);
    }
}
