using BjjTracker.Models;
using BjjTracker.Repositories;
using Microsoft.EntityFrameworkCore;

namespace BjjTracker.Services
{
    public class AcademyService : IAcademyService
    {
        private readonly IRepository<Academy> _repository;

        public AcademyService(IRepository<Academy> repository)
        {
            _repository = repository;
        }
        public async Task<Academy?> GetByIdAsync(int id)
        {
            return await _repository.GetByIdAsync(id);
        }
        public async Task DeleteAsync(int id)
        {
            var academy = await _repository.GetByIdAsync(id);

            if (academy == null)
            {
                return;
            }

            _repository.Delete(academy);
            await _repository.SaveChangesAsync();
        }
        public async Task UpdateAsync(Academy academy)
        {
            _repository.Update(academy);
            await _repository.SaveChangesAsync();
        }
        public async Task<List<Academy>> GetAllAsync()
        {
            return await _repository.All().Include(a => a.TrainingSessions).ToListAsync();
        }
        public async Task CreateAsync(Academy academy)
        {
            await _repository.AddAsync(academy);
            await _repository.SaveChangesAsync();
        }
    }
}
