using BjjTracker.Models;
using BjjTracker.Repositories;
using Microsoft.EntityFrameworkCore;

namespace BjjTracker.Services
{
    public class TechniqueService : ITechniqueService
    {
        private readonly IRepository<Technique> _repository;

        public TechniqueService(IRepository<Technique> repository)
        {
            _repository = repository;
        }

        public async Task<Technique?> GetByIdAsync(int id)
        {
            return await _repository.GetByIdAsync(id);
        }

        public async Task DeleteAsync(int id)
        {
            var technique = await _repository.GetByIdAsync(id);

            if (technique == null)
            {
                return;
            }

            _repository.Delete(technique);
            await _repository.SaveChangesAsync();
        }
        public async Task UpdateAsync(Technique technique)
        {
            _repository.Update(technique);
            await _repository.SaveChangesAsync();
        }
        public async Task<List<Technique>> GetAllAsync()
        {
            return await _repository.All().ToListAsync();
        }
        public async Task CreateAsync(Technique technique)
        {
            await _repository.AddAsync(technique);
            await _repository.SaveChangesAsync();
        }
    }
}
