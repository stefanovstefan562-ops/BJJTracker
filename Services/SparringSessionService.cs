using BjjTracker.Data;
using BjjTracker.Models;
using BjjTracker.Repositories;
using Microsoft.EntityFrameworkCore;

namespace BjjTracker.Services
{
    public class SparringSessionService : ISparringSessionService
    {
        private readonly IRepository<SparringSession> _repository;

        public SparringSessionService(IRepository<SparringSession> repository)
        {
            _repository = repository;
        }
        public async Task CreateAsync(SparringSession sparringSession)
        {
            await _repository.AddAsync(sparringSession);
            await _repository.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var sparringSession = await _repository.GetByIdAsync(id);

            if (sparringSession == null)
            {
                return;
            }

            _repository.Delete(sparringSession);
            await _repository.SaveChangesAsync();
        }

        public async Task<List<SparringSession>> GetAllAsync(int practitionerId)
        {
            IQueryable<SparringSession> query = _repository.All()
                .Include(s => s.Practitioner)
                .Include(s => s.TrainingSession)
                    .ThenInclude(s => s!.Academy);

            query = query.Where(s => s.PractitionerId == practitionerId);
            return await query.ToListAsync();
        }

        public async Task<SparringSession?> GetByIdAsync(int id)
        {
            return await _repository.GetByIdAsync(id);
        }

        public async Task UpdateAsync(SparringSession sparringSession)
        {
            _repository.Update(sparringSession);
            await _repository.SaveChangesAsync();
        }
    }
}
