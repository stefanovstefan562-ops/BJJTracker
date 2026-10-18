using MyMvcApp.Models;

namespace MyMvcApp.Services
{
    public interface IAttendanceService
    {
        Task<List<Attendance>> GetAllAsync(int? academyId = null);
        Task CreateAsync(Attendance attendance);
        Task<bool> ExistsAsync(int trainingSessionId, int practitionerId);
    }
}