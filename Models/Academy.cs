using System.ComponentModel.DataAnnotations;

namespace MyMvcApp.Models
{
    public class Academy
    {
        public int Id { get; set; }

        [Required (ErrorMessage = "The academy name is mandatory!!!")]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "The academy name must be atleast 3 symbols!!!")]
        public required string AcademyName { get; set; }
        public string? AcademyAddress { get; set; }
        public string? AcademyDescription { get; set; }
        public ICollection<TrainingSession> TrainingSessions { get; set; } = new List<TrainingSession>();
    }
}
