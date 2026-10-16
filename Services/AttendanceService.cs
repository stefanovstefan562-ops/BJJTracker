using Microsoft.EntityFrameworkCore;
using MyMvcApp.Data;
using MyMvcApp.Models;

namespace MyMvcApp.Services
{
    public class AttendanceService : IAttendanceService
    {
        private readonly ApplicationDbContext _context;

        public AttendanceService(ApplicationDbContext context)
        {
            _context = context;
        }

        public void Create(Attendance attendance)
        {
            _context.Attendances.Add(attendance);
            _context.SaveChanges();
        }

        public bool Exists(int trainingSessionId, int practitionerId)
        {
            return _context.Attendances.Any(a => a.TrainingSessionId == trainingSessionId && a.PractitionerId == practitionerId);
        }

        public List<Attendance> GetAll()
        {
            return _context.Attendances.Include(p => p.Practitioner).Include(s => s.TrainingSession).ToList();
        }
    }
}
