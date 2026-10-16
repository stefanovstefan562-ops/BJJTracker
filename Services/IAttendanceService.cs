using MyMvcApp.Models;

namespace MyMvcApp.Services
{
    public interface IAttendanceService
    {
        List<Attendance> GetAll();
        void Create(Attendance attendance);
        bool Exists(int trainingSessionId, int practitionerId);
    }
}
