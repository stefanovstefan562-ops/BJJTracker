using MyMvcApp.Models;

namespace MyMvcApp.Services
{
    public interface ITechniqueService
    {
        Task<List<Technique>> GetAllAsync();
        Task<Technique?> GetByIdAsync(int id);
        Task CreateAsync(Technique technique);
        Task UpdateAsync(Technique technique);
        Task DeleteAsync(int id);
    }
}
