using Microsoft.EntityFrameworkCore;
using BjjTracker.Data;
using BjjTracker.Models;

namespace BjjTracker.Services
{
    public class AttendanceService : IAttendanceService
    {
        private readonly ApplicationDbContext _context;

        public AttendanceService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task CreateAsync(Attendance attendance)
        {
            _context.Attendances.Add(attendance);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> ExistsAsync(int trainingSessionId, int practitionerId)
        {
            return await _context.Attendances.AnyAsync(a => a.TrainingSessionId == trainingSessionId && a.PractitionerId == practitionerId);
        }

        public async Task<List<Attendance>> GetAllAsync(int? academyId = null)
        {
            IQueryable<Attendance> query = _context.Attendances
                .Include(a => a.Practitioner)
                .Include(a => a.TrainingSession)
                    .ThenInclude(t => t.Academy);

            if (academyId != null)
            {
                query = query.Where(a => a.TrainingSession.AcademyId == academyId);
            }

            return await query.ToListAsync();
        }
        public async Task<List<Attendance>> GetAllByPractitionerAsync(int practitionerId)
        {
            IQueryable<Attendance> query = _context.Attendances
                .Include(a => a.Practitioner)
                .Include(a => a.TrainingSession).ThenInclude(t => t.Academy)
                .Where(a => a.PractitionerId == practitionerId);

            return await query.ToListAsync();
        }
    }
    
}