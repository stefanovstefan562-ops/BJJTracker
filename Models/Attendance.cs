using System.ComponentModel.DataAnnotations;

namespace MyMvcApp.Models
{
    public class Attendance
    {
        public int Id { get; set; }
        [Display(Name = "Training session")]
        public int TrainingSessionId { get; set; }
        public TrainingSession? TrainingSession { get; set; }
        [Display(Name = "Practitioner")]
        public int PractitionerId { get; set; }
        public Practitioner? Practitioner { get; set; }
    }
}
