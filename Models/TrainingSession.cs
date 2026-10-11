using MyMvcApp.Models.Enums;
using System.ComponentModel.DataAnnotations;
using MyMvcApp.Validation;

namespace MyMvcApp.Models
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
