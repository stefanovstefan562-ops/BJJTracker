
using Microsoft.EntityFrameworkCore;
using BjjTracker.Data;
using BjjTracker.Models;

namespace BjjTracker.Services
{
    public class TechniqueService : ITechniqueService
    {
        private readonly ApplicationDbContext _context;

        public TechniqueService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Technique?> GetByIdAsync(int id)
        {
            return await _context.Techniques.FindAsync(id);
        }

        public async Task DeleteAsync(int id)
        {
            var technique = await _context.Techniques.FindAsync(id);

            if (technique == null)
            {
                return;
            }

            _context.Techniques.Remove(technique);
            await _context.SaveChangesAsync();
        }
        public async Task UpdateAsync(Technique technique)
        {
            _context.Techniques.Update(technique);
            await _context.SaveChangesAsync();
        }
        public async Task<List<Technique>> GetAllAsync()
        {
            return await _context.Techniques.ToListAsync();
        }
        public async Task CreateAsync(Technique technique)
        {
            _context.Techniques.Add(technique);
            await _context.SaveChangesAsync();
        }
    }
}
