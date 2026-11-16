using BjjTracker.Models;
using BjjTracker.Repositories;
using Microsoft.EntityFrameworkCore;

namespace BjjTracker.Services
{
    public class PractitionerService : IPractitionerService
    {
        private readonly IRepository<Practitioner> _repository;

        public PractitionerService(IRepository<Practitioner> repository)
        {
            _repository = repository;
        }
        public async Task<Practitioner?> GetByIdAsync(int id)
        {
            return await _repository.GetByIdAsync(id);
        }
        public async Task DeleteAsync(int id)
        {
            var practitioner = await _repository.GetByIdAsync(id);

            if (practitioner == null)
            {
                return;
            }

            _repository.Delete(practitioner);
            await _repository.SaveChangesAsync();
        }

        public async Task<List<Practitioner>> GetAllAsync()
        {
            return await _repository.All().Include(t => t.Academy).ToListAsync();
        }
        public async Task UpdateAsync(Practitioner practitioner)
        {
            _repository.Update(practitioner);
            await _repository.SaveChangesAsync();
        }
        public async Task CreateAsync(Practitioner practitioner)
        {
            await _repository.AddAsync(practitioner);
            await _repository.SaveChangesAsync();
        }
    }
}
