using BjjTracker.Data;
using BjjTracker.Models;
using Microsoft.EntityFrameworkCore;

namespace BjjTracker.Services
{
    public class AcademyService : IAcademyService
    {
        private readonly ApplicationDbContext _context;

        public AcademyService(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<Academy?> GetByIdAsync(int id)
        {
            return await _context.Academies.FindAsync(id);
        }
        public async Task DeleteAsync(int id)
        {
            var academy = await _context.Academies.FindAsync(id);

            if (academy == null)
            {
                return;
            }

            _context.Academies.Remove(academy);
            await _context.SaveChangesAsync();
        }
        public async Task UpdateAsync(Academy academy)
        {
            _context.Academies.Update(academy);
            await _context.SaveChangesAsync();
        }
        public async Task<List<Academy>> GetAllAsync()
        {
            return await _context.Academies.ToListAsync();
        }
        public async Task CreateAsync(Academy academy)
        {
            _context.Academies.Add(academy);
            await _context.SaveChangesAsync();
        }
    }
}
