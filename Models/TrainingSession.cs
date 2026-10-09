using MyMvcApp.Models.Enums;

namespace MyMvcApp.Models
{
    public class TrainingSession
    {
        public int Id { get; set; }
        public DateOnly DateOfTheTraining { get; set; }
        public TimeOnly StartTime { get; set; }
        public TimeSpan DurationOfTheTraining { get; set; }
        public TypeOfTheTraining TrainingType { get; set; }
        public int AcademyId { get; set; }

    }
}
