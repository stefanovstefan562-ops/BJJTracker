using BjjTracker.Data;
using BjjTracker.Models;
using Microsoft.EntityFrameworkCore;

namespace BjjTracker.Services
{
    public class PractitionerService : IPractitionerService
    {
        private readonly ApplicationDbContext _context;

        public PractitionerService(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<Practitioner?> GetByIdAsync(int id)
        {
            return await _context.Practitioners.FindAsync(id);
        }
        public async Task DeleteAsync(int id)
        {
            var practitioner = await _context.Practitioners.FindAsync(id);

            if (practitioner == null)
            {
                return;
            }

            _context.Practitioners.Remove(practitioner);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Practitioner practitioner)
        {
            _context.Practitioners.Update(practitioner);
            await _context.SaveChangesAsync();
        }
        public async Task<List<Practitioner>> GetAllAsync()
        {
            return await _context.Practitioners.Include(p => p.Academy).ToListAsync();
        }
        public async Task CreateAsync(Practitioner practitioner)
        {
            _context.Practitioners.Add(practitioner);
            await _context.SaveChangesAsync();
        }
    }
}
