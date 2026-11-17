using BjjTracker.Models.Enums;
using BjjTracker.Validation;
using System.ComponentModel.DataAnnotations;

namespace BjjTracker.ViewModels
{
    public class TrainingSessionViewModel
    {
        public int Id { get; set; }
        public DateOnly DateOfTheTraining { get; set; }
        public TimeOnly StartTime { get; set; }
        [Required(ErrorMessage = "Need the duration time!")]
        [DurationRange]
        public TimeSpan DurationOfTheTraining { get; set; }
        public TypeOfTheTraining TrainingType { get; set; }
        public int AcademyId { get; set; }
        public string? AcademyName { get; set; }
    }
}
