using MyMvcApp.Models;

namespace MyMvcApp.Services
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
