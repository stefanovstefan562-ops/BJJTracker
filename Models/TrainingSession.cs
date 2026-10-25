using BjjTracker.Models.Enums;
using System.ComponentModel.DataAnnotations;
using BjjTracker.Validation;

namespace BjjTracker.Models
{
    public class TrainingSession
    {
        public int Id { get; set; }
        public DateOnly DateOfTheTraining { get; set; }
        public TimeOnly StartTime { get; set; }
        [Required(ErrorMessage = "Need the duration time!")]
        [DurationRange]
        public TimeSpan DurationOfTheTraining { get; set; }
        public TypeOfTheTraining TrainingType { get; set; }
        public int AcademyId { get; set; }
        public Academy? Academy { get; set; }
    }
}
