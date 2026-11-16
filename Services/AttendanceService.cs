using BjjTracker.Models;
using BjjTracker.Repositories;
using Microsoft.EntityFrameworkCore;

namespace BjjTracker.Services
{
    public class AttendanceService : IAttendanceService
    {
        private readonly IRepository<Attendance> _repository;

        public AttendanceService(IRepository<Attendance> repository)
        {
            _repository = repository;
        }

        public async Task CreateAsync(Attendance attendance)
        {
            await _repository.AddAsync(attendance);
            await _repository.SaveChangesAsync();
        }

        public async Task<bool> ExistsAsync(int trainingSessionId, int practitionerId)
        {
            return await _repository.All().AnyAsync(a => a.TrainingSessionId == trainingSessionId && a.PractitionerId == practitionerId);
        }

        public async Task<List<Attendance>> GetAllAsync(int? academyId = null)
        {
            IQueryable<Attendance> query = _repository.All()
                .Include(a => a.Practitioner)
                .Include(a => a.TrainingSession)
                    .ThenInclude(t => t!.Academy);

            if (academyId != null)
            {
                query = query.Where(a => a.TrainingSession!.AcademyId == academyId);
            }

            return await query.ToListAsync();
        }
        public async Task<List<Attendance>> GetAllByPractitionerAsync(int practitionerId)
        {
            IQueryable<Attendance> query = _repository.All()
                .Include(a => a.Practitioner)
                .Include(a => a.TrainingSession).ThenInclude(t => t!.Academy)
                .Where(a => a.PractitionerId == practitionerId);

            return await query.ToListAsync();
        }
    }
    
}