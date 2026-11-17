using BjjTracker.Models.Enums;
using System.ComponentModel.DataAnnotations;

namespace BjjTracker.ViewModels
{
    public class AttendanceViewModel
    {
        public int Id { get; set; }
        [Display(Name = "Training session")]
        public int TrainingSessionId { get; set; }
        [Display(Name = "Practitioner")]
        public int PractitionerId { get; set; }
        public string? AcademyName { get; set; }
        public string? PractitionerName { get; set; }
        public DateOnly DateOfTheTraining { get; set; }
        public TimeOnly StartTime { get; set; }
        public TimeSpan DurationOfTheTraining { get; set; }
        public TypeOfTheTraining TrainingType { get; set; }
    }
}
