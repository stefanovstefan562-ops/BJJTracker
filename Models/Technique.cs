using MyMvcApp.Models.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations;

namespace MyMvcApp.Models
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
        public string? TechniqueVideo { get; set; }
    }
}