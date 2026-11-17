using System.ComponentModel.DataAnnotations;

namespace BjjTracker.ViewModels
{
    public class AcademyViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "The academy name is mandatory!!!")]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "The academy name must be at least 3 symbols!!!")]
        public required string AcademyName { get; set; }
        public string? AcademyAddress { get; set; }
        public string? AcademyDescription { get; set; }
    }
}
