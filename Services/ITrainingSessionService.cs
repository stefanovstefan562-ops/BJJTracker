using MyMvcApp.Models;

namespace MyMvcApp.Services
{
    public interface ITrainingSessionService
    {
        List<TrainingSession> GetAll();
        TrainingSession? GetById(int id);
        void Create(TrainingSession trainingSession);
        void Update(TrainingSession trainingSession);
        void Delete(int id);
    }
}

