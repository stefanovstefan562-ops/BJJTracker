using BjjTracker.Models.Enums;
using BjjTracker.Validation;
using System.ComponentModel.DataAnnotations;

namespace BjjTracker.Models
{
    public class Technique
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "The technique name is mandatory!!!")]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "The technique name must be at least 3 symbols!!!")]
        [Display(Name = "Technique name")]
        public required string TechniqueName { get; set; }

        [Display(Name = "Technique position")]
        public required TechniquePosition TechniquePosition { get; set; }

        [Display(Name = "Technique description")]
        public string? TechniqueDescription { get; set; }

        [Display(Name = "Technique video")]
        [VideoUrl]
        public string? TechniqueVideo { get; set; }
    }
}