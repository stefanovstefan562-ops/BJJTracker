using MyMvcApp.Models.Enums;
using System.ComponentModel.DataAnnotations;

namespace MyMvcApp.Models
{
    public class SparringSession
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "The sparring partner name is mandatory!!!")]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "The sparring partner name must be at least 3 characters!!!")]
        [Display(Name = "Sparring partner")]
        public string? SparringPrtnerName { get; set; }
        [Display(Name = "Belt rank")]
        public BeltRank? SparringParatnerBelt {  get; set; }
        [StringLength(100, MinimumLength = 3, ErrorMessage = "The academy name must be at least 3 characters!!!")]
        [Display(Name = "Academy")]
        public string? SparringPartnerAcademy { get; set; }
        [Required(ErrorMessage = "The sparring result is mandatory!!!")]
        [Display(Name = "Sparring result")]
        public SparringResult? SparringResult { get; set; }
        [Display(Name = "Training session")]
        public int TrainingSessionId { get; set; }
        public TrainingSession? TrainingSession { get; set; }
        public int PractitionerId { get; set; }
        public Practitioner? Practitioner { get; set; }
    }
}
