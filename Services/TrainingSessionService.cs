using Microsoft.EntityFrameworkCore;
using MyMvcApp.Data;
using MyMvcApp.Models;

namespace MyMvcApp.Services
{
    public class TrainingSessionService : ITrainingSessionService
    {
        private readonly ApplicationDbContext _context;

        public TrainingSessionService (ApplicationDbContext context)
        {
            _context = context;
        }

        public void Create(TrainingSession trainingSession)
        {
            _context.TrainingSessions.Add(trainingSession);
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            var trainingSession = _context.TrainingSessions.Find(id);

            if (trainingSession == null)
            { return; }
            _context.TrainingSessions.Remove(trainingSession);
            _context.SaveChanges();
        }

        public List<TrainingSession> GetAll()
        {
            return _context.TrainingSessions.Include(t => t.Academy).ToList();
        }

        public TrainingSession? GetById(int id)
        {
            return _context.TrainingSessions.Find(id);
        }

        public void Update(TrainingSession trainingSession)
        {
            _context.TrainingSessions.Update(trainingSession);
            _context.SaveChanges();
        }
    }
}
