using Microsoft.EntityFrameworkCore;
using MyMvcApp.Data;
using MyMvcApp.Models;

namespace MyMvcApp.Services
{
    public class SparringSessionService : ISparringSessionService
    {
        private readonly ApplicationDbContext _context;

        public SparringSessionService(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task CreateAsync(SparringSession sparringSession)
        {
            _context.SparringSessions.Add(sparringSession);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var sparringSession = await _context.SparringSessions.FindAsync(id);

            if (sparringSession == null)
            {
                return;
            }

            _context.SparringSessions.Remove(sparringSession);
            await _context.SaveChangesAsync();
        }

        public async Task<List<SparringSession>> GetAllAsync(int practitionerId)
        {
            IQueryable<SparringSession> query = _context.SparringSessions
                .Include(s => s.Practitioner)
                .Include(s => s.TrainingSession)
                    .ThenInclude(s => s.Academy);

            query = query.Where(s => s.PractitionerId == practitionerId);
            return await query.ToListAsync();
        }

        public async Task<SparringSession?> GetByIdAsync(int id)
        {
            return await _context.SparringSessions.FindAsync(id);
        }

        public async Task UpdateAsync(SparringSession sparringSession)
        {
            _context.SparringSessions.Update(sparringSession);
            await _context.SaveChangesAsync();
        }
    }
}
